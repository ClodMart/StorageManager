using DataRepository.DataModel.DBDataModel;
using DataRepository.Services;
using DBManager.Interfacce;
using DBManager.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.ObjectModel;

namespace DataRepository.DataModel
{
    public class IngredientsDataModel
    {
        private static IngredientsDataModel instance;
        private static readonly object padlock = new object();

        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly IngredientsRepository IngredientsRepository = new IngredientsRepository(context);
        private static readonly IngredientsFormatsRepository IngredientsFormatsRepository = new IngredientsFormatsRepository(context);
        private static readonly IsUsedValuesRepository isUsedValuesRepository = new IsUsedValuesRepository(context);

        private List<IngredientTemplate> AllIngredients = new List<IngredientTemplate>();
        private List<IngredientTemplate> NotUsedIngredients = new List<IngredientTemplate>();
        private List<IngredientTemplate> UsedIngredients = new List<IngredientTemplate>();

        private List<IngredientViewer> UsedIngredientLists = new List<IngredientViewer>();
        private List<IngredientViewer> NotUsedIngredientLists = new List<IngredientViewer>();

        private List<long> IsUsedValuesID;

        public static IngredientsDataModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new IngredientsDataModel();
                    }
                    return instance;
                }
            }
        }

        public IngredientsDataModel()
        {
            //UsedValuesList.AddRange(UsedValues.IsUsedValues.Where(x => !x.CorrespondsToUsed).Select(x => x.Description).ToList());
            IsUsedValuesID = isUsedValuesRepository.GetUsedId();
            List<Ingredient> Ingredients = IngredientsRepository.GetAll().ToList();
            foreach(Ingredient x in Ingredients)
            {
                AllIngredients.Add(new IngredientTemplate(x));
            }
            NotUsedIngredients = AllIngredients.Where(x => !IsUsedValuesID.Contains(x.isUsedValue)).ToList();
            UsedIngredients = AllIngredients.Where(x => IsUsedValuesID.Contains(x.isUsedValue)).ToList();

            foreach (IngredientTemplate y in NotUsedIngredients)
            {
                IngredientViewer New = new IngredientViewer(y);
                NotUsedIngredientLists.Add(New);

            }
            foreach (IngredientTemplate x in UsedIngredients)
            {
                IngredientViewer New = new IngredientViewer(x);
                UsedIngredientLists.Add(New);
            }

            UsedIngredientLists.Sort((l, r) => l.Title.name.CompareTo(r.Title.name));
            NotUsedIngredientLists.Sort((l, r) => l.Title.name.CompareTo(r.Title.name));
        }

        public List<IngredientViewer> GetUsedIngredients(string filter, string query)
        {
            List<IngredientViewer> result = FilterUsedList(filter);
            return SearchList(result, query);
        }

        public List<IngredientViewer> GetUnUsedIngredients(string filter, string query)
        {
            List<IngredientViewer> result = FilterUnUsedList(filter);
            return SearchList(result, query);
        }

        #region GetMethods_Dependencies
        private List<IngredientViewer> FilterUsedList(string filter)
        {
            List<IngredientViewer> OUT = new List<IngredientViewer>();
            switch (filter)
            {
                case "FilterAll":
                    return UsedIngredientLists;
                    ////SearchDefault();
                    
                case "FilterEnough":
                    return  UsedIngredientLists.FindAll(x => x.Title.isEnough == true);
                    //SearchDefault();
                    
                case "NotEnough":
                    return  UsedIngredientLists.FindAll(x => x.Title.isEnough == false);
                    //SearchDefault();
                    
                case "FilterPriceRising":                    
                    //FilteredIngredients.Clear();
                    foreach (IngredientViewer x in UsedIngredientLists)
                    {
                        if (x.Ingredients.Any(x => x.isDefault))
                        {
                            if (x.Ingredients.FirstOrDefault(x => x.isDefault).costDifference > 0)
                            {
                                OUT.Add(x);
                            }
                        }
                    }
                    return OUT;
                    //SearchDefault();
                    
                case "FilterPriceLowering":                    
                    foreach (IngredientViewer x in UsedIngredientLists)
                    {
                        if (x.Ingredients.Any(x => x.isDefault))
                        {
                            if (x.Ingredients.FirstOrDefault(x => x.isDefault).costDifference < 0)
                            {
                                OUT.Add(x);
                            }
                        }
                    }
                    return OUT;
                    //SearchDefault();                    
                    default : return UsedIngredientLists;
            }
        }

        private List<IngredientViewer> FilterUnUsedList(string filter)
        {
            if (filter != "Tutti")
            {
                long UsedId = isUsedValuesRepository.GetAll().FirstOrDefault(x => x.Description == filter).Id;
                return NotUsedIngredientLists.Where(x => x.Title.isUsedValue.Equals(UsedId)).ToList();
            }
            else
            {
                List<long> UsedId = isUsedValuesRepository.GetUsedId();
                return NotUsedIngredientLists.Where(x => !UsedId.Contains(x.Title.isUsedValue)).ToList();
            }
        }
        

        private List<IngredientViewer> SearchList(List<IngredientViewer> list, string query)
        {
            List<IngredientViewer> results = new List<IngredientViewer>();
            if (query != "NoQuery")
            {
                results = list.Where(x => x.Title.name.Contains(query, StringComparison.OrdinalIgnoreCase) || x.Title.category.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
                results.Sort((l, r) => l.Title.name.CompareTo(r.Title.name));
            }
            else
            {
                results = list;
            }
            return results;
        }
        #endregion

        #region AddMethods
        public long AddIngredient(Ingredient ingredient)
        {
            long NewId = IngredientsRepository.Add(ingredient);
            ingredient.Id = NewId;
            IngredientTemplate NewIng = new IngredientTemplate(ingredient);
            AllIngredients.Add(NewIng);

            if (IsUsedValuesID.Contains(ingredient.IsUsedValue))
            {
                UsedIngredientLists.Add(new IngredientViewer(NewIng));
            }
            else
            {
                NotUsedIngredientLists.Add(new IngredientViewer(NewIng));
            }
            return NewId;
        }

        public long AddFormat(IngredientFormatTemplate Format)
        {
            IngredientTemplate ing = AllIngredients.FirstOrDefault(x => x.id == Format.ingredientId);
            if (ing != null) 
            {
                long NewID = IngredientsFormatsRepository.Add(Format.GetNewIngredientFormat());
                if (IsUsedValuesID.Contains(ing.isUsedValue))
                {
                    UsedIngredientLists.FirstOrDefault(x => x.Title == ing).Ingredients.Add(Format);
                }
                else
                {
                    UsedIngredientLists.FirstOrDefault(x => x.Title == ing).Ingredients.Add(Format);
                }
                return NewID;
            }
            else
            {
                return -1;
            }
        }
        #endregion

        #region UpdateMethods
        public bool UpdateIngredient(IngredientTemplate ing)
        {
            try
            {
                IngredientViewer OldIng = UsedIngredientLists.FirstOrDefault(x => x.Title.id == ing.id) ?? NotUsedIngredientLists.FirstOrDefault(x => x.Title.id == ing.id);
                IngredientViewer NewIng = OldIng.Clone();
                NewIng.Title = ing;
                Update(OldIng, NewIng);
                IngredientsRepository.Update(ing.GetNewIngredient());
                return true;
            }
            catch { return false; }
            //if (IsUsedValuesID.Contains(ingredient.IsUsedValue))
            //{
            //   int index = UsedIngredientLists.IndexOf(UsedIngredientLists.FirstOrDefault(x=>x.Title.Id);
            //}
            //else
            //{
            //    NotUsedIngredientLists.Add(new IngredientViewer(ingredient));
            //}

        }

        private void Update(IngredientViewer OldIng, IngredientViewer ingredient)
        {
            if (IsUsedValuesID.Contains(OldIng.Title.isUsedValue) && IsUsedValuesID.Contains(ingredient.Title.isUsedValue))
            {
                //UsedIngredientLists.ElementAt(UsedIngredientLists.IndexOf(OldIng)) = ingredient;
                UsedIngredientLists.Remove(OldIng);
                UsedIngredientLists.Add(ingredient);
                UsedIngredientLists.Sort((l, r) => l.Title.name.CompareTo(r.Title.name));
            }
            else if (!IsUsedValuesID.Contains(OldIng.Title.isUsedValue) && !IsUsedValuesID.Contains(ingredient.Title.isUsedValue))
            {
                NotUsedIngredientLists.Remove(OldIng);
                NotUsedIngredientLists.Add(ingredient);
                NotUsedIngredientLists.Sort((l, r) => l.Title.name.CompareTo(r.Title.name));
            }
            else
            {
                if (IsUsedValuesID.Contains(ingredient.Title.isUsedValue))
                {
                    NotUsedIngredientLists.Remove(OldIng);
                    UsedIngredientLists.Add(ingredient);
                    UsedIngredientLists.Sort((l, r) => l.Title.name.CompareTo(r.Title.name));
                }
                else
                {
                    UsedIngredientLists.Remove(OldIng);
                    NotUsedIngredientLists.Add(ingredient);
                    NotUsedIngredientLists.Sort((l, r) => l.Title.name.CompareTo(r.Title.name));
                }
            }
        }

        public void UpdateFormat(IngredientFormatTemplate form)
        {
            IngredientViewer OldIng = UsedIngredientLists.FirstOrDefault(x => x.Title.id == form.ingredientId) ?? NotUsedIngredientLists.FirstOrDefault(x => x.Title.id == form.ingredientId);
            int index = OldIng.Ingredients.IndexOf(OldIng.Ingredients.FirstOrDefault(x => x.id == form.id));
            OldIng.Ingredients.RemoveAt(index);
            OldIng.Ingredients.Add(form);
            OldIng.Ingredients.Sort((l, r) =>
            (l.lastOrderDate ?? DateOnly.MinValue).CompareTo(r.lastOrderDate ?? DateOnly.MinValue));
            OldIng.Ingredients.Reverse();
        }
        #endregion




    }

    public class IngredientViewer
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly IngredientsFormatsRepository IngredientsFormatsRepository = new IngredientsFormatsRepository(context);

        private List<IngredientFormatTemplate> AllFormats = new List<IngredientFormatTemplate>();
        public IngredientTemplate Title;
        public List<IngredientFormatTemplate> Ingredients;
        private string QuantityDisplay;

        public IngredientViewer()
        { }

        public IngredientViewer(IngredientTemplate title)
        {
            Title = title;
            List<IngredientsFormat> forms = IngredientsFormatsRepository.GetFormatsFromIngredientId(Title.id).ToList();
            foreach(IngredientsFormat x in forms)
            {
                AllFormats.Add(new IngredientFormatTemplate(x));
            }
            //AllFormats = IngredientsFormatsRepository.GetFormatsFromIngredientId(Title.id);
            AllFormats.Sort((l, r) =>
            (l.lastOrderDate ?? DateOnly.MinValue).CompareTo(r.lastOrderDate ?? DateOnly.MinValue));
            AllFormats.Reverse();
            Ingredients = AllFormats;
            QuantityDisplay = title.actualQuantity + "/" + title.quantityNeeded;
        }

        //public IngredientViewer(Ingredient title)
        //{
        //    Title = title;
        //    AllFormats = IngredientsFormatsRepository.GetFormatsFromIngredientId(Title.Id);
        //    AllFormats.Sort((l, r) =>
        //    (l.LastOrderDate ?? DateOnly.MinValue).CompareTo(r.LastOrderDate ?? DateOnly.MinValue));
        //    AllFormats.Reverse();
        //    Ingredients = AllFormats;
        //    QuantityDisplay = title.ActualQuantity + "/" + title.QuantityNeeded;
        //}

        public void RefreshIngredientFormat()
        {
            List<IngredientsFormat> forms = IngredientsFormatsRepository.GetFormatsFromIngredientId(Title.id).ToList();
            foreach (IngredientsFormat x in forms)
            {
                AllFormats.Add(new IngredientFormatTemplate(x));
            }
            AllFormats.Sort((l, r) =>
            (l.lastOrderDate ?? DateOnly.MinValue).CompareTo(r.lastOrderDate ?? DateOnly.MinValue));
            AllFormats.Reverse();
            Ingredients = AllFormats;
        }

        public long AddFormat(IngredientsFormat format)
        {
           return IngredientsFormatsRepository.Add(format);
        }

        public void UpdateFormat(IngredientsFormat format)
        {            
            IngredientsFormatsRepository.Update(format);
        }

        public IngredientViewer Clone()
        {
            IngredientViewer OUT = new IngredientViewer();
            OUT.Title = Title;
            OUT.Ingredients= Ingredients;
            OUT.AllFormats = AllFormats;
            OUT.QuantityDisplay = QuantityDisplay;
            return OUT;
        }
    }   
}
