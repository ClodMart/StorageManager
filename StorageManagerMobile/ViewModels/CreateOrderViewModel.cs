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
        //public int NumberOfItems { get; set; }
        //public bool IsSelected { get; set; }
        public bool IsFocused { get; set; }
        public IngredientsFormat Item { get; set; }
        public CategoryIngredientList Listing { get; set; }
        public List<int> Numbers { get; set; }

        public OrderItem(IngredientsFormat item, CategoryIngredientList listing)
        {
            Item = item;
            //NumberOfItems = 1;
            //IsSelected = false;
            Numbers = new List<int>();
            for (int i = 1; i < 11; i++)
            {
                Numbers.Add(i);
            }
            Listing = listing;
        }
    }

    public class CreateOrderViewModel : BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly IngredientsRepository IngredientsRepository = new IngredientsRepository(context);
        private static readonly IngredientsFormatsRepository IngredientsFormatsRepository = new IngredientsFormatsRepository(context);
        private static readonly CategoryIngredientListsRepository CategoryIngredientListsRepository = new CategoryIngredientListsRepository(context);

        public bool ItemsMissed = false;

        private List<Ingredient> missed = new List<Ingredient>();
        public List<Ingredient> Missed
        {
            get { return missed; }
            set { missed = value; NotifyPropertyChanged();}
        }

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
           List<CategoryIngredientList> IngList = CategoryIngredientListsRepository.GetFromCategory_Id(Cat.Id).ToList();
            //List<IngredientsFormat> IF = IngredientsFormatsRepository.GetDefaultFormatFromCategoryIngredientList(IngList);
            foreach (CategoryIngredientList x in IngList)
            {
                IngredientsFormat y = IngredientsFormatsRepository.GetDefaultFormatFromCategoryIngredient(x);
                if (y != null)
                {
                    items.Add(new OrderItem(y,x));
                }
                else
                {
                    ItemsMissed = true;
                    Missed.Add(IngredientsRepository.GetById(x.IngredientId));
                }
            }
            Items = items;
            AvailableItems = new ObservableCollection<OrderItem>(items.Where(x=>!x.Listing.Selected));
            SelectedItems = new ObservableCollection<OrderItem>(items.Where(x => x.Listing.Selected));
        }

        public void SelectItem(OrderItem item)
        {
            AvailableItems.Remove(item);
            item.Listing.Selected= true;            
            SelectedItems.Add(item);
            CategoryIngredientListsRepository.Update(item.Listing);

        }

        public void DeselectItem(OrderItem item)
        {
            SelectedItems.Remove(item);
            item.Listing.Selected= false;
            AvailableItems.Add(item);
            CategoryIngredientListsRepository.Update(item.Listing);
        }

        public void UpdateListing(OrderItem item)
        {
            CategoryIngredientListsRepository.Update(item.Listing);
        }

    }
}
