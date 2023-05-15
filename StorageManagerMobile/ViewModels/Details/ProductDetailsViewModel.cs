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

namespace StorageManagerMobile.ViewModels.Details
{
    public class ProductDetailsViewModel : BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly ProductsRepository ProductsRepository = new ProductsRepository(context);
        private static readonly ProductCompositionsRepository ProductCompositionsRepository = new ProductCompositionsRepository(context);
        //private static readonly IngredientsRepository IngredientsRepository = new IngredientsRepository(context);

        private Product _product;
        public Product Product 
        {
            get { return _product; } 
            set { _product = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<ProductComposition> _compositions;
        public ObservableCollection<ProductComposition> Compositions
        {
            get { return _compositions; }
            set { _compositions = value; NotifyPropertyChanged(); }
        }

        public ProductDetailsViewModel(ProductViewerViewModel vm)
        {
            Product= vm.Title;
            Compositions = vm.Compositions;
        }

        #region Commands

        public ICommand SaveModifications => new Command(() =>
        {
            SaveAll();
        });

        private void SaveModificationsMethod()
        {
            ProductsRepository.Update(Product);
            context.SaveChanges();
        }

        public ICommand Delete => new Command(() =>
        {
            DeleteMethod();
        });

        private void DeleteMethod()
        {
            foreach (ProductComposition x in Compositions)
            {
                ProductCompositionsRepository.Delete(x);
            }
            ProductsRepository.Delete(Product);
            context.SaveChanges();
        }

        #endregion

        public void SaveIngredient(Ingredient Ingredient)
        {
            ProductComposition Out = new ProductComposition();
            Out.IngredientId= Ingredient.Id;
            Out.Ingredient = Ingredient;
            Out.Product= Product;
            Out.ProductId = Product.Id;
            Compositions.Add(Out);
            ProductCompositionsRepository.Add(Out);
        }

        public void AddComposition(ProductComposition composition)
        {
            Compositions.Add(composition);
            //ProductCompositionsRepository.Add(composition);
            //context.SaveChanges();
        }

        public void SaveAll()
        {
            if (ProductsRepository.GetAll().Any(x => x.Id == Product.Id))
            {
                ProductsRepository.Update(Product);
            }
            else
            {
                ProductsRepository.Add(Product);
            }
            ProductCompositionsRepository.AddAll(Compositions.ToList());
        }
    }
}
