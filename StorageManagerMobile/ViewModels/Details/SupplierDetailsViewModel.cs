using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.ViewModels.Details
{
    public class SupplierDetailsViewModel : BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly SuppliersRepository SuppliersRepository = new SuppliersRepository(context);

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
                SuppliersRepository.Delete(Supplier);
            }
        }
    }
}
