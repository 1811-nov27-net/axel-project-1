using System;
using System.Collections.Generic;

namespace PizzaStore.DataAccess
{
    public partial class Ingredients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StockNumber { get; set; }
        public int StoreId { get; set; }
        public int? PizzaId { get; set; }

        public virtual Pizza Pizza { get; set; }
        public virtual Store Store { get; set; }
    }
}
