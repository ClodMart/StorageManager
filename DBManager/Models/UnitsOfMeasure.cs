using System;
using System.Collections.Generic;

namespace DBManager.Models
{
    public partial class UnitsOfMeasure
    {
        public long Id { get; set; }
        public string Description { get; set; } = null!;
    }
}
