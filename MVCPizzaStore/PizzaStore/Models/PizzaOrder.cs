using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaStore.Models
{
    public class PizzaOrder
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [Range(0, 12)]
        public int Quantity { get; set; }

        public int OrderId { get; set; }

        public List<Pizza> Pizza { get; set; } = new List<Pizza>();
    }
}
