using System;
using System.Collections.Generic;

namespace DBManager.Models
{
    public partial class ProductComposition
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long IngredientId { get; set; }
        public double Quantity { get; set; }
        public double? Cost { get; set; }

        public virtual Ingredient Ingredient { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
