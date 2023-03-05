using System;
using System.Collections.Generic;

namespace DBManager.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            DrinkIngredients = new HashSet<DrinkIngredient>();
            Ingredients = new HashSet<Ingredient>();
            UseMaterials = new HashSet<UseMaterial>();
        }

        public int Id { get; set; }
        public string SupplierName { get; set; } = null!;
        public string? PtIva { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; }

        public virtual ICollection<DrinkIngredient> DrinkIngredients { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<UseMaterial> UseMaterials { get; set; }
    }
}
