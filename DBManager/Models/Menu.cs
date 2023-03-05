using System;
using System.Collections.Generic;

namespace DBManager.Models
{
    public partial class Menu
    {
        public Menu()
        {
            MenuPreparations = new HashSet<MenuPreparation>();
        }

        public int Id { get; set; }
        public string MenuEntry { get; set; } = null!;
        public string Category { get; set; } = null!;
        public decimal? SellingPrice { get; set; }

        public virtual ICollection<MenuPreparation> MenuPreparations { get; set; }
    }
}
