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
        private static readonly GestioneMagazzinoContext context = DBService.Instance.DbContext;
        private static readonly IngredientsRepository IngredientsRepository = new IngredientsRepository(context);

        private string LastSearch = "";
        private string LastFilter = "FilterAll";

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

        public IngredientsViewModel(List<Ingredient> List)
        {
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
                    FilteredIngredients = FullIngredients.FindAll(x => x.Title.IsEnough == true);
                    Search(LastSearch);
                    break;
                case "NotEnough":
                    FilteredIngredients = FullIngredients.FindAll(x => x.Title.IsEnough == false);
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

        public void RefreshIngredientList()
        {
            IngredientList.Clear();
            List<Ingredient> List = context.Ingredients.ToList();
            List<Ingredient> Ingredients = List;
            FullIngredients.Clear();
            while (Ingredients.Count > 0)
            {
                Ingredient GroupTitle = Ingredients.ElementAt(0);
                List<Ingredient> Input = Ingredients.FindAll(x => (x.Ingredient1 == GroupTitle.Ingredient1) && (x.Category == GroupTitle.Category));

                FullIngredients.Add(new IngredientViewerViewModel(GroupTitle, Input));

                foreach (Ingredient y in Input)
                {
                    Ingredients.RemoveAll(x => x.Id == y.Id);
                }
            }
            FilterList(LastFilter);
        }

        public void UpdateIngredientList(Ingredient In)
        {
            context.Ingredients.Update(In);
            context.SaveChanges();
        }
    }
}
