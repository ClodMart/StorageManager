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
        #region Parameters
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly IngredientsRepository IngredientsRepository = new IngredientsRepository(context);
        private static readonly IngredientsFormatsRepository IngredientsFormatsRepository = new IngredientsFormatsRepository(context);

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
        private ObservableCollection<IngredientsFormat> ingredients;
        public ObservableCollection<IngredientsFormat> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; NotifyPropertyChanged(); }
        }
        #endregion

        public IngredientDetailViewModel(IngredientViewerViewModel vm)
        {
            IsUsedValues = UsedValues.IsUsedValues.ToList();
            Title = vm.Title;
            Ingredients= vm.Ingredients;            
            PickerValues = IsUsedValues.Select(x=>x.Description).ToList();
        }

        #region Commands

        public ICommand SaveModifications => new Command(() =>
        {
            SaveModificationsMethod();
        });

        private void SaveModificationsMethod()
        {
            IngredientsRepository.Update(Title);
            context.SaveChanges();
        }

        public ICommand Delete => new Command(() =>
        {
            DeleteMethod();
        });

        private void DeleteMethod()
        {
            foreach (IngredientsFormat x in Ingredients)
            {
                IngredientsFormatsRepository.Delete(x);
            }
            IngredientsRepository.Delete(Title);
            context.SaveChanges();
        }

        #endregion

        #region DataTranslation
        private void GetIsUsedSelected()
        {
            if (IsUsedValues != null) 
            {
                DefaultIsUsed = IsUsedValues.FirstOrDefault(x => x.Id == Title.IsUsedValue);
            }            
        }
        #endregion

        public void SaveIngredientFormat(IngredientsFormat Ingredient)
        {
            IngredientsFormatsRepository.Add(Ingredient);
            context.SaveChanges();
            Ingredients = new ObservableCollection<IngredientsFormat>(IngredientsFormatsRepository.GetFormatsFromIngredientId(Title.Id));
        }
    }
}
