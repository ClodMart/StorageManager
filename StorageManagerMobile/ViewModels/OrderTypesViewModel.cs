using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.ViewModels
{
    public class OrderTypesViewModel : BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly OrderCategoriesRepository OrderCategoriesRepository = new OrderCategoriesRepository(context);

        private ObservableCollection<OrderCategory> categories = new ObservableCollection<OrderCategory>();
        public ObservableCollection<OrderCategory> Categories { get { return categories; } set { categories = value; NotifyPropertyChanged(); } }

        public OrderTypesViewModel()
        {
            Categories= new ObservableCollection<OrderCategory>(OrderCategoriesRepository.GetAll());
        }

        public void RefreshList()
        {
            Categories = new ObservableCollection<OrderCategory>(OrderCategoriesRepository.GetAll());
        }
    }
}
