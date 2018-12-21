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
    public class OrdersController : Controller
    {
        public IPizzaStoreRepository Repo { get; set; }

        public OrdersController(IPizzaStoreRepository repo)
        {
            Repo = repo;
        }

        //// get: pizzaorders
        public ActionResult Index()
        {
            IEnumerable<Orders> orders = Repo.GetAllOrders();
            return View(orders);

        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            var users = Repo.GetUsersById(id);

            return View(users);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            //ViewData["ShopId"] = new SelectList(Store, "Id", "Address");
            //ViewData["UserLocationId"] = new SelectList(_db.UserLocation, "Id", "Address");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind("Id,UserLocationId,ShopId")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                Repo.AddOrders(orders);
                
                Repo.Save();

                return RedirectToAction(nameof(Index));
            }
           // return View(restaurant);
            return View(orders);
        }

    }
}
