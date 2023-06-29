using DBManager.Interfacce;
using DBManager.Models;
using Newtonsoft.Json;
using StorageManagerMobile.DataModels.DBDataModel;
using StorageManagerMobile.Services;
using StorageManagerMobile.ViewModels.Groupings;
using System.Collections.ObjectModel;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace StorageManagerMobile.DataModels
{
    public class IngredientViewer
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly IngredientsFormatsRepository IngredientsFormatsRepository = new IngredientsFormatsRepository(context);

        public List<IngredientFormatTemplate> AllFormats = new List<IngredientFormatTemplate>();
        public IngredientTemplate Title;
        public List<IngredientFormatTemplate> Ingredients = new List<IngredientFormatTemplate>();
        public string QuantityDisplay;

        public IngredientViewer(IngredientTemplate title)
        {
            Title = title;
            List<IngredientsFormat> List = IngredientsFormatsRepository.GetFormatsFromIngredientId(Title.id);
            foreach(IngredientsFormat x in List)
            {
                AllFormats.Add(new IngredientFormatTemplate(x));
            }
            AllFormats.Sort((l, r) =>
            (l.lastOrderDate ?? DateOnly.MinValue).CompareTo(r.lastOrderDate ?? DateOnly.MinValue));
            AllFormats.Reverse();
            Ingredients = AllFormats;
            QuantityDisplay = title.actualQuantity + "/" + title.quantityNeeded;
        }

        public IngredientViewer()
        { }

        public IngredientViewer(List<IngredientFormatTemplate> allformat, IngredientTemplate title, List<IngredientFormatTemplate> ingredients, string quantityDisplay)
        {
            Title=title;
            AllFormats= allformat;
            Ingredients = ingredients;
            QuantityDisplay = quantityDisplay;
        }

        public IngredientViewerViewModel IngredientViewerToViewmodel()
            {
            IngredientViewerViewModel OUT = new IngredientViewerViewModel();
            OUT.Title = this.Title.GetNewIngredient();
            //OUT.Ingredients = new ObservableCollection<IngredientsFormat>(this.Ingredients);
            foreach (IngredientFormatTemplate x in AllFormats)
            {
                OUT.AllFormats.Add(x.GetNewIngredientFormat());
            }
            foreach (IngredientFormatTemplate x in Ingredients)
            {
                OUT.Ingredients.Add(x.GetNewIngredientFormat());
            }
            OUT.QuantityDisplay = this.QuantityDisplay;
            //OUT.AllFormats= this.AllFormats;
            OUT.IsExpanded = false;
            return OUT;
            }

        public static IngredientViewer IngredientViewerFromViewModel(IngredientViewerViewModel ing)
        {
            IngredientViewer OUT = new IngredientViewer();
            OUT.Title = new IngredientTemplate(ing.Title);
            foreach (IngredientsFormat x in ing.AllFormats)
            {
                OUT.AllFormats.Add(new IngredientFormatTemplate(x));
            }
            foreach (IngredientsFormat x in ing.Ingredients)
            {
                OUT.Ingredients.Add(new IngredientFormatTemplate(x));
            }
            //OUT.AllFormats = ing.AllFormats;
            //OUT.Ingredients = ing.Ingredients.ToList();
            OUT.QuantityDisplay = ing.QuantityDisplay;
            return OUT;
        }

        public string ConvertToJson()
        {
            try { 


                return JsonConvert.SerializeObject(this, Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                    NullValueHandling = NullValueHandling.Include,
                    PreserveReferencesHandling = PreserveReferencesHandling.None
                });
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

        }
    }   
}
