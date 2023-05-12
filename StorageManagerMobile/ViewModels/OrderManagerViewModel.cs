using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Services;
using StorageManagerMobile.ViewModels.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.ViewModels
{
    public class OrderManagerViewModel : BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly CategoryIngredientListsRepository OrderLists = new CategoryIngredientListsRepository(context);

        private ObservableCollection<OrderIngredientGroupingViewModel> selectedList = new ObservableCollection<OrderIngredientGroupingViewModel>();
        public ObservableCollection<OrderIngredientGroupingViewModel> SelectedList
        {
            get { return selectedList; }
            set { selectedList = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<OrderIngredientGroupingViewModel> availableList = new ObservableCollection<OrderIngredientGroupingViewModel>();
        public ObservableCollection<OrderIngredientGroupingViewModel> AvailableList
        {
            get { return availableList; }
            set { availableList = value; NotifyPropertyChanged(); }
        }

        public OrderManagerViewModel(OrderCategory Current)
        {
            List<CategoryIngredientList> list = OrderLists.GetFromCategory_Id(Current.Id);
            foreach (CategoryIngredientList item in list)
            {
                OrderIngredientGroupingViewModel CurrentCategory;
                if (!AvailableList.Select(x=>x.Title).Contains(item.Ingredient.Category) && !SelectedList.Select(x => x.Title).Contains(item.Ingredient.Category))
                {
                    AvailableList.Add(new OrderIngredientGroupingViewModel(item, false));
                    SelectedList.Add(new OrderIngredientGroupingViewModel(item, true));
                    //CurrentCategory = AvailableList.FirstOrDefault(x => x.Title == item.Ingredient.Category);
                    //AvailableList.FirstOrDefault(x => x.Title == item.Ingredient.Category).Items.Add(new OrderItemViewModel(item));
                }
                //else if(SelectedList.Select(x => x.Title).Contains(item.Ingredient.Category))
                //{
                //    //CurrentCategory = SelectedList.FirstOrDefault(x => x.Title == item.Ingredient.Category);
                //    SelectedList.FirstOrDefault(x => x.Title == item.Ingredient.Category).Items.Add(new OrderItemViewModel(item));
                //}
                //else
                //{
                //    AvailableList.Add(new OrderIngredientGroupingViewModel())
                //}
            }
            NotifyPropertyChanged(nameof (AvailableList));
            NotifyPropertyChanged(nameof(SelectedList));
        }
    }
}
