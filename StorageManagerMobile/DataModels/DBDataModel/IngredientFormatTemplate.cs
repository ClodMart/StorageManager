using DBManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.DataModels.DBDataModel
{
    public class IngredientFormatTemplate
    {
        public long id { get; set; }
        public long ingredientId { get; set; }
        public long supplierId { get; set; }
        public decimal cost { get; set; }
        public decimal? pastCost1 { get; set; }
        public decimal? pastCost2 { get; set; }
        public decimal? pastCost3 { get; set; }
        public decimal sizeKg { get; set; }
        public int sizeUnit { get; set; }
        public decimal? costKg { get; set; }
        public decimal? costUnit { get; set; }
        public decimal? costDifference { get; set; }
        public DateOnly? lastOrderDate { get; set; }
        public bool isDefault { get; set; }
        public DateOnly? lastPriceChange { get; set; }

        public IngredientFormatTemplate(IngredientsFormat Ing)
        {
            id = Ing.Id;
            ingredientId = Ing.IngredientId;
            supplierId = Ing.SupplierId;
            cost = Ing.Cost;
            pastCost1 = Ing.PastCost1;
            pastCost2 = Ing.PastCost2;
            pastCost3 = Ing.PastCost3;
            sizeKg = Ing.SizeKg;
            sizeUnit = Ing.SizeUnit;
            costKg = Ing.CostKg;
            costUnit = Ing.CostUnit;
            costDifference = Ing.CostDifference;
            lastOrderDate = Ing.LastOrderDate;
            isDefault = Ing.IsDefault;
            lastPriceChange = Ing.LastPriceChange;
        }

        public IngredientsFormat GetNewIngredientFormat()
        {
            IngredientsFormat OUT = new IngredientsFormat();
            OUT.Id = id;
            OUT.IngredientId = ingredientId;
            OUT.SupplierId = supplierId;
            OUT.Cost = cost;
            OUT.PastCost1 = pastCost1;
            OUT.PastCost2 = pastCost2;
            OUT.PastCost3 = pastCost3;
            OUT.SizeKg = sizeKg;
            OUT.SizeUnit = sizeUnit;
            OUT.CostKg = costKg;
            OUT.CostUnit = costUnit;
            OUT.CostDifference = costDifference;
            OUT.LastOrderDate = lastOrderDate;
            OUT.IsDefault = isDefault;
            OUT.LastPriceChange = lastPriceChange;
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
