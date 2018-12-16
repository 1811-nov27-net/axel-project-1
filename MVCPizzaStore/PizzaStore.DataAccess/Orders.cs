using System;
using System.Collections.Generic;

namespace PizzaStore.DataAccess
{
    public partial class Orders
    {
        public Orders()
        {
            PizzaOrder = new HashSet<PizzaOrder>();
        }

        public int Id { get; set; }
        public int UserLocationId { get; set; }
        public int ShopId { get; set; }
        public DateTime OrderTime { get; set; }
        public double TotalDue { get; set; }

        public virtual Store Shop { get; set; }
        public virtual UserLocation UserLocation { get; set; }
        public virtual ICollection<PizzaOrder> PizzaOrder { get; set; }
    }
}
