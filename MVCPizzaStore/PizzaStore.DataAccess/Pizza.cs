using System;
using System.Collections.Generic;

namespace PizzaStore.DataAccess
{
    public partial class Pizza
    {
        public Pizza()
        {
            Ingredients = new HashSet<Ingredients>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string CrustType { get; set; }
        public decimal LinePrice { get; set; }
        public int? PizzaOrderId { get; set; }

        public virtual PizzaOrder PizzaOrder { get; set; }
        public virtual ICollection<Ingredients> Ingredients { get; set; }
    }
}
