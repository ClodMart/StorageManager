using DBManager.Interfacce;
using DBManager.Models;
using Newtonsoft.Json;
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

        public static IngredientViewer IngredientViewerFromViewModel(IngredientViewerViewModel ing)
        {
            IngredientViewer OUT = new IngredientViewer();
            OUT.Title = ing.Title;
            OUT.AllFormats = ing.AllFormats;
            OUT.Ingredients = ing.Ingredients.ToList();
            OUT.QuantityDisplay = ing.QuantityDisplay;
            return OUT;
        }

        public string ConvertToJson()
        {
            try
            {
                //StringBuilder sb = new StringBuilder();
                //sb.Append("{\"Title\":{");
                //sb.Append(JsonConvert.SerializeObject(Title, Formatting.None, new JsonSerializerSettings
                //{
                //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                //    NullValueHandling = NullValueHandling.Include,
                //    PreserveReferencesHandling = PreserveReferencesHandling.None
                //})+"}");
                //sb.Append(",\"Ingredients\":[");
                //foreach (IngredientsFormat x in Ingredients)
                //{
                //    sb.Append("{" + JsonConvert.SerializeObject(x, Formatting.None, new JsonSerializerSettings
                //    {
                //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                //        NullValueHandling = NullValueHandling.Include,
                //        PreserveReferencesHandling = PreserveReferencesHandling.None
                //    }) + "},");
                //}
                //sb.Remove(sb.Length - 1, 1);
                //sb.Append("], \"QuantityDisplay\":" + QuantityDisplay);
                //sb.Append(",\"AllFormats\"" +":[");
                //foreach (IngredientsFormat x in AllFormats)
                //{
                //    sb.Append("{" + JsonConvert.SerializeObject(x, Formatting.None, new JsonSerializerSettings
                //    {
                //        ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                //        NullValueHandling = NullValueHandling.Include,
                //        PreserveReferencesHandling = PreserveReferencesHandling.None
                //    }) + "},");
                //}

                //return sb.ToString();

                return JsonConvert.SerializeObject(this, Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                    NullValueHandling = NullValueHandling.Include,
                    PreserveReferencesHandling = PreserveReferencesHandling.Arrays
                });
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

        }
    }   
}
