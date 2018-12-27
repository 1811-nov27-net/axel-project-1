using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PizzaStore.Models
{
    public class Pizza
    {
        public Pizza()
        {
        }

        public Pizza(List<Ingredients> ingredients)
        {
            IngredientList = ingredients;
        }

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
        public double LinePrice { get; set; }

        public int PizzaOrderId { get; set; }
       
        public int IngredientId { get; set; }

        public List<Ingredients> IngredientList { get; set; } = new List<Ingredients>();

     //   public virtual List<PizzaIngredient> PizzaIngredients { get; set; }

        //public void addIngredients(Ingredients ingredient)
        //{
        //    PizzaIngredient pi = new PizzaIngredient();
        //    pi.addIngredients(ingredient);

        //    if (Id > 0)
        //        pi.PizzaId = Id;

        //    PizzaIngredients.Add(pi);
        //}
    }
}
