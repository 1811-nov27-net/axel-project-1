using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult Create()
        {
            var ing = new Ingredients
            {
                StoreId = storeId,
                PizzaId = pizzaId
            };
            return View();
        }

        // POST: Ingredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind("Id,Name,StockNumber")] Ingredients ingredients)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Repo.Add(ingredients);
        //        await Repo.SaveChanges();
        //        return RedirectToAction(nameof(Index));
        //    }
        ////  ViewData["PizzaId"] = new SelectList(Models.Pizza, "Id", "CrustType", ingredients.PizzaId);
        ////  ViewData["StoreId"] = new SelectList(Repo.Store, "Id", "Address", ingredients.StoreId);
        //    return View(ingredients);
        //}

        //// GET: Ingredients/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var ingredients = await Repo.Ingredients.Find(id);
        //    if (ingredients == null)
        //    {
        //        return NotFound();
        //    }
        //   // ViewData["PizzaId"] = new SelectList(Repo.Pizza, "Id", "CrustType", ingredients.PizzaId);
        // //   ViewData["StoreId"] = new SelectList(Repo.Store, "Id", "Address", ingredients.StoreId);
        //    return View(ingredients);
        //}

        //// POST: Ingredients/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, [Bind("Id,Name,StockNumber,StoreId,PizzaId")] Ingredients ingredients)
        //{
        //    if (id != ingredients.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            Repo.Update(ingredients);
        //            await Repo.SaveChanges();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!IngredientsExists(ingredients.Id))
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
        //    ViewData["PizzaId"] = new SelectList(Repo.Pizza, "Id", "CrustType", ingredients.PizzaId);
        //    ViewData["StoreId"] = new SelectList(Repo.Store, "Id", "Address", ingredients.StoreId);
        //    return View(ingredients);
        //}

        //// GET: Ingredients/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var ingredients = await Repo.Ingredients
        //        .Include(i => i.Pizza)
        //        .Include(i => i.Store)
        //        .FirstOrDefault(m => m.Id == id);
        //    if (ingredients == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(ingredients);
        //}

        //// POST: Ingredients/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    var ingredients = await Repo.Ingredients.Find(id);
        //    Repo.Ingredients.Remove(ingredients);
        //    await Repo.SaveChanges();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool IngredientsExists(int id)
        //{
        //    return Repo.Ingredients.Any(e => e.Id == id);
        //}
    }
}
