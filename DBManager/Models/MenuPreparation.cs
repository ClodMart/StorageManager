using System;
using System.Collections.Generic;

namespace DBManager.Models
{
    public partial class MenuPreparation
    {
        public int EntryId { get; set; }
        public int MenuProductId { get; set; }
        public int IngedientId { get; set; }
        public decimal? IngredientQuantity { get; set; }
        public int UnitOfMesure { get; set; }

        public virtual Menu MenuProduct { get; set; } = null!;
        public virtual UnitsOfMesure UnitOfMesureNavigation { get; set; } = null!;
    }
}
