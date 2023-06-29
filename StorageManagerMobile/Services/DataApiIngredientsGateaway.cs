using CommunityToolkit.Maui.Storage;
using DBManager.Models;
using DevExpress.Data.Helpers;
using Newtonsoft.Json;
using StorageManagerMobile.DataModels;
using StorageManagerMobile.DataModels.DBDataModel;
using StorageManagerMobile.Interface;
using StorageManagerMobile.ViewModels.Groupings;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StorageManagerMobile.Services
{
    public class DataApiIngredientsGateaway : IDataIngredientsReopository
    {
        public string Username = "Prins";
        public string Password = "Prins123!";

        #region Get
        public async Task<List<IngredientViewerViewModel>> GetUsedIngredientsAsync(string filter, string query)
        {
            if(query == "")
            {
                query = "NoQuery";
            }
            HttpClient client = new HttpClient();
            Uri uri = new Uri(string.Format("https://10.147.18.219:5024/api/DataController/{0}/{1}/GetUsedIngredients/{2}/{3}", Username, Password, filter, query));
            try
            {
                List<IngredientViewerViewModel> OUT = new List<IngredientViewerViewModel>();
                var response = await client.GetAsync(uri).ConfigureAwait(false);
                var stringResponse = await response.Content.ReadAsStringAsync();
                List<IngredientViewer> stream = JsonConvert.DeserializeObject<List<IngredientViewer>>(stringResponse);
                if (stream == null)
                {
                    return OUT;
                }
                foreach (IngredientViewer ingredient in stream)
                {
                    OUT.Add(ingredient.IngredientViewerToViewmodel());
                }

                return OUT;
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
                return null;
            }
        }

        public async Task<List<IngredientViewerViewModel>> GetUnUsedIngredientsAsync(string filter, string query)
        {
            if (query == "")
            {
                query = "NoQuery";
            }
            HttpClient client = new HttpClient();
            Uri uri = new Uri(string.Format("https://10.147.18.219:5024/api/DataController/{0}/{1}/GetUnUsedIngredients/{2}/{3}", Username, Password, filter, query));
            try
            {
                List<IngredientViewerViewModel> OUT = new List<IngredientViewerViewModel>();
                var response = await client.GetAsync(uri).ConfigureAwait(false);
                var stringResponse = await response.Content.ReadAsStringAsync();
                List<IngredientViewer> stream = JsonConvert.DeserializeObject<List<IngredientViewer>>(stringResponse);
                if(stream == null)
                {
                    return OUT;
                }
                foreach (IngredientViewer ingredient in stream)
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

        public async Task<List<IngredientsFormat>> GetFormatsFromIngredientIdAsync(long id)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri(string.Format("https://10.147.18.219:5024/api/DataController/{0}/{1}/GetFormatsFromIngredientId/{2}", Username, Password, id));
            try
            {
                List<IngredientsFormat> OUT = new List<IngredientsFormat>();
                var response = await client.GetAsync(uri).ConfigureAwait(false);
                var stringResponse = await response.Content.ReadAsStringAsync();
                List<IngredientFormatTemplate> stream = JsonConvert.DeserializeObject<List<IngredientFormatTemplate>>(stringResponse);
                if (stream == null)
                {
                    return OUT;
                }
                foreach (IngredientFormatTemplate format in stream)
                {
                    OUT.Add(format.GetNewIngredientFormat());
                }
                return OUT;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return null;
            }
        }

        public async Task<IngredientTemplate> GetIngredientByName(string name)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri(string.Format("https://10.147.18.219:5024/api/DataController/{0}/{1}/GetIngredientByName/{2}", Username, Password, name));
            try
            {
                var response = await client.GetAsync(uri).ConfigureAwait(false);
                var stringResponse = await response.Content.ReadAsStringAsync();
                IngredientTemplate stream = JsonConvert.DeserializeObject<IngredientTemplate>(stringResponse);
                if (stream == null)
                {
                    return null;
                }
                else
                {
                    return stream;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return null;
            }
        }
            #endregion

            #region AddMrthods
            public async Task<long> AddIngredientViewer(IngredientViewer Ingredient)
        {
            var options = new JsonWriterOptions
            {
                Indented = true
            };
            IngredientTemplate Ing = Ingredient.Title;
            long NewId = await AddIngredient(Ing);
            foreach (IngredientFormatTemplate x in Ingredient.Ingredients)
            {
                x.ingredientId= NewId;
                await AddFormat(x);
            }

            return NewId;
        }

        public async Task<long> AddIngredient(IngredientTemplate Ing)
        {
            var options = new JsonWriterOptions
            {
                Indented = true
            };

            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream, options);
            HttpClient client = new HttpClient();
            Uri uri = new Uri(string.Format("https://10.147.18.219:5024/api/DataController/{0}/{1}/PostNewIngredient", Username, Password));
            string json = Ing.ConvertToJson();
            var HTTPContent = new StringContent(json, Encoding.UTF8, "application/json");
            var HttpResponse = await client.PostAsync(uri, HTTPContent);
            long newId = 0;
            bool succeded = long.TryParse(HttpResponse.Content.ReadAsStringAsync().Result, out newId);
            if (succeded)
            {
                return newId;
            }
            else
            {
                return -1;
            }
        }

        public async Task<long> AddFormat(IngredientFormatTemplate format)
        {
            var options = new JsonWriterOptions
            {
                Indented = true
            };

            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream, options);
            HttpClient client = new HttpClient();
            Uri uri = new Uri(string.Format("https://10.147.18.219:5024/api/DataController/{0}/{1}/PostNewFormat", Username, Password));
            string json = format.ConvertToJson();
            var HTTPContent = new StringContent(json, Encoding.UTF8, "application/json");
            var HttpResponse = await client.PostAsync(uri, HTTPContent);
            long FormatID = 0;
            bool succeded = long.TryParse(HttpResponse.Content.ReadAsStringAsync().Result, out FormatID);
            return FormatID;
        }
            #endregion

            #region Updates
            public async Task<bool> UpdateIngredientViewer(IngredientViewer Ingredient)
        {
            IngredientTemplate Ing = Ingredient.Title;
            bool succeded = UpdateIngredient(Ing).Result;
            foreach (IngredientFormatTemplate x in Ingredient.Ingredients)
            {
                succeded = UpdateFormat(x).Result;
            }
            return succeded;
        }

        public async Task<bool> UpdateFormat(IngredientFormatTemplate format)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri(string.Format("https://10.147.18.219:5024/api/DataController/{0}/{1}/UpdateFormat", Username, Password));
            string json = format.ConvertToJson();
            var HTTPContent = new StringContent(json, Encoding.UTF8, "application/json");
            var HttpResponse = await client.PostAsync(uri, HTTPContent);
            bool succeded = true;
            bool.TryParse(HttpResponse.Content.ReadAsStringAsync().Result, out succeded);
            return succeded;
        }

        public async Task<bool> UpdateIngredient(IngredientTemplate Ingredient)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri(string.Format("https://10.147.18.219:5024/api/DataController/{0}/{1}/UpdateIngredient", Username, Password));
            string json = Ingredient.ConvertToJson();
            var HTTPContent = new StringContent(json, Encoding.UTF8, "application/json");
            var HttpResponse = await client.PostAsync(uri, HTTPContent);
            bool succeded = true;
            bool.TryParse(HttpResponse.Content.ReadAsStringAsync().Result, out succeded);
            return succeded;
        }
        #endregion

        #region Delete

        public async Task<bool> DeleteIngredient(IngredientTemplate Ingredient)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri(string.Format("https://10.147.18.219:5024/api/DataController/{0}/{1}/DeleteIngredient/{2}", Username, Password, Ingredient.id));
            string json = Ingredient.ConvertToJson();
            var HTTPContent = new StringContent(json, Encoding.UTF8, "application/json");
            var HttpResponse = await client.PostAsync(uri, HTTPContent);
            bool succeded = true;
            bool.TryParse(HttpResponse.Content.ReadAsStringAsync().Result, out succeded);
            return succeded;
        }

        public async Task<bool> DeleteFormat(IngredientFormatTemplate Format)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri(string.Format("https://10.147.18.219:5024/api/DataController/{0}/{1}/DeleteFormat/{2}", Username, Password, Format.id));
            string json = Format.ConvertToJson();
            var HTTPContent = new StringContent(json, Encoding.UTF8, "application/json");
            var HttpResponse = await client.PostAsync(uri, HTTPContent);
            bool succeded = true;
            bool.TryParse(HttpResponse.Content.ReadAsStringAsync().Result, out succeded);
            return succeded;
        }
        #endregion
    }
}
