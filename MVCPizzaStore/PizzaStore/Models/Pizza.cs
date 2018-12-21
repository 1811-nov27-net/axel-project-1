using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PizzaStore.Models
{
    public class Pizza
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Pizza type")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Crust type")]
        public string CrustType { get; set; }

        [Required]
        [Display(Name = "Pizza price")]
        public decimal LinePrice { get; set; }

        public int PizzaOrderId { get; set; }

        public List<Ingredients> IngredientList { get; set; } = new List<Ingredients>();
    }
}
