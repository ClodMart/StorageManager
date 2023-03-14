using CommunityToolkit.Maui.Core;
using DBManager.Models;
using StorageManagerMobile.CustomComponents.ViewModels;
using StorageManagerMobile.Services;
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
        private readonly GestioneMagazzinoContext context = DBService.Instance.DbContext;

        private string LastSearch = "";
        private string LastFilter = "";

        //public List<Ingredient> AllIngredients { get; set; }
        //private List<Ingredient> FilteredIngredients { get; set; }

        private readonly List<IngredientViewerViewModel> FullIngredients;
        private List<IngredientViewerViewModel> FilteredIngredients { get; set; }


        private ObservableCollection<IngredientViewerViewModel> ingredientList;
        public ObservableCollection<IngredientViewerViewModel> IngredientList
        {
            get { return ingredientList; }
            set { ingredientList = value; NotifyPropertyChanged(nameof(IngredientList)); }
        }

        private bool showFilters = false;
        public bool ShowFilters
        {
            get { return showFilters; }
            set { showFilters = value; NotifyPropertyChanged(nameof(ShowFilters)); }
        }

        public IngredientsViewModel(List<Ingredient> List)
        {
            //AllIngredients = List;
            List<Ingredient> Ingredients = List;
            FullIngredients = new List<IngredientViewerViewModel>();  
            while(Ingredients.Count>0)
            {
                Ingredient GroupTitle = Ingredients.ElementAt(0);
                List<Ingredient> Input = new List<Ingredient>(Ingredients.FindAll(x => (x.Ingredient1 == GroupTitle.Ingredient1) && (x.Category == GroupTitle.Category)));

                FullIngredients.Add(new IngredientViewerViewModel(GroupTitle, Input));

                foreach(Ingredient y in Input)
                {
                    Ingredients.RemoveAll(x => x.Id == y.Id);
                }              
            }
            FullIngredients.Sort((l, r) => l.Title.Ingredient1.CompareTo(r.Title.Ingredient1));
            FilteredIngredients = FullIngredients;
            IngredientList = new ObservableCollection<IngredientViewerViewModel>(FullIngredients);
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

        public ICommand PerformDeletion => new Command<string>((string MatName) =>
        {
            //DeleteIngredienti(MatName);
        });

        private void FiltersMethod()
        {
            ShowFilters = !ShowFilters;
        }

        public void Search(string query)
        {
            LastSearch = query;
            List<IngredientViewerViewModel> results = new List<IngredientViewerViewModel>();
            foreach (IngredientViewerViewModel Ingredient in FilteredIngredients)
            {
                if (Ingredient.Title.Ingredient1.Contains(query, StringComparison.OrdinalIgnoreCase) || Ingredient.Title.Category.Contains(query, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(Ingredient);
                }
            }
            results.Sort((l, r) => l.Title.Ingredient1.CompareTo(r.Title.Ingredient1));
            IngredientList = new ObservableCollection<IngredientViewerViewModel>(results);
            CollapseExpanders();
        }

        public void CollapseExpanders()
        {
            foreach(IngredientViewerViewModel x in IngredientList)
            {
                x.IsExpanded = false;
            }
        }

        //public void DeleteIngredienti(string IngredientName)
        //{
        //    Ingredient Ingredient = AllIngredients.FirstOrDefault(x => x.Ingredient1 == IngredientName);
        //    if (Ingredient != null)
        //    {
        //        AllIngredients.Remove(Ingredient);
        //        FilteredIngredients.Remove(Ingredient);
        //        IngredientList = new ObservableGroupedCollection<string, Ingredient>(
        //                        FilteredIngredients.GroupBy(x => x.Ingredient1.ToUpperInvariant().ToString())
        //                        .OrderBy(y => y.Key));
        //        Search(LastSearch);
        //    }
        //}
        #endregion
        public void FilterList(string Filter)
        {
            LastFilter = Filter;
            switch (Filter)
            {
                case "FilterAll":
                    FilteredIngredients = FullIngredients;
                    Search(LastSearch);
                    break;
                case "FilterEnough":
                    FilteredIngredients = FullIngredients.FindAll(x => x.Title.IsEnough == 1);
                    Search(LastSearch);
                    break;
                case "NotEnough":
                    FilteredIngredients = FullIngredients.FindAll(x => x.Title.IsEnough == 0);
                    Search(LastSearch);
                    break;
                case "FilterPriceRising":
                    FilteredIngredients = FullIngredients.FindAll(x => x.Title.CostDifference > 0);
                    Search(LastSearch);
                    break;
                case "FilterPriceLowering":
                    FilteredIngredients = FullIngredients.FindAll(x => x.Title.CostDifference < 0);
                    Search(LastSearch);
                    break;
            }
            CollapseExpanders();
        }
        //#endregion
        //public void AddIngredienti(Ingredient Ingredient)
        //{
        //    AllIngredients.Add(Ingredient);
        //    FilteredIngredients.Add(Ingredient);
        //    IngredientList.Add(Ingredient);
        //    Search(LastSearch);
        //}

        //public void EditIngredienti(Ingredient Ingredient)
        //{
        //    IngredientList.Clear();
        //    int index = AllIngredients.FindIndex(x => x.Id == Ingredient.Id);
        //    if (index != -1)
        //    {
        //        AllIngredients[index] = Ingredient;
        //    }
        //    FilterList(LastFilter);
        //    Search(LastSearch);
        //}

        //public void RefreshDataset()
        //{
        //    Search(LastSearch);
        //}

        public void UpdateIngredientList(Ingredient In)
        {
            context.Ingredients.Update(In);
            context.SaveChanges();
        }
    }
}
