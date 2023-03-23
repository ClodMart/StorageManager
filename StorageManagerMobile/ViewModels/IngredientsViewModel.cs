using CommunityToolkit.Maui.Core;
using DBManager.Interfacce;
using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using StorageManagerMobile.Services;
using StorageManagerMobile.ViewModels.Groupings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StorageManagerMobile.ViewModels
{


    public class IngredientsViewModel : BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly IngredientsRepository IngredientsRepository = new IngredientsRepository(context);
        private static readonly IngredientsFormatsRepository IngredientsFormatsRepository = new IngredientsFormatsRepository(context);

        private string LastSearch = "";
        private string LastFilter = "FilterAll";

        private List<Ingredient> AllIngredients= new List<Ingredient>();
        private List<IngredientViewerViewModel> FullIngredients { get; set; }
        private List<IngredientViewerViewModel> FilteredIngredients { get; set; }


        private ObservableCollection<IngredientViewerViewModel> ingredientList;
        public ObservableCollection<IngredientViewerViewModel> IngredientList
        {
            get { return ingredientList; }
            set { ingredientList = value; NotifyPropertyChanged(); }
        }

        private bool showFilters = false;
        public bool ShowFilters
        {
            get { return showFilters; }
            set { showFilters = value; NotifyPropertyChanged(); }
        }

        public IngredientsViewModel()
        {
            AllIngredients = IngredientsRepository.GetAll().ToList();
            AllIngredients.Sort((l,r) => l.Name.CompareTo(r.Name));
            FullIngredients = new List<IngredientViewerViewModel>();

            foreach (Ingredient x in AllIngredients)
            {
                //List<IngredientsFormat> formats = IngredientsFormatsRepository.GetAll().Where(x=>x.IngredientId== x.Id).ToList();
                //formats.Sort((l,r)=> ((l.LastOrderDate.Value) ?? DateOnly.MinValue).CompareTo(r.LastOrderDate.Value ?? DateOnly.MinValue));
                FullIngredients.Add(new IngredientViewerViewModel(x));
            }
            FilteredIngredients = FullIngredients;
            IngredientList = new ObservableCollection<IngredientViewerViewModel>(FullIngredients);
            //List<Ingredient> Ingredients = List;
            //FullIngredients = new List<IngredientViewerViewModel>();  
            //while(Ingredients.Count>0)
            //{
            //    Ingredient GroupTitle = Ingredients.ElementAt(0);
            //    List<Ingredient> Input = new List<Ingredient>(Ingredients.FindAll(x => (x.Ingredient1 == GroupTitle.Ingredient1) && (x.Category == GroupTitle.Category)));

            //    FullIngredients.Add(new IngredientViewerViewModel(GroupTitle, Input));

            //    foreach(Ingredient y in Input)
            //    {
            //        Ingredients.RemoveAll(x => x.Id == y.Id);
            //    }              
            //}
            //FullIngredients.Sort((l, r) => l.Title.Name.CompareTo(r.Title.Name));
            //FilteredIngredients = FullIngredients;
            //IngredientList = new ObservableCollection<IngredientViewerViewModel>(FullIngredients);
        }

        #region Icommands

        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            Search(query);
        });

        public ICommand Filters => new Command(() =>
        {
            FiltersMethod();
        });

        private void FiltersMethod()
        {
            ShowFilters = !ShowFilters;
        }

        public void Search(string query)
        {
            LastSearch = query;
            List<IngredientViewerViewModel> results = new List<IngredientViewerViewModel>();
            if(query == "")
            {
                results = FilteredIngredients;
            }
            else
            {
                foreach (IngredientViewerViewModel Ingredient in FilteredIngredients)
                {
                    if (Ingredient.Title.Name.Contains(query, StringComparison.OrdinalIgnoreCase) || Ingredient.Title.Category.Contains(query, StringComparison.OrdinalIgnoreCase))
                    {
                        results.Add(Ingredient);
                    }
                }
            }

            results.Sort((l, r) => l.Title.Name.CompareTo(r.Title.Name));
            IngredientList = new ObservableCollection<IngredientViewerViewModel>(results);
            CollapseExpanders();
        }

        private void SearchDefault()
        {
            List<IngredientViewerViewModel> results = new List<IngredientViewerViewModel>();
            if (LastSearch == "")
            {
                results = FilteredIngredients;
            }
            else
            {
                foreach (IngredientViewerViewModel Ingredient in FilteredIngredients)
                {
                    if (Ingredient.Title.Name.Contains(LastSearch, StringComparison.OrdinalIgnoreCase) || Ingredient.Title.Category.Contains(LastSearch, StringComparison.OrdinalIgnoreCase))
                    {
                        results.Add(Ingredient);
                    }
                }
            }
            results.Sort((l, r) => l.Title.Name.CompareTo(r.Title.Name));
            IngredientList = new ObservableCollection<IngredientViewerViewModel>(results);
        }

        public void CollapseExpanders()
        {
            foreach(IngredientViewerViewModel x in IngredientList)
            {
                x.IsExpanded = false;
            }
        }

        #endregion
        public void FilterList(string Filter)
        {
            LastFilter = Filter;
            switch (Filter)
            {
                case "FilterAll":
                    FilteredIngredients = FullIngredients;
                    SearchDefault();
                    break;
                case "FilterEnough":
                    FilteredIngredients = FullIngredients.FindAll(x => x.Title.IsEnough == true);
                    SearchDefault();
                    break;
                case "NotEnough":
                    FilteredIngredients = FullIngredients.FindAll(x => x.Title.IsEnough == false);
                    SearchDefault();
                    break;
                case "FilterPriceRising":
                    List<IngredientsFormat> Default = new List<IngredientsFormat>();
                    FilteredIngredients.Clear();
                    foreach (IngredientViewerViewModel x in FullIngredients)
                    {
                        if (x.Ingredients.Any(x => x.IsDefault))
                        {
                            if(x.Ingredients.FirstOrDefault(x => x.IsDefault).CostDifference > 0)
                            {
                                FilteredIngredients.Add(x);
                            }                            
                        }

                    }
                    //FilteredIngredients = FullIngredients.FindAll(x => x.Ingredients.FirstOrDefault(x=>x.IsDefault).CostDifference > 0);
                    SearchDefault();
                    break;
                case "FilterPriceLowering":
                    foreach (IngredientViewerViewModel x in FullIngredients)
                    {
                        if (x.Ingredients.Any(x => x.IsDefault))
                        {
                            if (x.Ingredients.FirstOrDefault(x => x.IsDefault).CostDifference < 0)
                            {
                                FilteredIngredients.Add(x);
                            }
                        }

                    }
                    //FilteredIngredients = FullIngredients.FindAll(x => x.Ingredients.FirstOrDefault(x => x.IsDefault).CostDifference < 0);
                    SearchDefault();
                    break;
            }
            CollapseExpanders();
        }

        private void FilterDefault()
        {
            switch (LastFilter)
            {
                case "FilterAll":
                    FilteredIngredients = FullIngredients;
                    SearchDefault();
                    break;
                case "FilterEnough":
                    FilteredIngredients = FullIngredients.FindAll(x => x.Title.IsEnough == true);
                    SearchDefault();
                    break;
                case "NotEnough":
                    FilteredIngredients = FullIngredients.FindAll(x => x.Title.IsEnough == false);
                    SearchDefault();
                    break;
                case "FilterPriceRising":
                    List<IngredientsFormat> Default = new List<IngredientsFormat>();
                    FilteredIngredients.Clear();
                    foreach (IngredientViewerViewModel x in FullIngredients)
                    {
                        if (x.Ingredients.Any(x => x.IsDefault))
                        {
                            if (x.Ingredients.FirstOrDefault(x => x.IsDefault).CostDifference > 0)
                            {
                                FilteredIngredients.Add(x);
                            }
                        }

                    }
                    //FilteredIngredients = FullIngredients.FindAll(x => x.Ingredients.FirstOrDefault(x=>x.IsDefault).CostDifference > 0);
                    SearchDefault();
                    break;
                case "FilterPriceLowering":
                    foreach (IngredientViewerViewModel x in FullIngredients)
                    {
                        if (x.Ingredients.Any(x => x.IsDefault))
                        {
                            if (x.Ingredients.FirstOrDefault(x => x.IsDefault).CostDifference < 0)
                            {
                                FilteredIngredients.Add(x);
                            }
                        }

                    }
                    //FilteredIngredients = FullIngredients.FindAll(x => x.Ingredients.FirstOrDefault(x => x.IsDefault).CostDifference < 0);
                    SearchDefault();
                    break;
            }

        }

        public void RefreshIngredientList()
        {
            List<Ingredient> ToDelete = new List<Ingredient>();
            List<Ingredient> List = IngredientsRepository.GetAll().ToList();
            List.Sort((l, r) => l.Name.CompareTo(r.Name));
            FullIngredients.Clear();

            foreach (Ingredient x in List)
            {
                //List<IngredientsFormat> formats = IngredientsFormatsRepository.GetAll().Where(x => x.IngredientId == x.Id).ToList();
                //formats.Sort((l, r) => l.LastOrderDate.Value.CompareTo(r.LastOrderDate.Value));
                IngredientViewerViewModel VM = new IngredientViewerViewModel(x);
                if(VM.Ingredients.Count != 0)
                {
                    FullIngredients.Add(VM);
                }                               
                else
                {
                    ToDelete.Add(x);
                }
            }
            //FilteredIngredients = FullIngredients;
            //IngredientList = new ObservableCollection<IngredientViewerViewModel>(FullIngredients);   

            FilterDefault();
            foreach (Ingredient x in ToDelete)
            {
                IngredientsRepository.Delete(x);
            }
        }

        public void UpdateIngredientList(Ingredient In)
        {
            IngredientsRepository.Update(In);
            context.SaveChanges();
        }

        public void DeleteIngredients(IngredientViewerViewModel Ig)
        {
            IngredientList.Remove(Ig);
            FilteredIngredients.Remove(Ig);
            FullIngredients.Remove(Ig);
        }
    }
}
