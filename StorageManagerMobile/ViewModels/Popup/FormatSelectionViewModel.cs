using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.ViewModels.Popup
{
    public class FormatSelectionViewModel : BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly IngredientsRepository IngredientsRepository = new IngredientsRepository(context);

        private List<Ingredient> ingredients;
        public List<Ingredient> Ingredients { get { return ingredients; } set { ingredients = value; NotifyPropertyChanged(); } }

        private Ingredient selectedSIngredient;
        public Ingredient SelectedIngredient
        {
            get { return selectedSIngredient; }
            set { selectedSIngredient = value; NotifyPropertyChanged(); }
        }

        private decimal cost;
        public decimal Cost
        {
            get { return cost; }
            set { cost = value; NotifyPropertyChanged(); }
        }

        private decimal sizeKg = 1;
        public decimal SizeKg
        {
            get { return sizeKg; }
            set { sizeKg = value; NotifyPropertyChanged(); }
        }

        private decimal sizeUnits = 1;
        public decimal SizeUnits
        {
            get { return sizeUnits; }
            set { sizeUnits = value; NotifyPropertyChanged(); }
        }

        public FormatSelectionViewModel()
        {
            Ingredients = IngredientsRepository.GetAll().ToList();
        }
    }
}
