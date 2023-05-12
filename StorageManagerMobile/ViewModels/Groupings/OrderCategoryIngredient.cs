using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.ViewModels.Groupings
{
    public class OrderItem
    {
        //public int NumberOfItems { get; set; }
        //public bool IsSelected { get; set; }
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly IngredientsFormatsRepository IngredientsFormatsRepository = new IngredientsFormatsRepository(context);
        public bool IsFocused { get; set; }
        public List<IngredientsFormat> Item { get; set; }
        public CategoryIngredientList Listing { get; set; }
        public List<int> Numbers { get; set; }
        public IngredientsFormat SelectedFormat { get; set; }

        public OrderItem(CategoryIngredientList listing)
        {
            Item = IngredientsFormatsRepository.GetFormatsFromIngredientId(listing.IngredientId);
            SelectedFormat = Item.FirstOrDefault(x => x.IsDefault);
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

    public class OrderCategoryIngredient : BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly IngredientsRepository IngredientsRepository = new IngredientsRepository(context);

        private string category;
        public string Category
        {
            get { return category; }
            set { category = value; NotifyPropertyChanged(); }
        }

        private OrderItem item;
        public OrderItem Item
        {
            get { return item; }
            set { item = value; NotifyPropertyChanged(); }
        }

        public OrderCategoryIngredient(CategoryIngredientList listing)
        {
            Category = IngredientsRepository.GetById(listing.IngredientId).Category;
            Item = new OrderItem(listing);
        }

        public void UpdateOrderItem(OrderItem item)
        {
            Item = item;
        }
    }
}
