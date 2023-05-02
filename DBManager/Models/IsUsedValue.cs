using System;
using System.Collections.Generic;

namespace DBManager.Models
{
    public partial class IsUsedValue
    {
        public IsUsedValue()
        {
            Ingredients = new HashSet<Ingredient>();
        }

        public long Id { get; set; }
        public string Description { get; set; } = null!;
        public bool CorrespondsToUsed { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
