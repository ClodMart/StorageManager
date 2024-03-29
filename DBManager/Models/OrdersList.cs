﻿using System;
using System.Collections.Generic;

namespace DBManager.Models
{
    public partial class OrdersList
    {
        public long EntryId { get; set; }
        public long OrderId { get; set; }
        public long IngredientId { get; set; }
        public long Quantity { get; set; }

        public virtual Ingredient Ingredient { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
