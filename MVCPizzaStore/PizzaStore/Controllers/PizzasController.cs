using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using PizzaStore.Repos;
using PizzaStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace PizzaStore.Controllers
{
    public class PizzasController : Controller
    {
        
        public IPizzaStoreRepository Repo { get; set; }

        public PizzasController(IPizzaStoreRepository repo)
        {
      
            Repo = repo;
        }
    

        // GET: Pizzas
        public ActionResult Index()
        {
            IEnumerable<Pizza> allPizzas = Repo.GetAllPizzas();
            return View(allPizzas);
        }

        // GET: Pizzas/Details/5
        public ActionResult Details(int id)
        {
             Pizza pizzaByID = Repo.GetByPizzaId(id);
         
            return View(pizzaByID);
        }

        //  GET: Pizzas/Create
        public ActionResult Create()
        {
            ViewData["IngredientList"] = new SelectList(Repo.GetAllIngredients(), "Id", "Name");
            return View();

        }

        // POST: Pizzas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Name,CrustType,LinePrice,PizzaOrderId,IngredientsList")] Pizza pizza)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    Repo.AddPizza(pizza);
                    Repo.Save();
                }
                
                ViewData["IngredientList"] = new SelectList(Repo.GetAllIngredients(), "Id", "Name", pizza.IngredientId);
                return View(pizza);
            }
            catch
            {
                return View(pizza);
            }
        }

        //// GET: Pizzas/Edit/5
        public ActionResult Edit(int id)
        {

            Pizza pizza = Repo.GetByPizzaId(id);

            return View(pizza);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromRoute]int id, [Bind("Name,CrustType,LinePrice,PizzaOrderId")] Pizza pizza)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updated = Repo.GetByPizzaId(id);
                    updated.Name = pizza.Name;
                    updated.CrustType = pizza.CrustType;
                    updated.LinePrice = pizza.LinePrice;
                    Repo.UpdatePizza(updated);
                    Repo.Save();

                    return RedirectToAction(nameof(Index));
                }
                return View(pizza);
            }
            catch (DbUpdateConcurrencyException)
            {
                return View(pizza);
            }

        }

        // GET: Stores/Delete/5
        public ActionResult Delete(int id)
        {
            Pizza pizza = Repo.GetByPizzaId(id);

            return View(pizza);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, [BindNever]IFormCollection collection)
        {
            try
            {
                Repo.DeletePizza(id);

                Repo.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
