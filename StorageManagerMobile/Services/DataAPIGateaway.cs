using CommunityToolkit.Maui.Storage;
using DevExpress.Data.Helpers;
using StorageManagerMobile.DataModels;
using StorageManagerMobile.ViewModels.Groupings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.Services
{
    public class DataAPIGateaway
    {
        public string Username = "Prins";
        public string Password = "Prins123!";

        private async Task<List<IngredientViewerViewModel>> GetUsedIngredientsAsync(string filter, string query)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri(string.Format("https://10.147.18.219:5024/api/ExportOrder/{0}/{1}/GetUsedIngredients/{2}/{3}", Username, Password, filter, query));
            try
            {
                List<IngredientViewerViewModel> OUT = new List<IngredientViewerViewModel>();
                HttpResponseMessage response = await client.GetAsync(uri);
                //string SavePath = FolderPicker.PickAsync();
                var stream = response.Content.ReadFromJsonAsync<List<IngredientViewer>>();
                foreach (var ingredient in stream.Result)
                {
                    OUT.Add(ingredient.IngredientViewerToViewmodel());
                }

                return OUT;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return null;
            }
        }

        private async Task<List<IngredientViewerViewModel>> GetUnUsedIngredientsAsync(string filter, string query)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri(string.Format("https://10.147.18.219:5024/api/ExportOrder/{0}/{1}/GetUnUsedIngredients/{2}/{3}", Username, Password, filter, query));
            try
            {
                List<IngredientViewerViewModel> OUT = new List<IngredientViewerViewModel>();
                HttpResponseMessage response = await client.GetAsync(uri);
                //string SavePath = FolderPicker.PickAsync();
                var stream = response.Content.ReadFromJsonAsync<List<IngredientViewer>>();
                foreach (var ingredient in stream.Result)
                {
                    OUT.Add(ingredient.IngredientViewerToViewmodel());
                }

                return OUT;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return null;
            }
        }
    }
}
