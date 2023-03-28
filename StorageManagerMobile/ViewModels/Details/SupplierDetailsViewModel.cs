using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.ViewModels.Details
{
    public class SupplierDetailsViewModel : BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly SuppliersRepository SuppliersRepository = new SuppliersRepository(context);
        private static readonly IngredientsFormatsRepository IngredientsFormatsRepository = new IngredientsFormatsRepository(context);

        private Supplier supplier;
        public Supplier Supplier
        {
            get { return supplier; }
            set { supplier = value; NotifyPropertyChanged(); }
        }

        public SupplierDetailsViewModel(Supplier Sup)
        {
            Supplier = Sup;
        }

        public void SaveSupplier()
        {
            if (Supplier != null)
            {
                SuppliersRepository.Update(Supplier);
            }
        }

        public void DeleteSupplier()
        {
            if (Supplier != null)
            {
                SuppliersRepository.DeleteCascadeSupplier(Supplier);
            }
        }

        public void GetSupplierFromDB()
        {
            long Id = Supplier.Id;
            Supplier = new Supplier();

            Supplier = SuppliersRepository.GetById(Id);
        }

        public bool DeleteFormat(IngredientsFormat IF)
        {
            try
            {
                IngredientsFormatsRepository.Delete(IF);
                GetSupplierFromDB();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void SaveIngredientFormat(IngredientsFormat Ingredient)
        {
            IngredientsFormatsRepository.Add(Ingredient);
            context.SaveChanges();
            GetSupplierFromDB();
        }
    }
}
