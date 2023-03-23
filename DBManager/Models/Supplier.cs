using System;
using System.Collections.Generic;

namespace DBManager.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            IngredientsFormats = new HashSet<IngredientsFormat>();
        }

        public long Id { get; set; }
        public string SupplierName { get; set; } = null!;
        public string? PtIva { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Notes { get; set; }

        public virtual ICollection<IngredientsFormat> IngredientsFormats { get; set; }
    }
}
