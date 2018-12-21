using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaStore.Models
{
    public class Ingredients
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ingredient name")]
        public string Name { get; set; }

        [Required]
        public int StockNumber { get; set; }

        public int StoreId { get; set; }

        public int PizzaId { get; set; }
    }
}