using DataRepository.Services;
using DBManager.Interfacce;
using DBManager.Models;
using System.Collections.ObjectModel;

namespace DataRepository.DataModel
{
    public class IngredientsDataModel
    {
        private static IngredientsDataModel instance;
        private static readonly object padlock = new object();

        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly IngredientsRepository IngredientsRepository = new IngredientsRepository(context);
        private static readonly IsUsedValuesRepository isUsedValuesRepository = new IsUsedValuesRepository(context);

        private List<Ingredient> AllIngredients = new List<Ingredient>();
        private List<Ingredient> NotUsedIngredients = new List<Ingredient>();
        private List<Ingredient> UsedIngredients = new List<Ingredient>();

        private List<IngredientViewer> UsedIngredientLists = new List<IngredientViewer>();
        private List<IngredientViewer> NotUsedIngredientLists = new List<IngredientViewer>();

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
            List<long> IsUsedID = isUsedValuesRepository.GetUsedId();
            AllIngredients = IngredientsRepository.GetAll().ToList();
            NotUsedIngredients = AllIngredients.Where(x => !IsUsedID.Contains(x.IsUsedValue)).ToList();
            UsedIngredients = AllIngredients.Where(x => IsUsedID.Contains(x.IsUsedValue)).ToList();

            foreach (Ingredient y in NotUsedIngredients)
            {
                IngredientViewer New = new IngredientViewer(y);
                NotUsedIngredientLists.Add(New);

            }
            foreach (Ingredient x in UsedIngredients)
            {
                IngredientViewer New = new IngredientViewer(x);
                UsedIngredientLists.Add(New);
            }

            UsedIngredientLists.Sort((l, r) => l.Title.Name.CompareTo(r.Title.Name));
            NotUsedIngredientLists.Sort((l, r) => l.Title.Name.CompareTo(r.Title.Name));
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
                    return  UsedIngredientLists.FindAll(x => x.Title.IsEnough == true);
                    //SearchDefault();
                    
                case "NotEnough":
                    return  UsedIngredientLists.FindAll(x => x.Title.IsEnough == false);
                    //SearchDefault();
                    
                case "FilterPriceRising":                    
                    //FilteredIngredients.Clear();
                    foreach (IngredientViewer x in UsedIngredientLists)
                    {
                        if (x.Ingredients.Any(x => x.IsDefault))
                        {
                            if (x.Ingredients.FirstOrDefault(x => x.IsDefault).CostDifference > 0)
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
                        if (x.Ingredients.Any(x => x.IsDefault))
                        {
                            if (x.Ingredients.FirstOrDefault(x => x.IsDefault).CostDifference < 0)
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
                return NotUsedIngredientLists.Where(x => x.Title.IsUsedValue.Equals(UsedId)).ToList();
            }
            else
            {
                List<long> UsedId = isUsedValuesRepository.GetUsedId();
                return NotUsedIngredientLists.Where(x => !UsedId.Contains(x.Title.IsUsedValue)).ToList();
            }
        }
        

        private List<IngredientViewer> SearchList(List<IngredientViewer> list, string query)
        {
            List<IngredientViewer> results = new List<IngredientViewer>();
            if (query != "NoQuery")
            {
                results = list.Where(x => x.Title.Name.Contains(query, StringComparison.OrdinalIgnoreCase) || x.Title.Category.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
                results.Sort((l, r) => l.Title.Name.CompareTo(r.Title.Name));
            }
            else
            {
                results = list;
            }
            return results;
        }
        #endregion

        public long AddIngredient(IngredientViewer ingredient)
        {
            long NewId = IngredientsRepository.Add(ingredient.Title);
            foreach(IngredientsFormat x in ingredient.Ingredients)
            {
                ingredient.AddFormat(x);
            }
            context.SaveChanges();
            return NewId;
        }
    }

    public class IngredientViewer
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly IngredientsFormatsRepository IngredientsFormatsRepository = new IngredientsFormatsRepository(context);

        private List<IngredientsFormat> AllFormats = new List<IngredientsFormat>();
        public Ingredient Title;
        public List<IngredientsFormat> Ingredients;
        private string QuantityDisplay;

        public IngredientViewer(Ingredient title)
        {
            Title = title;
            AllFormats = IngredientsFormatsRepository.GetFormatsFromIngredientId(Title.Id);
            AllFormats.Sort((l, r) =>
            (l.LastOrderDate ?? DateOnly.MinValue).CompareTo(r.LastOrderDate ?? DateOnly.MinValue));
            AllFormats.Reverse();
            Ingredients = AllFormats;
            QuantityDisplay = title.ActualQuantity + "/" + title.QuantityNeeded;
        }

        public void RefreshIngredientFormat()
        {
            AllFormats = IngredientsFormatsRepository.GetFormatsFromIngredientId(Title.Id);
            AllFormats.Sort((l, r) =>
            (l.LastOrderDate ?? DateOnly.MinValue).CompareTo(r.LastOrderDate ?? DateOnly.MinValue));
            AllFormats.Reverse();
            Ingredients = AllFormats;
        }

        public long AddFormat(IngredientsFormat format)
        {
           return IngredientsFormatsRepository.Add(format);
        }

    }
   
}
