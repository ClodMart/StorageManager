using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Resources;
using StorageManagerMobile.Services;
using StorageManagerMobile.ViewModels.Groupings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly IngredientsRepository IngredientsRepository = new IngredientsRepository(context);
        private static readonly IngredientsFormatsRepository IngredientsFormatsRepository = new IngredientsFormatsRepository(context);

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
            { isUsed = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<IngredientsFormat> formats = new ObservableCollection<IngredientsFormat>();
        public ObservableCollection<IngredientsFormat> Formats
        {
            get { return formats; }
            set { formats = value; NotifyPropertyChanged(); }
        }

        private int qtNeeded;
        public int QtNeeded
        {
            get { return qtNeeded; }
            set { qtNeeded = value; NotifyPropertyChanged(); }
        }

        private string notes;
        public string Notes
        {
            get { return notes; }
            set { notes = value; NotifyPropertyChanged(); }
        }

        public ICommand RemFormat => new Command<long>((Id) =>
        {
            RemIngredientFormat(Id);
        });

        private void RemIngredientFormat(long Id)
        {
            Formats.Remove(Formats.FirstOrDefault(x => x.Id == Id));
        }

        public AddIngredientViewModel()
        {
            IsUsedValues = UsedValues.IsUsedValues.ToList();
            //Formats.Add(new IngredientsFormat());
        }
        #endregion

        public long CheckIfExists()
        {
            if (IngredientsRepository.Exists(IngredientName))
            {
                return IngredientsRepository.GetByName(IngredientName).Id;
            }
            else
            {
                return 0;
            }
        }

        public void SaveFormats(long IngredientId)
        {
            foreach (IngredientsFormat x in Formats)
            {
                x.IngredientId = IngredientId;
            }
            IngredientsFormatsRepository.AddAll(Formats.ToList());
        }

        public long SaveIngredient()
        {
            Ingredient ing = new Ingredient();
            ing.Name = IngredientName;
            ing.Category = Category;
            ing.QuantityNeeded = QtNeeded;
            ing.IsUsedValue = IsUsed.Id;
            ing.Notes = Notes;

            return IngredientsRepository.Add(ing);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public IngredientViewerViewModel GetIngredientViewer()
        {
            IngredientViewerViewModel OUT = new IngredientViewerViewModel();
            OUT.Title = new Ingredient();
            OUT.Title.Name = IngredientName;
            OUT.Title.Category = Category;
            OUT.Title.QuantityNeeded = QtNeeded;
            OUT.Title.IsUsedValue = IsUsed.Id;
            OUT.Title.Notes = Notes;

            OUT.Ingredients = Formats;
            OUT.AllFormats = Formats.ToList();

            return OUT;
        }
    }
}
