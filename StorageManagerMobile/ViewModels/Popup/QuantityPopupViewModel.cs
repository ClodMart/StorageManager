using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.ViewModels.Popup
{
    public class QuantityPopupViewModel : BaseViewModel
    {
        private string name;
        public string Name 
        {
            get { return name; } 
            set { name = value; NotifyPropertyChanged(); }
        }

        private string category;
        public string Category
        {
            get { return category; }
            set { category = value; NotifyPropertyChanged(); }
        }

        private decimal quantityNeeded;
        public decimal QuantityNeeded
        {
            get { return quantityNeeded; }
            set { quantityNeeded = value; NotifyPropertyChanged(); }
        }

        private decimal actualQuantity;
        public decimal ActualQuantity
        {
            get { return actualQuantity; }
            set { actualQuantity = value; NotifyPropertyChanged();}
        }

        public QuantityPopupViewModel(string name, string category, decimal quantityNeeded, decimal actualQuantity)
        {
            this.Name = name;
            this.Category = category;
            this.QuantityNeeded = quantityNeeded;
            this.ActualQuantity = actualQuantity;
        }
    }
}
