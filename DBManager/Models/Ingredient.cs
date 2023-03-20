using System;
using System.Collections.Generic;

namespace DBManager.Models
{
    public partial class Ingredient
    {
        public int Id { get; set; }
        public string Ingredient1 { get; set; } = null!;
        public string Category { get; set; } = null!;
        public int? IsUsed { get; set; }
        public int SupplierId { get; set; }
        public decimal SizeKg { get; set; }
        public int SizeUnits { get; set; }
        public decimal Cost { get; set; }
        public decimal OldCost { get; set; }
        public decimal? CostKg { get; set; }
        public decimal? CostUnit { get; set; }
        public int QuantityNeeded { get; set; }
        public int ActualQuantity { get; set; }
        public string? Notes { get; set; }
        public bool? IsEnough { get; set; }
        public decimal? CostDifference { get; set; }
        public DateOnly? LastOrderDateTime { get; set; }
        public bool IsDefault { get; set; }
    }
}
