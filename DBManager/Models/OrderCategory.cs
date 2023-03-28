using System;
using System.Collections.Generic;

namespace DBManager.Models
{
    public partial class OrderCategory
    {
        public OrderCategory()
        {
            CategoryIngredientLists = new HashSet<CategoryIngredientList>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<CategoryIngredientList> CategoryIngredientLists { get; set; }
    }
}
