using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using PizzaStore.Repos;
using PizzaStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace PizzaStore.Controllers
{
    public class PizzaOrdersController : Controller
    {
        public IPizzaStoreRepository Repo { get; set; }

      
        public PizzaOrdersController(IPizzaStoreRepository repo)
        {
            Repo = repo;
        }
        
        
        //// GET: PizzaOrders/Details/5
        public ActionResult Details(int id)
        {
            PizzaOrder pizzaOrder = Repo.GetPizzaOrderById(id);
         

            return View(pizzaOrder);
        }

        //// GET: PizzaOrders/Create
        public ActionResult Create([FromQuery] int id)
        {
            var pizzaOrder = new PizzaOrder
            {
                OrderId = id,
            };
            return View(pizzaOrder);
        }
        //// POST: PizzaOrders/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Quantity,OrderId,Pizza")]PizzaOrder pizzaOrder)
        {
            if (ModelState.IsValid)
            {
                List<Pizza> selectedPizzas = new List<Pizza>();
                var pizzas = Repo.GetAllPizzas();

                var selectedIng = new List<Ingredients>();
                var Ingredients = Repo.GetAllIngredients();

                foreach(var item in pizzaOrder.Pizza)
                {
                    selectedPizzas.Add(item);
                }

              

                PizzaOrder newPizzaOrder = new PizzaOrder
                {
                    Quantity =  pizzaOrder.Quantity,
                    OrderId = pizzaOrder.OrderId,
                    Pizza = selectedPizzas,
                    //Ingredients = selectedIng
                };

                // Store store = Repo.GetStoreById(1);

                Repo.AddPizzaOrders(newPizzaOrder);

                Repo.Save();
            }
            return View(pizzaOrder);
          
            //return View(pizzaOrder);
        }

   
    }
}
