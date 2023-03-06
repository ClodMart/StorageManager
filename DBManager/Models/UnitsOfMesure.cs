using System;
using System.Collections.Generic;

namespace DBManager.Models
{
    public partial class UnitsOfMesure
    {
        public UnitsOfMesure()
        {
            MenuPreparations = new HashSet<MenuPreparation>();
        }

        public int Id { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<MenuPreparation> MenuPreparations { get; set; }
    }
}
