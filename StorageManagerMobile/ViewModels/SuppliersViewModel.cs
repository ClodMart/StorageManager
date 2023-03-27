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

namespace StorageManagerMobile.ViewModels
{
    public class SuppliersViewModel : BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly SuppliersRepository SuppliersRepository = new SuppliersRepository(context);

        private List<Supplier> AllSuppliers;
        private string LastSearch = "";


        private ObservableCollection<Supplier> suppliers;
        public ObservableCollection<Supplier> Suppliers
        {
            get { return suppliers; }
            set { suppliers = value; NotifyPropertyChanged(); }
        }

        public SuppliersViewModel()
        {
            AllSuppliers = SuppliersRepository.GetAll().ToList();
            Suppliers = new ObservableCollection<Supplier>(AllSuppliers);
        }

        public ICommand PerformSearch => new Command<string>((query) =>
        {
            Search(query);
        });

        public void Search(string query)
        {
            LastSearch = query;
            List<Supplier> results = new List<Supplier>();
            if (query == "")
            {
                results = AllSuppliers;
            }
            else
            {
                foreach (Supplier Sup in AllSuppliers)
                {
                    if (Sup.SupplierName.Contains(query, StringComparison.OrdinalIgnoreCase) || Sup.Id.ToString().Contains(query, StringComparison.OrdinalIgnoreCase))
                    {
                        results.Add(Sup);
                    }
                }
            }

            results.Sort((l, r) => l.Id.CompareTo(r.Id));
            Suppliers = new ObservableCollection<Supplier>(results);
        }

        private void SearchDefault()
        {
            string query= LastSearch;
            List<Supplier> results = new List<Supplier>();
            if (query == "")
            {
                results = AllSuppliers;
            }
            else
            {
                foreach (Supplier Sup in AllSuppliers)
                {
                    if (Sup.SupplierName.Contains(query, StringComparison.OrdinalIgnoreCase) || Sup.Id.ToString().Contains(query, StringComparison.OrdinalIgnoreCase))
                    {
                        results.Add(Sup);
                    }
                }
            }

            results.Sort((l, r) => l.Id.CompareTo(r.Id));
            Suppliers = new ObservableCollection<Supplier>(results);
        }

        public void RefreshList()
        {
            AllSuppliers = SuppliersRepository.GetAll().ToList();
            Suppliers = new ObservableCollection<Supplier>(AllSuppliers);
            SearchDefault();
        }
    }
}
