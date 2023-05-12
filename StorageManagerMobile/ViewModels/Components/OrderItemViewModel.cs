using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.ViewModels.Components
{
    public class OrderItemViewModel : BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly SuppliersRepository Suppliers = new SuppliersRepository(context);
        private static readonly IngredientsFormatsRepository Formats = new IngredientsFormatsRepository(context);
        private static readonly CategoryIngredientListsRepository CategoryIngredientListsRepository = new CategoryIngredientListsRepository(context);

        private CategoryIngredientList ingredient;
        public CategoryIngredientList Ingredient
        {
            get { return ingredient; }
            set { ingredient = value; NotifyPropertyChanged(); }
        }

        private List<Supplier> supplierList;
        public List<Supplier> SupplierList
        {
            get { return supplierList; }
            set { supplierList = value; NotifyPropertyChanged(); }
        }

        public OrderItemViewModel(CategoryIngredientList ingredient)
        {
            Ingredient = ingredient;
            List<long> formats = Formats.GetFormatsFromIngredientId(ingredient.IngredientId).Select(x=>x.SupplierId).ToList();
            SupplierList = Suppliers.GetAll().Where(x=>formats.Contains(x.Id)).ToList();
        }

        public void UpdateListing()
        {
            CategoryIngredientListsRepository.Update(Ingredient);
        }
    }
}
