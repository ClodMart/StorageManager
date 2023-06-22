using CommunityToolkit.Maui.Core;
using DBManager.Interfacce;
using DBManager.Models;
using GemBox.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using StorageManagerMobile.Resources;
using StorageManagerMobile.Services;
using StorageManagerMobile.ViewModels.Groupings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
        private static readonly IsUsedValuesRepository isUsedValuesRepository = new IsUsedValuesRepository(context);

        private string LastSearch = "";
        private string LastSearchUnused = "";
        private string LastFilter = "FilterAll";

        private List<Ingredient> AllIngredients = new List<Ingredient>();
        private List<Ingredient> NotUsedIngredients = new List<Ingredient>();
        private List<Ingredient> UsedIngredients = new List<Ingredient>();
        private List<IngredientViewerViewModel> UsedIngredientLists = new List<IngredientViewerViewModel>();
        private List<IngredientViewerViewModel> NotUsedIngredientLists = new List<IngredientViewerViewModel>();
        private List<IngredientViewerViewModel> FullIngredients { get; set; }
        private List<IngredientViewerViewModel> FilteredIngredients { get; set; }

        private List<string> usedValuesList = new List<string>() { "Tutti" };
        public List<string> UsedValuesList
        {
            get { return usedValuesList; }
            set { usedValuesList = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<IngredientViewerViewModel> ingredientList;
        public ObservableCollection<IngredientViewerViewModel> IngredientList
        {
            get { return ingredientList; }
            set { ingredientList = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<IngredientViewerViewModel> notUsedIngredientList;
        public ObservableCollection<IngredientViewerViewModel> NotUsedIngredientList
        {
            get { return notUsedIngredientList; }
            set { notUsedIngredientList = value; NotifyPropertyChanged(); }
        }

        private bool showFilters = false;
        public bool ShowFilters
        {
            get { return showFilters; }
            set { showFilters = value; NotifyPropertyChanged(); }
        }

        private bool showNotUsed = false;
        public bool ShowNotUsed
        {
            get { return showNotUsed; }
            set { showNotUsed = value; NotifyPropertyChanged(); }
        }

        public IngredientsViewModel()
        {

            //DataApiIngredientsGateaway test = new DataApiIngredientsGateaway();
            //List<IngredientViewerViewModel> Out = test.GetUsedIngredientsAsync("Tutti", "NoQuery").Result;
            UsedValuesList.AddRange(UsedValues.IsUsedValues.Where(x => !x.CorrespondsToUsed).Select(x => x.Description).ToList());
            List<long> IsUsedID = isUsedValuesRepository.GetUsedId();
            AllIngredients = IngredientsRepository.GetAll().ToList();
            NotUsedIngredients = AllIngredients.Where(x => !IsUsedID.Contains(x.IsUsedValue)).ToList();
            UsedIngredients = AllIngredients.Where(x => IsUsedID.Contains(x.IsUsedValue)).ToList();

            foreach (Ingredient y in NotUsedIngredients)
            {
                IngredientViewerViewModel New = new IngredientViewerViewModel(y);
                NotUsedIngredientLists.Add(New);

            }
            foreach (Ingredient x in UsedIngredients)
            { 
                IngredientViewerViewModel New = new IngredientViewerViewModel(x);
                UsedIngredientLists.Add(New);
            }

            UsedIngredientLists.Sort((l, r) => l.Title.Name.CompareTo(r.Title.Name));
            NotUsedIngredientLists.Sort((l, r) => l.Title.Name.CompareTo(r.Title.Name));
            FilteredIngredients = UsedIngredientLists;
            IngredientList = new ObservableCollection<IngredientViewerViewModel>(FilteredIngredients);
            NotUsedIngredientList = new ObservableCollection<IngredientViewerViewModel>(NotUsedIngredientLists);
        }

        private void UpdateVisualUnusedIngredientViewer(object sender, EventArgs e)
        {
            RefreshUnusedIngredientList();
        }

        private void UpdateVisualIngredientViewer(object sender, EventArgs e)
        {
            RefreshIngredientList();
        }

        #region UsedIngredients
        #region UsedCommands

        public ICommand PerformSearch => new Command<string>((query) =>
        {
            Search(query);
        });

        public void Search(string query)
        {
            LastSearch = query;
            List<IngredientViewerViewModel> results = new List<IngredientViewerViewModel>();
            if (query == "")
            {
                results = FilteredIngredients;
            }
            else
            {

                results = FilteredIngredients.Where(x => x.Title.Name.Contains(query, StringComparison.OrdinalIgnoreCase) || x.Title.Category.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            results.Sort((l, r) => l.Title.Name.CompareTo(r.Title.Name));
            IngredientList = new ObservableCollection<IngredientViewerViewModel>(results);
            CollapseExpanders();
        }

        public ICommand ExportListUsed => new Command(() =>
        {
            ExportUsedMethod();
        });

        public void ExportUsedMethod()
        {

        }

        public void SearchEmpty()
        {
            IngredientList = new ObservableCollection<IngredientViewerViewModel>(FilteredIngredients);
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
                results = FilteredIngredients.Where(x => x.Title.Name.Contains(LastSearch, StringComparison.OrdinalIgnoreCase) || x.Title.Category.Contains(LastSearch, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            results.Sort((l, r) => l.Title.Name.CompareTo(r.Title.Name));
            IngredientList = new ObservableCollection<IngredientViewerViewModel>(results);
        }

        public ICommand Filters => new Command(() =>
        {
            FiltersMethod();
        });

        private void FiltersMethod()
        {
            ShowFilters = !ShowFilters;
        }

        public void CollapseExpanders()
        {
            foreach (IngredientViewerViewModel x in IngredientList)
            {
                x.IsExpanded = false;
            }
        }
        #endregion
        #region UsedMethods

        public void RefreshIngredientList()
        {
            List<long> IsUsedID = isUsedValuesRepository.GetUsedId();
            AllIngredients = IngredientsRepository.GetAll().ToList();
            UsedIngredients = AllIngredients.Where(x => IsUsedID.Contains(x.IsUsedValue)).ToList();
            UsedIngredientLists = new List<IngredientViewerViewModel>();
            foreach (Ingredient x in UsedIngredients)
            {
                UsedIngredientLists.Add(new IngredientViewerViewModel(x));
            }
            UsedIngredientLists.Sort((l, r) => l.Title.Name.CompareTo(r.Title.Name));
            FilteredIngredients = UsedIngredientLists;
            IngredientList = new ObservableCollection<IngredientViewerViewModel>(FilteredIngredients);
        }


        public void NotifyUIChange()
        {
            NotifyPropertyChanged(nameof(NotUsedIngredientList));
            NotifyPropertyChanged(nameof(IngredientList));
        }

        


        public void FilterList(string Filter)
        {
            LastFilter = Filter;
            switch (Filter)
            {
                case "FilterAll":
                    FilteredIngredients = UsedIngredientLists;
                    SearchDefault();
                    break;
                case "FilterEnough":
                    FilteredIngredients = UsedIngredientLists.FindAll(x => x.Title.IsEnough == true);
                    SearchDefault();
                    break;
                case "NotEnough":
                    FilteredIngredients = UsedIngredientLists.FindAll(x => x.Title.IsEnough == false);
                    SearchDefault();
                    break;
                case "FilterPriceRising":
                    List<IngredientsFormat> Default = new List<IngredientsFormat>();
                    FilteredIngredients.Clear();
                    foreach (IngredientViewerViewModel x in UsedIngredientLists)
                    {
                        if (x.Ingredients.Any(x => x.IsDefault))
                        {
                            if (x.Ingredients.FirstOrDefault(x => x.IsDefault).CostDifference > 0)
                            {
                                FilteredIngredients.Add(x);
                            }
                        }

                    }                    
                    SearchDefault();
                    break;
                case "FilterPriceLowering":
                    foreach (IngredientViewerViewModel x in UsedIngredientLists)
                    {
                        if (x.Ingredients.Any(x => x.IsDefault))
                        {
                            if (x.Ingredients.FirstOrDefault(x => x.IsDefault).CostDifference < 0)
                            {
                                FilteredIngredients.Add(x);
                            }
                        }

                    }                    
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
                    FilteredIngredients = UsedIngredientLists;
                    SearchDefault();
                    break;
                case "FilterEnough":
                    FilteredIngredients = UsedIngredientLists.FindAll(x => x.Title.IsEnough == true);
                    SearchDefault();
                    break;
                case "NotEnough":
                    FilteredIngredients = UsedIngredientLists.FindAll(x => x.Title.IsEnough == false);
                    SearchDefault();
                    break;
                case "FilterPriceRising":
                    List<IngredientsFormat> Default = new List<IngredientsFormat>();
                    FilteredIngredients.Clear();
                    foreach (IngredientViewerViewModel x in UsedIngredientLists)
                    {
                        if (x.Ingredients.Any(x => x.IsDefault))
                        {
                            if (x.Ingredients.FirstOrDefault(x => x.IsDefault).CostDifference > 0)
                            {
                                FilteredIngredients.Add(x);
                            }
                        }
                    }                   
                    SearchDefault();
                    break;
                case "FilterPriceLowering":
                    foreach (IngredientViewerViewModel x in UsedIngredientLists)
                    {
                        if (x.Ingredients.Any(x => x.IsDefault))
                        {
                            if (x.Ingredients.FirstOrDefault(x => x.IsDefault).CostDifference < 0)
                            {
                                FilteredIngredients.Add(x);
                            }
                        }
                    }           
                    SearchDefault();
                    break;
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
            UsedIngredientLists.Remove(Ig);
        }
        #endregion
        #endregion

        #region UnUsedIngredients
        #region UnUsedCommands

        public ICommand PerformUnusedSearch => new Command<string>((query) =>
        {
            SearchUnused(query);
        });            

        public void SearchUnused(string query)
        {
            LastSearchUnused = query;
            List<IngredientViewerViewModel> results = new List<IngredientViewerViewModel>();
            if (query == "")
            {
                results = NotUsedIngredientLists;
            }
            else
            {

                results = NotUsedIngredientLists.Where(x => x.Title.Name.Contains(query, StringComparison.OrdinalIgnoreCase) || x.Title.Category.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            results.Sort((l, r) => l.Title.Name.CompareTo(r.Title.Name));
            NotUsedIngredientList = new ObservableCollection<IngredientViewerViewModel>(results);
            CollapseUnusedExpanders();
        }

        private void SearchUnusedDefault()
        {
            List<IngredientViewerViewModel> results = new List<IngredientViewerViewModel>();
            if (LastSearch == "")
            {
                results = FilteredIngredients;
            }
            else
            {
                results = FilteredIngredients.Where(x => x.Title.Name.Contains(LastSearch, StringComparison.OrdinalIgnoreCase) || x.Title.Category.Contains(LastSearch, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            results.Sort((l, r) => l.Title.Name.CompareTo(r.Title.Name));
            IngredientList = new ObservableCollection<IngredientViewerViewModel>(results);
        }



        public void CollapseUnusedExpanders()
        {
            foreach (IngredientViewerViewModel x in NotUsedIngredientList)
            {
                x.IsExpanded = false;
            }
        }

        public void FilterListUnused(string Filter)
        {
            if (Filter != "Tutti")
            {
                long UsedId = isUsedValuesRepository.GetAll().FirstOrDefault(x => x.Description == Filter).Id;
                NotUsedIngredientList = new ObservableCollection<IngredientViewerViewModel>(NotUsedIngredientLists.Where(x => x.Title.IsUsedValue.Equals(UsedId)));
            }
            else
            {
                List<long> UsedId = isUsedValuesRepository.GetUsedId();
                NotUsedIngredientList = new ObservableCollection<IngredientViewerViewModel>(NotUsedIngredientLists.Where(x => !UsedId.Contains(x.Title.IsUsedValue)));
            }
        }

        public void RefreshUnusedIngredientList()
        {
            List<long> IsUsedID = isUsedValuesRepository.GetUsedId();
            AllIngredients = IngredientsRepository.GetAll().ToList();
            List<Ingredient> NotUsed = AllIngredients.Where(x => !IsUsedID.Contains(x.IsUsedValue)).ToList();
            NotUsedIngredientLists = new List<IngredientViewerViewModel>();
            foreach (Ingredient x in NotUsed)
            {
                NotUsedIngredientLists.Add(new IngredientViewerViewModel(x));
            }
            NotUsedIngredientLists.Sort((l, r) => l.Title.Name.CompareTo(r.Title.Name));
            NotUsedIngredientList = new ObservableCollection<IngredientViewerViewModel>(NotUsedIngredientLists);
        }

        #endregion

        #endregion      

        //public void RefreshIngredientList()
        //{
        //    long IsUsedID = isUsedValuesRepository.GetUsedId();
        //    AllIngredients = IngredientsRepository.GetAll().ToList();

        //    NotUsedIngredients = AllIngredients.Where(x => x.IsUsedValue != IsUsedID).ToList();
        //    UsedIngredients = AllIngredients.Where(x => x.IsUsedValue == IsUsedID).ToList();
        //    UsedIngredientLists = new List<IngredientViewerViewModel>();

        //    foreach (Ingredient y in NotUsedIngredients)
        //    {
        //        NotUsedIngredientLists.Add(new IngredientViewerViewModel(y));
        //    }
        //    foreach (Ingredient x in UsedIngredients)
        //    {
        //        UsedIngredientLists.Add(new IngredientViewerViewModel(x));
        //    }
        //    if (ShowNotUsed)
        //    {
        //        FilteredIngredients = UsedIngredientLists;
        //        FilteredIngredients.AddRange(NotUsedIngredientLists);
        //    }

        //    UsedIngredientLists.Sort((l, r) => l.Title.Name.CompareTo(r.Title.Name));
        //    FilteredIngredients = UsedIngredientLists;
        //    IngredientList = new ObservableCollection<IngredientViewerViewModel>(FilteredIngredients);
        //}

        //public void AddNotUsed()
        //{
        //    UsedIngredientLists.AddRange(NotUsedIngredientLists);
        //    FilterDefault();
        //    SearchDefault();
        //}

        //public void RemoveNotUsed()
        //{
        //    UsedIngredientLists.RemoveAll(NotUsedIngredientLists.Contains);
        //    FilterDefault();
        //    SearchDefault();
        //}
    }
}
