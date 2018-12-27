using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;
using PizzaStore.Repos;

namespace PizzaStore.Controllers
{
    public class IngredientsController : Controller
    {
        public IPizzaStoreRepository Repo;

        public IngredientsController(IPizzaStoreRepository repo)
        {
            Repo = repo;
        }

        // GET: Ingredients
        public IActionResult Index()
        {
            var allIngredients = Repo.GetAllIngredients();
            return View(allIngredients);
        }

        // GET: Ingredients/Details/5
        public ActionResult Details(int id)
        {
                if (id == null)
                {
                    return NotFound();
                }

                Ingredients ing = Repo.GetIngredientsById(id);

                return View(ing);
        }

        // GET: Ingredients/Create
        public ActionResult Create([FromQuery]int storeId)
        {
            var ing = new Ingredients
            {
                StoreId = storeId,
            };
            return View(ing);
        }

        // POST: Ingredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,StockNumber,StoreId")] Ingredients ingredients)
        {
            if (ModelState.IsValid)
            {
                Store store = Repo.GetStoreById(ingredients.StoreId);

                Ingredients NewIngredient = new Ingredients
                {
                    Name = ingredients.Name,
                    StockNumber = ingredients.StockNumber,
                };
                Repo.AddIngredients(ingredients, store);
                 Repo.Save();
                return RedirectToAction(nameof(Index));
            }
        //  ViewData["PizzaId"] = new SelectList(Models.Pizza, "Id", "CrustType", ingredients.PizzaId);
        //  ViewData["StoreId"] = new SelectList(Repo.Store, "Id", "Address", ingredients.StoreId);
            return View(ingredients);
        }

        // GET: Ingredients/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredients =  Repo.GetIngredientsById(id);
            if (ingredients == null)
            {
                return NotFound();
            }
           // ViewData["PizzaId"] = new SelectList(Repo.Pizza, "Id", "CrustType", ingredients.PizzaId);
         //   ViewData["StoreId"] = new SelectList(Repo.Store, "Id", "Address", ingredients.StoreId);
            return View(ingredients);
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,Name,StockNumber,StoreId,PizzaId")] Ingredients ingredients)
        {
            if (id != ingredients.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Repo.UpdateIngredients(ingredients);
                    Repo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return View(ingredients);
                }
                return RedirectToAction(nameof(Index));
            }
         
            return View(ingredients);
        }

        // GET: Ingredients/Delete/5
        public ActionResult Delete(int id)
        {
            Ingredients ingredients = Repo.GetIngredientsById(id);

           // return View(pizza);

            return View(ingredients);
        }

        // POST: Ingredients/Delete/5
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
