using DBManager.Models;
using Microsoft.Toolkit.Collections;
using StorageManagerMobile.Grouping;
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
        public List<Ingredient> AllIngredients { get; set; }
        private List<Ingredient> FilteredIngredients { get; set; }

        private ObservableCollection<IngredientsView> ingredientList;
        public ObservableCollection<IngredientsView> IngredientList
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

        public IngredientsViewModel(List<Ingredient> List)
        {
            AllIngredients = List;
            FilteredIngredients = List;
            List<Ingredient> Ingredients = FilteredIngredients;
            List<IngredientsView> L = new List<IngredientsView>();  
            while(Ingredients.Count>0)
            {
                Ingredient GroupTitle = Ingredients.ElementAt(0);
                ObservableCollection<Ingredient> Input = new ObservableCollection<Ingredient>(Ingredients.FindAll(x => (x.Ingredient1 == GroupTitle.Ingredient1) && (x.Category == GroupTitle.Category)));

                L.Add(new IngredientsView(GroupTitle, Input));

                foreach(Ingredient y in Input)
                {
                    Ingredients.RemoveAll(x => x.Id == y.Id);
                }              
            }
            IngredientList = new ObservableCollection<IngredientsView>(L);
        }

        private string LastSearch = "";
        private string LastFilter = "";

        #region Icommands

        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            //Search(query);
        });

        public ICommand PerformDeletion => new Command<string>((string MatName) =>
        {
            //DeleteIngredienti(MatName);
        });

        //public void Search(string query)
        //{
        //    LastSearch = query;
        //    List<Ingredient> results = new List<Ingredient>();
        //    foreach (Ingredient Ingredient in FilteredIngredients)
        //    {
        //        if (Ingredient.Ingredient1.Contains(query, StringComparison.OrdinalIgnoreCase) || Ingredient.Category.Contains(query, StringComparison.OrdinalIgnoreCase))
        //        {
        //            results.Add(Ingredient);
        //        }
        //    }
        //    IngredientList = new ObservableGroupedCollection<string, Ingredient>(results.GroupBy(x => x.Ingredient1.ToUpperInvariant().ToString())
        //        .OrderBy(y => y.Key));
        //}

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
        //public void FilterList(string Filter)
        //{
        //    //LastFilter = Filter;
        //    //switch (Filter)
        //    //{
        //        //case "FilterAll":
        //        //    FilteredIngredients = AllIngredients;
        //        //    Search(LastSearch);
        //        //    break;
        //        //case "FilterEnough":
        //        //    FilteredIngredients = AllIngredients.FindAll(x => x.IsEnough);
        //        //    Search(LastSearch);
        //        //    break;
        //        //case "NotEnough":
        //        //    FilteredIngredients = AllIngredients.FindAll(x => !x.IsEnough);
        //        //    Search(LastSearch);
        //        //    break;
        //        //case "FilterPriceRising":
        //        //    FilteredIngredients = AllIngredients.FindAll(x => x.PriceDifference > 0);
        //        //    Search(LastSearch);
        //        //    break;
        //        //case "FilterPriceLowering":
        //        //    FilteredIngredients = AllIngredients.FindAll(x => x.PriceDifference < 0);
        //        //    Search(LastSearch);
        //        //    break;
        //    //}
        //}
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
    }
}
