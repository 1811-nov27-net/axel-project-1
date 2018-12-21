using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        [StringLength(128)]
        public string Address { get; set; }
        [Required]
        [StringLength(128)]
        public string State { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
