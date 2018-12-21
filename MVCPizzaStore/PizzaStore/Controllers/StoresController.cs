using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;
using PizzaStore.Repos;

namespace PizzaStore.Controllers
{
    public class StoresController : Controller
    {
        public IPizzaStoreRepository Repo { get; set; }

        public StoresController(IPizzaStoreRepository repo)
        {
            Repo = repo;
        }

        // GET: Stores
        public ActionResult Index()
        {
            var stores = Repo.GetAllStores();
            return View(stores);
        }

        // GET: Stores/Details/5
        public ActionResult Details(int id)
        {
            var store = Repo.GetStoreById(id);

            return View(store);
        }

        // GET: Stores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Address,State")] Store store)
        {
            if (ModelState.IsValid)
            {
                Repo.AddStores(store);
            }
            Repo.Save();
            return View(store);
        }
        // GET: Stores/Edit/5
        public ActionResult Edit(int id)
        {

            Store store = Repo.GetStoreById(id);

            return View(store);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromRoute]int id, [Bind("Address,State")] Store store)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updated = Repo.GetStoreById(id);
                    updated.Address = store.Address;
                    updated.State = store.State;
                    Repo.UpdateStore(updated);
                    Repo.Save();

                    return RedirectToAction(nameof(Index));
                }
                return View(store);
            }
             catch (DbUpdateConcurrencyException)
            {
                return View(store);
            }
            
        }

        // GET: Stores/Delete/5
        public ActionResult Delete(int id)
        {
            Store store = Repo.GetStoreById(id);

            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, [BindNever]IFormCollection collection)
        {
            try
            {
                Repo.DeleteStore(id);

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
