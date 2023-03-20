using CommunityToolkit.Maui.Core;
using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Resources;
using StorageManagerMobile.Services;
using StorageManagerMobile.ViewModels.Groupings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StorageManagerMobile.ViewModels.Details
{
    public class IngredientDetailViewModel : BaseViewModel
    {
        private static readonly GestioneMagazzinoContext context = DBService.Instance.DbContext;
        private static readonly IngredientsRepository IngredientsRepository = new IngredientsRepository(context);

        private List<IsUsedValue> isUsedValues;
        public List<IsUsedValue> IsUsedValues
        {
            get { return isUsedValues; }
            set { isUsedValues = value; NotifyPropertyChanged(); }
        }

        private IsUsedValue defaultIsUsed;
        public IsUsedValue DefaultIsUsed
        {
            get { return defaultIsUsed; }
            set { defaultIsUsed = value; NotifyPropertyChanged(); }
        }

        private List<string> pickerValues;
        public List<string> PickerValues
        {
            get { return pickerValues; }
            set { pickerValues = value; NotifyPropertyChanged();}
        }

        private Ingredient title;
        public Ingredient Title
        {
            get { return title; }
            set { title = value; GetIsUsedSelected(); NotifyPropertyChanged(); }
        }
        private ObservableCollection<Ingredient> ingredients;
        public ObservableCollection<Ingredient> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; NotifyPropertyChanged(); }
        }

        public IngredientDetailViewModel(IngredientViewerViewModel vm)
        {
            IsUsedValues = UsedValues.IsUsedValues.ToList();
            Title = vm.Title;
            Ingredients= vm.Ingredients;            
            PickerValues = IsUsedValues.Select(x=>x.Description).ToList();
        }

        private void GetIsUsedSelected()
        {
            if (IsUsedValues != null) 
            {
                DefaultIsUsed = IsUsedValues.FirstOrDefault(x => x.Id == Title.IsUsed);
            }            
        }

        public ICommand SaveModifications => new Command(() =>
        {
            SaveModificationsMethod();
        });

        private void SaveModificationsMethod()
        {
           foreach(Ingredient x in Ingredients)
            {
                x.Ingredient1 = Title.Ingredient1;
                x.Category= Title.Category;
                x.IsUsed = Title.IsUsed;
                x.QuantityNeeded= Title.QuantityNeeded;
                IngredientsRepository.Update(x);
            }  
           context.SaveChanges();
        }

        public ICommand Delete => new Command(() =>
        {
            DeleteMethod();
        });

        private void DeleteMethod()
        {
            foreach (Ingredient x in Ingredients)
            {
                IngredientsRepository.Delete(x);
            }
            context.SaveChanges();
        }
    }
}
