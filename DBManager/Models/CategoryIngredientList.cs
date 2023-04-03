using System;
using System.Collections.Generic;

namespace DBManager.Models
{
    public partial class CategoryIngredientList
    {
        public long EntryId { get; set; }
        public long CategoryId { get; set; }
        public long IngredientId { get; set; }
        public bool Selected { get; set; }
        public int Quantity { get; set; }

        public virtual OrderCategory Category { get; set; } = null!;
        public virtual Ingredient Ingredient { get; set; } = null!;
    }
}
