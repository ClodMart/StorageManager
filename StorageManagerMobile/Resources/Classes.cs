using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Services;
using StorageManagerMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.Resources
{
    public class PageLink
    {
        public string Label { get; set; }
        public string Page { get; set; }

        public PageLink(string label, string page)
        {
            Label = label; Page = page;
        }
    }

    public class QuantityObject
    {
        public int QuantityNeeded { get; set; }
        public int ActualQuantity { get; set; }

        public QuantityObject(int quantityNeeded, int actualQuantity) 
        {
            QuantityNeeded = quantityNeeded;
            ActualQuantity = actualQuantity;
        }
    }

    public static class UsedValues
    {
       private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
       private static readonly IsUsedValuesRepository IsUsedRepository = new IsUsedValuesRepository(context);

       public static readonly ImmutableList<IsUsedValue> IsUsedValues = IsUsedRepository.GetAll().ToImmutableList();
    }

    public class OrderIngredient : BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly IngredientsFormatsRepository FormatRepository = new IngredientsFormatsRepository(context);
        private static readonly CategoryIngredientListsRepository CategoryIngredient = new CategoryIngredientListsRepository(context);

        private CategoryIngredientList category;
        public CategoryIngredientList Category { get { return category; } set { category = value; NotifyPropertyChanged(); NotifyChanges(); } }

        private Ingredient ingredient;
        public Ingredient Ingredient { get { return ingredient; } set { ingredient = value; NotifyPropertyChanged(); NotifyChanges(); } }

        private int numberoOfItems;
        public int NumberOfItems { get { return numberoOfItems; } set { numberoOfItems = value; NotifyPropertyChanged(); NotifyChanges(); UpdateCategoryIngredient(); } }

        private ObservableCollection<IngredientsFormat> suppliers;
        public ObservableCollection<IngredientsFormat> Suppliers { get { return suppliers; } set { suppliers = value; NotifyPropertyChanged(); NotifyChanges(); } }

        private Supplier selectedSupplier;
        public Supplier SelectedSupplier { get { return selectedSupplier; } set { selectedSupplier = value; NotifyPropertyChanged(); NotifyChanges(); UpdateCategoryIngredient(); } }

        private List<Supplier> suppliersList = new List<Supplier>();
        public List<Supplier> SuppliersList { get { return suppliersList; } set { suppliersList = value; NotifyPropertyChanged(); NotifyChanges(); } }

        public OrderIngredient(CategoryIngredientList cl)
        {
            Category= cl;
            Ingredient ing = cl.Ingredient;
            Ingredient= ing;
            NumberOfItems = Category.Quantity;
            Suppliers = new ObservableCollection<IngredientsFormat>(FormatRepository.GetFormatsFromIngredientId(Ingredient.Id));
            foreach(IngredientsFormat x in Suppliers)
            {
                SuppliersList.Add(x.Supplier);
            }
            if(Category.SelectedFormat != null)
            {
                SelectedSupplier = Category.SelectedFormat.Supplier;
            }
            NotifyChanges();
        }

        public void NotifyChanges()
        {
            NotifyPropertyChanged(nameof(Ingredient));
            NotifyPropertyChanged(nameof(NumberOfItems));
            NotifyPropertyChanged(nameof(Suppliers));
            NotifyPropertyChanged(nameof(SelectedSupplier));
            NotifyPropertyChanged(nameof(SuppliersList));
            NotifyPropertyChanged(nameof(Category));
        }

        public void UpdateCategoryIngredient()
        {
            Category = CategoryIngredient.GetById(Category.EntryId);
            Category.Quantity = NumberOfItems;
            if(SelectedSupplier != null)
            {
                IngredientsFormat selected = Suppliers.FirstOrDefault(x => x.Supplier == SelectedSupplier);
                Category.SelectedFormat = selected;
                Category.SelectedFormatId = selected.Id;
            }            
            CategoryIngredient.Update(Category);
        }

    }
}
