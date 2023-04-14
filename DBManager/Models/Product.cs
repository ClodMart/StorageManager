using System;
using System.Collections.Generic;

namespace DBManager.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductCompositions = new HashSet<ProductComposition>();
        }

        public long Id { get; set; }
        public string ProductName { get; set; } = null!;
        public double ProductPrice { get; set; }
        public double? ProductCost { get; set; }
        public string ProductCategory { get; set; } = null!;

        public virtual ICollection<ProductComposition> ProductCompositions { get; set; }
    }
}
