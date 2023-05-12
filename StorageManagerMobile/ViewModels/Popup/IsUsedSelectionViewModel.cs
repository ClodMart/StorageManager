using DBManager.Interfacce;
using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using StorageManagerMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.ViewModels.Popup
{
    public class IsUsedSelectionViewModel : BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly IsUsedValuesRepository IsUsedValuesRepository = new IsUsedValuesRepository(context);

        private List<IsUsedValue> itemList = IsUsedValuesRepository.GetAll().ToList();
        public List<IsUsedValue> ItemList { get { return itemList; } }

        private IsUsedValue selected;
        public IsUsedValue Selected
        {
            get { return selected; }
            set { OriginalSelected = selected; selected = value; NotifyPropertyChanged(); }
        }

        private IsUsedValue origimalSelected;
        public IsUsedValue OriginalSelected
        {
            get { return origimalSelected; }
            set { origimalSelected = value; NotifyPropertyChanged(); }
        }

        public IsUsedSelectionViewModel(IsUsedValue Value)
        {
            OriginalSelected = Value;
        }
    }
}
