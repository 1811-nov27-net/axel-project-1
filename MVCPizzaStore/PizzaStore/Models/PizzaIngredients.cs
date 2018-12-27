using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStore.Models
{
    public class PizzaIngredient
    {
        public int Id { get; set; }

        public int IngredientId { get; set; }

        public int PizzaId { get; set; }

        public virtual Ingredients Ingredient { get; set; }

        public virtual Pizza Pizza { get; set; }

        public void addIngredients(Ingredients ingredients)
        {
            Ingredient = ingredients;
            IngredientId = ingredients.Id;
        }
    }
    }
