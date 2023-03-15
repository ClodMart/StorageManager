using DBManager.Models;
using StorageManagerMobile.ViewModels;
using StorageManagerMobile.ViewModels.Popup;
using StorageManagerMobile.Views.Popup;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StorageManagerMobile.CustomComponents.ViewModels
{
    public class IngredientViewerViewModel : BaseViewModel
    {
        private bool isExpanded = false;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set { isExpanded = value; NotifyPropertyChanged();}
        }

        private Ingredient title;
        public Ingredient Title 
        {
            get { return title; }
            set { title = value; NotifyPropertyChanged(); CalcQuantityDisplay(); }
        }
        private ObservableCollection<Ingredient> ingredients;
        public ObservableCollection<Ingredient> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; NotifyPropertyChanged(); UpdateIngredientList(); }
        }

        private string quantityDisplay;
        public string QuantityDisplay
        {
            get { return quantityDisplay; }
            set { quantityDisplay = value; NotifyPropertyChanged();}
        }

        public IngredientViewerViewModel(Ingredient title, List<Ingredient> ingredients)
        {
            Title =title;
            ingredients.Sort((l, r) =>
            (l.LastOrderDateTime ?? DateOnly.MinValue).CompareTo(r.LastOrderDateTime ?? DateOnly.MinValue));
            ingredients.Reverse();
            Ingredients = new ObservableCollection<Ingredient>(ingredients);
        }

        private void UpdateIngredientList()
        {
            Title = Ingredients.FirstOrDefault();
        }

        public ICommand Expand => new Command(() =>
        {
            ExpandMethod();
        });

        private void ExpandMethod()
        {
            IsExpanded = !IsExpanded;
        }

        private void CalcQuantityDisplay()
        {
            QuantityDisplay= Title.ActualQuantity.ToString() + "/" + Title.QuantityNeeded.ToString();
        }
    }
}
