using System;
using System.Collections.Generic;

namespace DBManager.Models
{
    public partial class IngredientsFormat
    {
        public long Id { get; set; }
        public long IngredientId { get; set; }
        public long SupplierId { get; set; }
        public decimal Cost { get; set; }
        public decimal? PastCost1 { get; set; }
        public decimal? PastCost2 { get; set; }
        public decimal? PastCost3 { get; set; }
        public decimal SizeKg { get; set; }
        public int SizeUnit { get; set; }
        public decimal? CostKg { get; set; }
        public decimal? CostUnit { get; set; }
        public decimal? CostDifference { get; set; }
        public DateOnly? LastOrderDate { get; set; }
        public bool IsDefault { get; set; }

        public virtual Ingredient Ingredient { get; set; } = null!;
        public virtual Supplier Supplier { get; set; } = null!;
    }
}
