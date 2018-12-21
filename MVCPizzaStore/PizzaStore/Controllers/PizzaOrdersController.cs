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
            var webRest = new PizzaOrder
            {
                Id = pizzaOrder.Id,
                Quantity = pizzaOrder.Quantity,
                Pizza = pizzaOrder.Pizza.Select(y => new Pizza
                {
                    Id = y.Id,
                    Name = y.Name,
                    CrustType = y.CrustType,
                    LinePrice = y.LinePrice
                }).ToList()
            };
            return View(webRest);
        }

        //// GET: PizzaOrders/Create
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        //public ActionResult AddToPizzaOrder(int id)
        //{
        //    Models.Pizza pizzas = Repo.GetPizzaById(id);
        //    IEnumerable<Models.PizzaOrder> pizzaOrders = Repo.GetAllPizzaOrders().ToList();
        //   // pizzaOrders = pizzaOrders.Where(p => p.Pizza == null).ToList().Add(pizza);

        //    foreach(var items in pizzaOrders)
        //    {
        //        if (items.Id == pizzas.Id)
        //        {
        //            _db.Add(items.Pizza);
        //            _db.SaveChanges();
        //        }
        //    }


        //    //ViewData["typesOfPizza"] = new SelectList(Repo.GetAllPizzas(), "Name", "CrustType");

        //    return View(pizzas);

        //}
        //// POST: PizzaOrders/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Quantity")] PizzaOrder pizzaOrder)
        {
            if (ModelState.IsValid)
            {
                Repo.AddPizzaOrders(pizzaOrder);

                Repo.Save();

                Pizza pizza = new Pizza();
                pizza = Repo.GetByPizzaId(pizzaOrder.Id);

                return RedirectToAction("Create", "Pizza", pizza);
            }
            // return View(restaurant);
           // ViewData["Pizzas"] = new SelectList(Repo.GetAllPizzas(), "Name", "CrustTypes");
           // ViewData["Ingredients"] = new SelectList(Repo.GetAllIngredients(), "Name", "CrustTypes");
            return View(pizzaOrder);
          
            //return View(pizzaOrder);
        }

   
    }
}
