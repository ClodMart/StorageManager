using DBManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.DataModels.DBDataModel
{
    public class IngredientTemplate
    {
        public long id { get; set; }
        public string name { get; set; } = null!;
        public string category { get; set; } = null!;
        public long isUsedValue { get; set; }
        public string? notes { get; set; }
        public decimal quantityNeeded { get; set; }
        public decimal actualQuantity { get; set; }
        public bool isEnough { get; set; }

        public IngredientTemplate(Ingredient ing)
        {
            id = ing.Id;
            name = ing.Name;
            category = ing.Category;
            isUsedValue = ing.IsUsedValue;
            notes = ing.Notes ?? "";
            actualQuantity = ing.ActualQuantity;
            quantityNeeded = ing.QuantityNeeded;
            isEnough = ing.IsEnough;
        }

        public Ingredient GetNewIngredient()
        {
            Ingredient OUT = new Ingredient();
            OUT.Id = id;
            OUT.Name = name;
            OUT.Category = category;
            OUT.IsUsedValue = isUsedValue;
            OUT.Notes = notes;
            OUT.ActualQuantity = actualQuantity;
            OUT.QuantityNeeded = quantityNeeded;
            OUT.IsEnough = isEnough;

            return OUT;
        }

        public string ConvertToJson()
        {
            try
            {


                return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                    NullValueHandling = NullValueHandling.Include,
                    PreserveReferencesHandling = PreserveReferencesHandling.None
                });
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
