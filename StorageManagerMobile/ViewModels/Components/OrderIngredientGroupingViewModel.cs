using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.ViewModels.Components
{
    public class OrderIngredientGroupingViewModel : BaseViewModel
    {

        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly CategoryIngredientListsRepository CategoryIngredientListsRepository = new CategoryIngredientListsRepository(context);

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; NotifyPropertyChanged();}
        }

        private ObservableCollection<OrderItemViewModel> items = new ObservableCollection<OrderItemViewModel>();
        public ObservableCollection<OrderItemViewModel> Items
        {
            get { return items; }
            set { items = value; NotifyPropertyChanged(); }
        }

        public OrderIngredientGroupingViewModel(CategoryIngredientList list, bool Selected)
        {
            Title = list.Ingredient.Category;
            List<CategoryIngredientList> ingredients = CategoryIngredientListsRepository.GetFromCategory_Id(list.CategoryId);
            if (Selected)
            {
                foreach (CategoryIngredientList x in ingredients)
                {
                    if (x.Selected)
                    {
                        Items.Add(new OrderItemViewModel(x));
                    }                    
                }
            }
            else
            {
                foreach (CategoryIngredientList x in ingredients)
                {
                    if (!x.Selected)
                    {
                        Items.Add(new OrderItemViewModel(x));
                    }
                }
            }

        }

        public OrderIngredientGroupingViewModel()
        {

        }
    }
}
