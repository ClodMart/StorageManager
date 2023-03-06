using System;
using System.Collections.Generic;

namespace DBManager.Models
{
    public partial class IsUsedValue
    {
        public IsUsedValue()
        {
            DrinkIngredients = new HashSet<DrinkIngredient>();
            Ingredients = new HashSet<Ingredient>();
            UseMaterials = new HashSet<UseMaterial>();
        }

        public int Id { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<DrinkIngredient> DrinkIngredients { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<UseMaterial> UseMaterials { get; set; }
    }
}
