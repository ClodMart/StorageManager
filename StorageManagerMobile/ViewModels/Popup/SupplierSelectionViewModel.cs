using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.ViewModels.Popup
{
    public class SupplierSelectionViewModel : BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly SuppliersRepository SuppliersRepository = new SuppliersRepository(context);
        private Ingredient Ingredient { get; set; }

        private List<Supplier> suppliers;
        public List<Supplier> Suppliers
        {
            get { return suppliers; }
            set { suppliers = value; NotifyPropertyChanged(); }
        }

        private Supplier selectedSupplier;
        public Supplier SelectedSupplier
        {
            get { return selectedSupplier; } 
            set{selectedSupplier = value; NotifyPropertyChanged();}
        }

        private decimal cost;
        public decimal Cost
        {
            get { return cost; }
            set { cost = value; NotifyPropertyChanged(); }
        }

        private decimal sizeKg;
        public decimal SizeKg
        {
            get { return sizeKg; }
            set { sizeKg = value; NotifyPropertyChanged(); }
        }

        private decimal sizeUnits;
        public decimal SizeUnits
        {
            get { return sizeUnits; }
            set { sizeUnits = value; NotifyPropertyChanged(); }
        }

        public SupplierSelectionViewModel (Ingredient ing)
        {
            Ingredient = ing;
            Suppliers = SuppliersRepository.GetAll().ToList();
        }

        public SupplierSelectionViewModel()
        {
            Suppliers = SuppliersRepository.GetAll().ToList();
        }


    }
}
