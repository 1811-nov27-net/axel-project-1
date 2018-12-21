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
        public ActionResult Create([FromQuery]int pizzaOrderId)
        {
            var pizza = new Pizza
            {
                PizzaOrderId = pizzaOrderId
            };
        return View(pizza);

        }

        // POST: Pizzas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Name,CrustType,LinePrice,PizzaOrderId")] Pizza pizza)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(pizza);
                }

              PizzaOrder pizzaOrder = Repo.GetPizzaOrderById(pizza.PizzaOrderId);
                var pizzas = new Pizza
                {
                    Name = pizza.Name,
                    CrustType = pizza.CrustType,
                    LinePrice = pizza.LinePrice,            
                };

                Repo.AddPizza(pizzas, pizzaOrder);
                Repo.Save();

                return RedirectToAction(nameof(PizzaOrdersController.Details),
                    "Pizza", new { id = pizza.PizzaOrderId });
            }
            catch
            {
                return View(pizza);
            }
        }

        //// GET: Pizzas/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var pizza = await Repo.Pizza.FindAsync(id);
        //    if (pizza == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["PizzaOrderId"] = new SelectList(Repo.PizzaOrder, "Id", "Id", pizza.PizzaOrderId);
        //    return View(pizza);
        //}

        //// POST: Pizzas/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CrustType,LinePrice,PizzaOrderId")] Models.Pizza pizza)
        //{
        //    if (id != pizza.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            Repo.Update(pizza);
        //            await Repo.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PizzaExists(pizza.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["PizzaOrderId"] = new SelectList(Repo.PizzaOrder, "Id", "Id", pizza.PizzaOrderId);
        //    return View(pizza);
        //}

        //// GET: Pizzas/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var pizza = await Repo.Pizza
        //        .Include(p => p.PizzaOrder)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (pizza == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(pizza);
        //}

        //// POST: Pizzas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var pizza = await Repo.Pizza.FindAsync(id);
        //    Repo.Pizza.Remove(pizza);
        //    await Repo.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool PizzaExists(int id)
        //{
        //    return Repo.Pizza.Any(e => e.Id == id);
        //}
    }
}
