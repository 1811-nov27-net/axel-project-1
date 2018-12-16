using System;
using System.Collections.Generic;

namespace PizzaStore.DataAccess
{
    public partial class UserLocation
    {
        public UserLocation()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; }
        public string State { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
