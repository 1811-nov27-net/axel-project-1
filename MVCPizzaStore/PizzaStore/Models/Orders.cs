using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaStore.Models
{
    public class Orders
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        public int UserLocationId { get; set; }

        public int ShopId { get; set; }


        public DateTime OrderTime { get; set; }

        [Range(0, 500.00)]
        public double TotalDue { get; set; }

        public List<PizzaOrder> PizzaOrder { get; set; } = new List<PizzaOrder>();
    }
}
