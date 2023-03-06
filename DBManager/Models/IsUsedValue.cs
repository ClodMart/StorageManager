using System;
using System.Collections.Generic;

namespace DBManager.Models
{
    public partial class IsUsedValue
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
    }
}
