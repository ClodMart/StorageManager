using System;
using System.Collections.Generic;

namespace DBManager.Models
{
    public partial class Order
    {
        public Order()
        {
            OrdersLists = new HashSet<OrdersList>();
        }

        public long Id { get; set; }
        public long SupplierId { get; set; }
        public DateOnly OrderDate { get; set; }
        public DateTime Time { get; set; }

        public virtual Supplier Supplier { get; set; } = null!;
        public virtual ICollection<OrdersList> OrdersLists { get; set; }
    }
}
