using DBManager.Models;

namespace DataRepository.DataModel.DBDataModel
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

        public IngredientFormatTemplate()
        { }

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
    }
}
