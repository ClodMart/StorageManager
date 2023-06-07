using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.ViewModels.Popup
{
    public class SupplierSelectorViewModel : BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly SuppliersRepository IsUsedValuesRepository = new SuppliersRepository(context);
        private static readonly IngredientsFormatsRepository FormatsRepository = new IngredientsFormatsRepository(context);

        private List<Supplier> itemList;
        public List<Supplier> ItemList { get { return itemList; }  set { itemList = value; NotifyPropertyChanged(); } }

        private Supplier selected;
        public Supplier Selected
        {
            get { return selected; }
            set { OriginalSelected = selected; selected = value; NotifyPropertyChanged(); }
        }

        private Supplier originalSelected;
        public Supplier OriginalSelected
        {
            get { return originalSelected; }
            set { originalSelected = value; NotifyPropertyChanged(); }
        }

        public SupplierSelectorViewModel(Ingredient Ing, Supplier Selected)
        {
            List<IngredientsFormat> formats = FormatsRepository.GetFormatsFromIngredientId(Ing.Id).ToList();
            ItemList = formats.Select(x => x.Supplier).ToList();
            OriginalSelected = Selected;
        }
    }
}
