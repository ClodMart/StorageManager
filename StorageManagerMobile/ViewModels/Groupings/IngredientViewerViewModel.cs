using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Services;
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

namespace StorageManagerMobile.ViewModels.Groupings
{

    public class IngredientViewerViewModel : BaseViewModel
    {
        #region Parameters

        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly IngredientsRepository IngredientsRepository = new IngredientsRepository(context);
        private static readonly IngredientsFormatsRepository IngredientsFormatsRepository = new IngredientsFormatsRepository(context);

        private List<IngredientsFormat> AllFormats = new List<IngredientsFormat>();

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
        private ObservableCollection<IngredientsFormat> ingredients;
        public ObservableCollection<IngredientsFormat> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; NotifyPropertyChanged(); 
            //    UpdateIngredientList();
            }
        }

        private string quantityDisplay;
        public string QuantityDisplay
        {
            get { return quantityDisplay; }
            set { quantityDisplay = value; NotifyPropertyChanged();}
        }

        #endregion

        public IngredientViewerViewModel(Ingredient title)
        {
            Title = title;
            AllFormats = IngredientsFormatsRepository.GetFormatsFromIngredientId(Title.Id);            
            AllFormats.Sort((l, r) =>
            (l.LastOrderDate ?? DateOnly.MinValue).CompareTo(r.LastOrderDate ?? DateOnly.MinValue));
            AllFormats.Reverse();
            Ingredients = new ObservableCollection<IngredientsFormat>(AllFormats);
        }

        #region Commands

        public ICommand Expand => new Command(() =>
        {
            ExpandMethod();
        });

        private void ExpandMethod()
        {
            IsExpanded = !IsExpanded;
        }

        public ICommand PerformDeletion => new Command<int>((int Id) =>
        {
            DeleteIngredienti(Id);
        });

        public void DeleteIngredienti(int Id)
        {
            IngredientsFormat Ingredient = Ingredients.FirstOrDefault(x => x.Id == Id);

            if (Ingredient != null)
            {
                Ingredients.Remove(Ingredient);
                IngredientsFormatsRepository.Delete(Ingredient);
            }
        }

        #endregion

        #region UImethods

        //private void UpdateIngredientList()
        //{
        //    Title = Ingredients.FirstOrDefault();
        //}

        public void RefreshIngredientList()
        {
            AllFormats = IngredientsFormatsRepository.GetFormatsFromIngredientId(Title.Id);
            AllFormats.Sort((l, r) =>
            (l.LastOrderDate ?? DateOnly.MinValue).CompareTo(r.LastOrderDate ?? DateOnly.MinValue));
            AllFormats.Reverse();
            Ingredients = new ObservableCollection<IngredientsFormat>(AllFormats);
        }

        private void CalcQuantityDisplay()
        {
            if (Title != null)
            {
                QuantityDisplay = Title.ActualQuantity.ToString() + "/" + Title.QuantityNeeded.ToString();
            }
        }

        #endregion

        #region DataMethods
        public void SaveIngredients()
        {
            foreach(IngredientsFormat x in Ingredients)
            {
                IngredientsFormatsRepository.Update(x);
            }
            RefreshIngredientList();
        }       

        public int DeleteIngredient(IngredientsFormat ig)
        {
            Ingredients.Remove(ig);
            IngredientsFormatsRepository.Delete(ig);
            context.SaveChanges();
            return Ingredients.Count;
        }
        #endregion
    }
}
