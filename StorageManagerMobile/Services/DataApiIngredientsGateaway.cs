﻿using CommunityToolkit.Maui.Storage;
using DBManager.Models;
using DevExpress.Data.Helpers;
using Newtonsoft.Json;
using StorageManagerMobile.DataModels;
using StorageManagerMobile.DataModels.DBDataModel;
using StorageManagerMobile.Interface;
using StorageManagerMobile.ViewModels.Groupings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

        public async Task<List<IngredientViewerViewModel>> GetUsedIngredientsAsync(string filter, string query)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri(string.Format("https://10.147.18.219:5024/api/DataController/{0}/{1}/GetUsedIngredients/{2}/{3}", Username, Password, filter, query));
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

        public async Task<List<IngredientViewerViewModel>> GetUnUsedIngredientsAsync(string filter, string query)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri(string.Format("https://10.147.18.219:5024/api/DataController/{0}/{1}/GetUnUsedIngredients/{2}/{3}", Username, Password, filter, query));
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

        public async Task<long> AddIngredient(IngredientViewer Ingredient)
        {
            var options = new JsonWriterOptions
            {
                Indented = true
            };

            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream, options);
            HttpClient client = new HttpClient();
            Uri uri = new Uri(string.Format("https://10.147.18.219:5024/api/DataController/{0}/{1}/PostNewIngredient", Username, Password));
            IngredientTemplate Ing = new IngredientTemplate(Ingredient.Title);
            string json = Ing.ConvertToJson();
            var HTTPContent = new StringContent(json, Encoding.UTF8, "application/json");
            var HttpResponse = await client.PostAsync(uri, HTTPContent);
            long newId = 0; 
            bool succeded = long.TryParse(HttpResponse.Content.ReadAsStringAsync().Result,out newId);
            uri = new Uri(string.Format("https://10.147.18.219:5024/api/DataController/{0}/{1}/PostNewFormat", Username, Password));
            foreach (IngredientsFormat x in Ingredient.Ingredients)
            {
                x.IngredientId = newId;
                IngredientFormatTemplate New = new IngredientFormatTemplate(x);
                json = New.ConvertToJson();
                HTTPContent = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponse = await client.PostAsync(uri, HTTPContent);
                long FormatID = 0;
                succeded = long.TryParse(HttpResponse.Content.ReadAsStringAsync().Result, out FormatID);
            }

            if (succeded)
            {
                return newId;
            }
            else
            {
                return -1;
            }          
        }

        public async Task<bool> UpdateIngredient(IngredientViewer Ingredient)
        {
            var options = new JsonWriterOptions
            {
                Indented = true
            };

            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream, options);
            HttpClient client = new HttpClient();
            Uri uri = new Uri(string.Format("https://10.147.18.219:5024/api/DataController/{0}/{1}/UpdateIngredient", Username, Password));
            IngredientTemplate Ing = new IngredientTemplate(Ingredient.Title);
            string json = Ing.ConvertToJson();
            var HTTPContent = new StringContent(json, Encoding.UTF8, "application/json");
            var HttpResponse = await client.PostAsync(uri, HTTPContent);
            bool succeded = true;
            bool a = bool.TryParse(HttpResponse.Content.ReadAsStringAsync().Result, out succeded);
            uri = new Uri(string.Format("https://10.147.18.219:5024/api/DataController/{0}/{1}/UpdateFormat", Username, Password));
            foreach (IngredientsFormat x in Ingredient.Ingredients)
            {
               // x.IngredientId = newId;
                IngredientFormatTemplate New = new IngredientFormatTemplate(x);
                json = New.ConvertToJson();
                HTTPContent = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponse = await client.PostAsync(uri, HTTPContent);
                //bool FormatID = true;
                a = bool.TryParse(HttpResponse.Content.ReadAsStringAsync().Result, out succeded);
            }
            return succeded;
        }
    }
}