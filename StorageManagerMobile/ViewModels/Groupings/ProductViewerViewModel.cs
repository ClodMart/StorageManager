using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StorageManagerMobile.ViewModels.Groupings
{
    public class ProductViewerViewModel : BaseViewModel
    {
        #region Parameters
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly ProductCompositionsRepository ProductCompositionsRepository = new ProductCompositionsRepository(context);
        private static readonly ProductsRepository ProductsRepository = new ProductsRepository(context);

        private List<ProductComposition> AllIngredients = new List<ProductComposition>();

        private bool isExpanded = false;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set { isExpanded = value; NotifyPropertyChanged(); }
        }

        private Product title;
        public Product Title
        {
            get { return title; }
            set { title = value; NotifyPropertyChanged(); }
        }
        private ObservableCollection<ProductComposition> compositions;
        public ObservableCollection<ProductComposition> Compositions
        {
            get { return compositions; }
            set
            {
                compositions = value; NotifyPropertyChanged();
                //    UpdateIngredientList();
            }
        }

        private string gainPercent;
        public string GainPercent
        {
            get { return gainPercent; }
            set { gainPercent = value; NotifyPropertyChanged();}
        }

        //private string quantityDisplay;
        //public string QuantityDisplay
        //{
        //    get { return quantityDisplay; }
        //    set { quantityDisplay = value; NotifyPropertyChanged(); }
        //}
        #endregion

        public ProductViewerViewModel(Product title)
        {
            Title = title;
            AllIngredients = ProductCompositionsRepository.GetFormatsFromProductId(Title.Id);
            AllIngredients.Sort((l, r) =>
            l.IngredientId.CompareTo(r.IngredientId));
            //AllIngredients.Reverse();
            Compositions = new ObservableCollection<ProductComposition>(AllIngredients);
            Title.ProductCost = AllIngredients.Sum(x => x.Cost);
            CalculateGainPercent();
            ProductsRepository.Update(Title);
        }

        #region Commands

        public ICommand Expand => new Command(() =>
        {
            ExpandMethod();
        });

        private void ExpandMethod()
        {
            IsExpanded = !IsExpanded;
        }

        public ICommand PerformDeletion => new Command<int>((Id) =>
        {
            DeleteIngredient(Id);
        });

        #endregion

        #region UImethods

        //private void UpdateIngredientList()
        //{
        //    Title = Ingredients.FirstOrDefault();
        //}

        public void RefreshIngredientList()
        {
            AllIngredients = ProductCompositionsRepository.GetFormatsFromProductId(Title.Id);
            AllIngredients.Sort((l, r) =>
            l.IngredientId.CompareTo(r.IngredientId));
            //AllIngredients.Reverse();
            Compositions = new ObservableCollection<ProductComposition>(AllIngredients);
            Title.ProductCost = AllIngredients.Sum(x => x.Cost);
            CalculateGainPercent();
            ProductsRepository.Update(Title);
        }

        //private void CalcQuantityDisplay()
        //{
        //    if (Title != null)
        //    {
        //        QuantityDisplay = Title.ActualQuantity.ToString() + "/" + Title.QuantityNeeded.ToString();
        //    }
        //}

        #endregion

        #region DataMethods
        public void SaveIngredients()
        {
            foreach (ProductComposition x in Compositions)
            {
                ProductCompositionsRepository.Update(x);
            }
            RefreshIngredientList();
        }

        public void DeleteIngredient(int Id)
        {
            ProductComposition composition = Compositions.FirstOrDefault(x => x.Id == Id);

            if (composition != null)
            {
                Compositions.Remove(composition);
                ProductCompositionsRepository.Delete(composition);
            }
        }

        public int DeleteComposition(ProductComposition composition)
        {
            try
            {
                if (composition != null)
                {
                    Compositions.Remove(composition);
                    ProductCompositionsRepository.Delete(composition);
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
            
        }
        #endregion

        private void CalculateGainPercent()
        {
            GainPercent = (Math.Round((decimal)((Title.ProductPrice/1.1)/Title.ProductCost/100), 2)).ToString() + "%";
        }
    }
}
