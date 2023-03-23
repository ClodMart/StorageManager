using DBManager.Models;
using StorageManagerMobile.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.ViewModels.Add
{
    public class SupplierForIngredient
    {
        public Supplier Supplier { get; set; }
        public double Cost { get; set; }
        public double SizeKg { get; set; }
        public int SizeUt { get; set; }
    }

    public class AddIngredientViewModel : BaseViewModel
    {
        #region Properties
        private List<IsUsedValue> isUsedValues;
        public List<IsUsedValue> IsUsedValues
        {
            get { return isUsedValues; }
            set { isUsedValues = value; NotifyPropertyChanged(); }
        }

        private string ingredientName;
        public string IngredientName
        {
            get { return ingredientName; }
            set { ingredientName = value; NotifyPropertyChanged(); }
        }

        private string category;
        public string Category
        {
            get { return category; }
            set { category = value; NotifyPropertyChanged(); }
        }

        private IsUsedValue isUsed;
        public IsUsedValue IsUsed
        {
            get { return isUsed; } 
            set
            {isUsed = value; NotifyPropertyChanged();}
        }

        private List<IngredientsFormat> formats;
        public List<IngredientsFormat> Formats
        {
            get { return formats; }
            set { formats = value; NotifyPropertyChanged();}
        }

        private int qtNeeded;
        public int QtNeeded
        {
            get { return qtNeeded; }
            set { qtNeeded = value; NotifyPropertyChanged();}
        }

        private string notes;
        public string Notes
        {
            get { return notes; }
            set { notes = value; NotifyPropertyChanged(); }
        }

        public AddIngredientViewModel()
        {
            IsUsedValues = UsedValues.IsUsedValues.ToList();
        }
        #endregion
    }
}
