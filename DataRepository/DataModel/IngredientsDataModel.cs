using DataRepository.Services;
using DBManager.Interfacce;
using DBManager.Models;
using System.Collections.ObjectModel;

namespace DataRepository.DataModel
{
    public class IngredientsDataModel
    {


    }

    public class IngredientViewer
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly IngredientsFormatsRepository IngredientsFormatsRepository = new IngredientsFormatsRepository(context);

        private List<IngredientsFormat> AllFormats = new List<IngredientsFormat>();
        private Ingredient Title;
        private ObservableCollection<IngredientsFormat> Ingredients;
        private string QuantityDisplay;

        public IngredientViewer(Ingredient title)
        {
            Title = title;
            AllFormats = IngredientsFormatsRepository.GetFormatsFromIngredientId(Title.Id);
            AllFormats.Sort((l, r) =>
            (l.LastOrderDate ?? DateOnly.MinValue).CompareTo(r.LastOrderDate ?? DateOnly.MinValue));
            AllFormats.Reverse();
            Ingredients = new ObservableCollection<IngredientsFormat>(AllFormats);
            QuantityDisplay = title.ActualQuantity + "/" + title.QuantityNeeded;
        }

        public void RefreshIngredientFormat()
        {
            AllFormats = IngredientsFormatsRepository.GetFormatsFromIngredientId(Title.Id);
            AllFormats.Sort((l, r) =>
            (l.LastOrderDate ?? DateOnly.MinValue).CompareTo(r.LastOrderDate ?? DateOnly.MinValue));
            AllFormats.Reverse();
            Ingredients = new ObservableCollection<IngredientsFormat>(AllFormats);
        }

    }
   
}
