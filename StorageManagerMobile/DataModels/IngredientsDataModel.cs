using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Services;
using StorageManagerMobile.ViewModels.Groupings;
using System.Collections.ObjectModel;

namespace StorageManagerMobile.DataModels
{
    public class IngredientViewer
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly IngredientsFormatsRepository IngredientsFormatsRepository = new IngredientsFormatsRepository(context);

        private List<IngredientsFormat> AllFormats = new List<IngredientsFormat>();
        public Ingredient Title;
        public List<IngredientsFormat> Ingredients;
        private string QuantityDisplay;

        public IngredientViewer(Ingredient title)
        {
            Title = title;
            AllFormats = IngredientsFormatsRepository.GetFormatsFromIngredientId(Title.Id);
            AllFormats.Sort((l, r) =>
            (l.LastOrderDate ?? DateOnly.MinValue).CompareTo(r.LastOrderDate ?? DateOnly.MinValue));
            AllFormats.Reverse();
            Ingredients = AllFormats;
            QuantityDisplay = title.ActualQuantity + "/" + title.QuantityNeeded;
        }

        public IngredientViewer()
        { }

            public IngredientViewerViewModel IngredientViewerToViewmodel()
        {
            IngredientViewerViewModel OUT = new IngredientViewerViewModel();
            OUT.Title = this.Title;
            OUT.Ingredients = new ObservableCollection<IngredientsFormat>(this.Ingredients);
            OUT.QuantityDisplay = this.QuantityDisplay;
            OUT.AllFormats= this.AllFormats;
            OUT.IsExpanded = false;
            return OUT;
        }

        public IngredientViewer IngredientViewerFromViewModel(IngredientViewerViewModel ing)
        {
            IngredientViewer OUT = new IngredientViewer();
            OUT.Title = ing.Title;
            OUT.AllFormats = ing.AllFormats;
            OUT.Ingredients = ing.Ingredients.ToList();
            OUT.QuantityDisplay = ing.QuantityDisplay;
            return OUT;
        }
    }   
}
