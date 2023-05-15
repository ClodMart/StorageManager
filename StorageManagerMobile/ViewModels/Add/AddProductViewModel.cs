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

namespace StorageManagerMobile.ViewModels.Add
{
    public class AddProductViewModel : BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly ProductsRepository ProductsRepository = new ProductsRepository(context);
        private static readonly ProductCompositionsRepository ProductCompositionsRepository = new ProductCompositionsRepository(context);

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

        public AddProductViewModel()
        {
            Product = new Product();
            Compositions = new ObservableCollection<ProductComposition>();
        }

        public void AddComposition(ProductComposition composition)
        {
            Compositions.Add(composition);
        }


        public ICommand SaveModifications => new Command(() =>
        {
            SaveAll();
        });

        public void SaveAll()
        {
            if(Product.ProductName != null && Product.ProductCategory != null && Product.ProductPrice != null)
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
}
