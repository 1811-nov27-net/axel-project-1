using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PizzaStore.Models
{
    public class Store
    {
       
        public int Id { get; set; }

        [Required]
        [Display(Name = "Street address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        public List<Ingredients> Ingredients { get; set; } = new List<Ingredients>();

        public List<Orders> Orders { get; set; } = new List<Orders>();
    }
}
