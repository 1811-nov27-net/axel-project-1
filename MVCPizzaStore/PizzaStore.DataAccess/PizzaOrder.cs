using System;
using System.Collections.Generic;

namespace PizzaStore.DataAccess
{
    public partial class PizzaOrder
    {
        public PizzaOrder()
        {
            Pizza = new HashSet<Pizza>();
        }

        public int Id { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }

        public virtual Orders Order { get; set; }
        public virtual ICollection<Pizza> Pizza { get; set; }
    }
}
