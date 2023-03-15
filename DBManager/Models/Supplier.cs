using System;
using System.Collections.Generic;

namespace DBManager.Models
{
    public partial class Supplier
    {
        public int Id { get; set; }
        public string SupplierName { get; set; } = null!;
        public string? PtIva { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; }
    }
}
