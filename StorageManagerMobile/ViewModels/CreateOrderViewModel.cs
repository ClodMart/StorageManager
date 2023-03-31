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
    public class OrderItem
    {
        public int NumberOfItems { get; set; }
        public bool IsSelected { get; set; }
        public bool IsFocused { get; set; }
        public IngredientsFormat Item { get; set; }
        public List<int> Numbers { get; set; }

        public OrderItem(IngredientsFormat item)
        {
            Item = item;
            NumberOfItems = 1;
            IsSelected = false;
            Numbers = new List<int>();
            for (int i = 1; i < 11; i++)
            {
                Numbers.Add(i);
            }
        }
    }

    public class CreateOrderViewModel : BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly IngredientsFormatsRepository IngredientsFormatsRepository = new IngredientsFormatsRepository(context);
        private static readonly CategoryIngredientListsRepository CategoryIngredientListsRepository = new CategoryIngredientListsRepository(context);

        public bool ItemsMissed = false;

        private List<OrderItem> items;
        public List<OrderItem> Items
        {
            get { return items; }
            set { items = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<OrderItem> availableItems;
        public ObservableCollection<OrderItem> AvailableItems
        {
            get { return availableItems; }
            set { availableItems = value; NotifyPropertyChanged();}
        }

        private ObservableCollection<OrderItem> selectedItems;
        public ObservableCollection<OrderItem> SelectedItems
        {
            get { return selectedItems; }
            set { selectedItems = value; NotifyPropertyChanged(); }
        }

        public CreateOrderViewModel(OrderCategory Cat)
        {
           List<OrderItem> items = new List<OrderItem>();
           List<CategoryIngredientList> IngList = CategoryIngredientListsRepository.GetFromCategory_Id(Cat.Id);
            List<IngredientsFormat> IF = IngredientsFormatsRepository.GetDefaultFormatFromCategoryIngredientList(IngList);
            foreach (IngredientsFormat x in IF)
            {
                if (x != null)
                {
                    items.Add(new OrderItem(x));
                }
                else
                {
                    ItemsMissed = true;
                }
            }
            Items = items;
            AvailableItems = new ObservableCollection<OrderItem>(items);
            SelectedItems = new ObservableCollection<OrderItem>();
        }

        public void SelectItem(OrderItem item)
        {
            AvailableItems.Remove(item);
            item.IsSelected= true;
            SelectedItems.Add(item);
            
        }

        public void DeselectItem(OrderItem item)
        {
            SelectedItems.Remove(item);
            item.IsSelected= false;
            AvailableItems.Add(item);
        }

    }
}
