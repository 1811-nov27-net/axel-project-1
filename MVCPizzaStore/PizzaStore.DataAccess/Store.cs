using System;
using System.Collections.Generic;

namespace PizzaStore.DataAccess
{
    public partial class Store
    {
        public Store()
        {
            Ingredients = new HashSet<Ingredients>();
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string Address { get; set; }
        public string State { get; set; }

        public virtual ICollection<Ingredients> Ingredients { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
