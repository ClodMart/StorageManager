﻿using System;
using System.Collections.Generic;

namespace DBManager.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            IngredientsFormats = new HashSet<IngredientsFormat>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public long IsUsedValue { get; set; }
        public string? Notes { get; set; }
        public decimal QuantityNeeded { get; set; }
        public decimal ActualQuantity { get; set; }
        public bool IsEnough { get; set; }

        public virtual IsUsedValue IsUsedValueNavigation { get; set; } = null!;
        public virtual ICollection<IngredientsFormat> IngredientsFormats { get; set; }
    }
}
