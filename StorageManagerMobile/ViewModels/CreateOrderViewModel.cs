using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Services;
using StorageManagerMobile.ViewModels.Groupings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.ViewModels
{
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

        private List<OrderCategoryIngredient> items;
        public List<OrderCategoryIngredient> Items
        {
            get { return items; }
            set { items = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<OrderCategoryIngredient> availableItems;
        public ObservableCollection<OrderCategoryIngredient> AvailableItems
        {
            get { return availableItems; }
            set { availableItems = value; NotifyPropertyChanged();}
        }

        private ObservableCollection<OrderCategoryIngredient> selectedItems;
        public ObservableCollection<OrderCategoryIngredient> SelectedItems
        {
            get { return selectedItems; }
            set { selectedItems = value; NotifyPropertyChanged(); }
        }

        public CreateOrderViewModel(OrderCategory Cat)
        {
           List<OrderCategoryIngredient> items = new List<OrderCategoryIngredient>();
           List<CategoryIngredientList> IngList = CategoryIngredientListsRepository.GetFromCategory_Id(Cat.Id).ToList();
            //List<IngredientsFormat> IF = IngredientsFormatsRepository.GetDefaultFormatFromCategoryIngredientList(IngList);
            foreach (CategoryIngredientList x in IngList)
            {
                IngredientsFormat y = IngredientsFormatsRepository.GetDefaultFormatFromCategoryIngredient(x);
                if (y != null)
                {
                    if(items.Any(z=>z.Category == y.Ingredient.Category))
                    {

                    }
                    items.Add(new OrderCategoryIngredient(x));
                }
                else
                {
                    ItemsMissed = true;
                    Missed.Add(IngredientsRepository.GetById(x.IngredientId));
                }
            }
            Items = items;
            AvailableItems = new ObservableCollection<OrderCategoryIngredient>(items.Where(x=>!x.Item.Listing.Selected));
            SelectedItems = new ObservableCollection<OrderCategoryIngredient>(items.Where(x => x.Item.Listing.Selected));
        }

        public void SelectItem(OrderCategoryIngredient item)
        {
            AvailableItems.Remove(item);
            item.Item.Listing.Selected= true;            
            SelectedItems.Add(item);
            CategoryIngredientListsRepository.Update(item.Item.Listing);

        }

        public void DeselectItem(OrderCategoryIngredient item)
        {
            SelectedItems.Remove(item);
            item.Item.Listing.Selected= false;
            AvailableItems.Add(item);
            CategoryIngredientListsRepository.Update(item.Item.Listing);
        }

        public void UpdateListing(OrderItem item)
        {
            CategoryIngredientListsRepository.Update(item.Listing);
        }

    }
}
