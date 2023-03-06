using System;
using System.Collections.Generic;

namespace DBManager.Models
{
    public partial class UseMaterial
    {
        public int Id { get; set; }
        public string MaterialName { get; set; } = null!;
        public string Category { get; set; } = null!;
        public int SupplierId { get; set; }
        public int IsUsed { get; set; }
        public decimal? SizeUnits { get; set; }
        public decimal Cost { get; set; }
        public decimal? CostUnit { get; set; }
        public int QuantityNeeded { get; set; }
        public string? Notes { get; set; }

        public virtual IsUsedValue IsUsedNavigation { get; set; } = null!;
        public virtual Supplier Supplier { get; set; } = null!;
    }
}
