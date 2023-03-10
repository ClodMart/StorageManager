using DBManager.Models;
using StorageManagerMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            set { isExpanded = value; NotifyPropertyChanged(); }
        }

        private double height = 80;
        public double Height
        {
            get { return height; }
            set { height = value; NotifyPropertyChanged(); }
        }

        private Ingredient title;
        public Ingredient Title 
        {
            get { return title; }
            set { title = value; NotifyPropertyChanged(); }
        }
        private ObservableCollection<Ingredient> ingredients;
        public ObservableCollection<Ingredient> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; NotifyPropertyChanged(); }
        }

        public IngredientViewerViewModel(Ingredient title, List<Ingredient> ingredients)
        {
            Title=title;
            Ingredients = new ObservableCollection<Ingredient>(ingredients);
        }

        public ICommand Expand => new Command(() =>
        {
            ExpandMethod();
        });

        private void ExpandMethod()
        {
            IsExpanded = !IsExpanded;
        }
    }
}
