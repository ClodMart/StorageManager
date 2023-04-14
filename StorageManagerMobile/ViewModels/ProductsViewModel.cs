using DBManager.Interfacce;
using DBManager.Models;
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
    public class ProductsViewModel : BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private ProductsRepository productsRepository = new ProductsRepository(context);

        private string LastSearch = "";
        private string LastFilter = "FilterAll";

        private List<Product> AllProducts = new List<Product>();
        private List<ProductViewerViewModel> FullProducts { get; set; }
        private List<ProductViewerViewModel> FilteredProducts { get; set; }


        private ObservableCollection<ProductViewerViewModel> productList;
        public ObservableCollection<ProductViewerViewModel> ProductList
        {
            get { return productList; }
            set { productList = value; NotifyPropertyChanged(); }
        }

        private bool showFilters = false;
        public bool ShowFilters
        {
            get { return showFilters; }
            set { showFilters = value; NotifyPropertyChanged(); }
        }

        public ProductsViewModel()
        {
            AllProducts = productsRepository.GetAll().ToList();
            AllProducts.Sort((l, r) => l.ProductName.CompareTo(r.ProductName));
            FullProducts = new List<ProductViewerViewModel>();

            foreach (Product x in AllProducts)
            {
                FullProducts.Add(new ProductViewerViewModel(x));
            }
            FilteredProducts = FullProducts;
            ProductList = new ObservableCollection<ProductViewerViewModel>(FullProducts);
        }
        #region Icommands

        public ICommand PerformSearch => new Command<string>((query) =>
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
            List<ProductViewerViewModel> results = new List<ProductViewerViewModel>();
            if (query == "")
            {
                results = FilteredProducts;
            }
            else
            {
                foreach (ProductViewerViewModel Ingredient in FilteredProducts)
                {
                    if (Ingredient.Title.ProductName.Contains(query, StringComparison.OrdinalIgnoreCase) || Ingredient.Title.ProductCategory.Contains(query, StringComparison.OrdinalIgnoreCase))
                    {
                        results.Add(Ingredient);
                    }
                }
            }

            results.Sort((l, r) => l.Title.ProductName.CompareTo(r.Title.ProductName));
            ProductList = new ObservableCollection<ProductViewerViewModel>(results);
            CollapseExpanders();
        }

        private void SearchDefault()
        {
            List<ProductViewerViewModel> results = new List<ProductViewerViewModel>();
            if (LastSearch == "")
            {
                results = FilteredProducts;
            }
            else
            {
                foreach (ProductViewerViewModel Ingredient in FilteredProducts)
                {
                    if (Ingredient.Title.ProductName.Contains(LastSearch, StringComparison.OrdinalIgnoreCase) || Ingredient.Title.ProductCategory.Contains(LastSearch, StringComparison.OrdinalIgnoreCase))
                    {
                        results.Add(Ingredient);
                    }
                }
            }
            results.Sort((l, r) => l.Title.ProductName.CompareTo(r.Title.ProductName));
            ProductList = new ObservableCollection<ProductViewerViewModel>(results);
        }

        public void CollapseExpanders()
        {
            foreach (ProductViewerViewModel x in ProductList)
            {
                x.IsExpanded = false;
            }
        }

        #endregion
        //public void FilterList(string Filter)
        //{
        //    LastFilter = Filter;
        //    switch (Filter)
        //    {
        //        case "FilterAll":
        //            FilteredProducts = FullProducts;
        //            SearchDefault();
        //            break;
        //        case "FilterEnough":
        //            FilteredProducts = FullProducts.FindAll(x => x.Title.IsEnough == true);
        //            SearchDefault();
        //            break;
        //        case "NotEnough":
        //            FilteredProducts = FullProducts.FindAll(x => x.Title.IsEnough == false);
        //            SearchDefault();
        //            break;
        //        case "FilterPriceRising":
        //            List<IngredientsFormat> Default = new List<IngredientsFormat>();
        //            FilteredProducts.Clear();
        //            foreach (ProductViewerViewModel x in FullProducts)
        //            {
        //                if (x.Compositions.Any(x => x.IsDefault))
        //                {
        //                    if (x.Compositions.FirstOrDefault(x => x.IsDefault).CostDifference > 0)
        //                    {
        //                        FilteredProducts.Add(x);
        //                    }
        //                }

        //            }
        //            //FilteredProducts = FullIngredients.FindAll(x => x.Ingredients.FirstOrDefault(x=>x.IsDefault).CostDifference > 0);
        //            SearchDefault();
        //            break;
        //        case "FilterPriceLowering":
        //            foreach (ProductViewerViewModel x in FullProducts)
        //            {
        //                if (x.Compositions.Any(x => x.IsDefault))
        //                {
        //                    if (x.Compositions.FirstOrDefault(x => x.IsDefault).CostDifference < 0)
        //                    {
        //                        FilteredProducts.Add(x);
        //                    }
        //                }

        //            }
        //            //FilteredProducts = FullIngredients.FindAll(x => x.Ingredients.FirstOrDefault(x => x.IsDefault).CostDifference < 0);
        //            SearchDefault();
        //            break;
        //    }
        //    CollapseExpanders();
        //}

        //private void FilterDefault()
        //{
        //    switch (LastFilter)
        //    {
        //        case "FilterAll":
        //            FilteredProducts = FullProducts;
        //            SearchDefault();
        //            break;
        //        case "FilterEnough":
        //            FilteredProducts = FullProducts.FindAll(x => x.Title.IsEnough == true);
        //            SearchDefault();
        //            break;
        //        case "NotEnough":
        //            FilteredProducts = FullProducts.FindAll(x => x.Title.IsEnough == false);
        //            SearchDefault();
        //            break;
        //        case "FilterPriceRising":
        //            List<IngredientsFormat> Default = new List<IngredientsFormat>();
        //            FilteredProducts.Clear();
        //            foreach (ProductViewerViewModel x in FullProducts)
        //            {
        //                if (x.Compositions.Any(x => x.IsDefault))
        //                {
        //                    if (x.Compositions.FirstOrDefault(x => x.IsDefault).CostDifference > 0)
        //                    {
        //                        FilteredProducts.Add(x);
        //                    }
        //                }

        //            }
        //            //FilteredProducts = FullIngredients.FindAll(x => x.Ingredients.FirstOrDefault(x=>x.IsDefault).CostDifference > 0);
        //            SearchDefault();
        //            break;
        //        case "FilterPriceLowering":
        //            foreach (ProductViewerViewModel x in FullProducts)
        //            {
        //                if (x.Compositions.Any(x => x.IsDefault))
        //                {
        //                    if (x.Compositions.FirstOrDefault(x => x.IsDefault).CostDifference < 0)
        //                    {
        //                        FilteredProducts.Add(x);
        //                    }
        //                }

        //            }
        //            //FilteredProducts = FullIngredients.FindAll(x => x.Ingredients.FirstOrDefault(x => x.IsDefault).CostDifference < 0);
        //            SearchDefault();
        //            break;
        //    }

        //}

        public void RefreshIngredientList()
        {
            List<Product> ToDelete = new List<Product>();
            List<Product> List = productsRepository.GetAll().ToList();
            List.Sort((l, r) => l.ProductName.CompareTo(r.ProductName));
            FullProducts.Clear();

            foreach (Product x in List)
            {
                ProductViewerViewModel VM = new ProductViewerViewModel(x);
                if (VM.Compositions.Count != 0)
                {
                    FullProducts.Add(VM);
                }
                else
                {
                    ToDelete.Add(x);
                }
            }
            //FilteredProducts = FullIngredients;
            //IngredientList = new ObservableCollection<ProductViewerViewModel>(FullIngredients);   

            //FilterDefault();
            foreach (Product x in ToDelete)
            {
                productsRepository.Delete(x);
            }
        }

        public void UpdateIngredientList(Product In)
        {
            productsRepository.Update(In);
            context.SaveChanges();
        }

        public void DeleteIngredients(ProductViewerViewModel Ig)
        {
            ProductList.Remove(Ig);
            FilteredProducts.Remove(Ig);
            FullProducts.Remove(Ig);
        }
    }
}
