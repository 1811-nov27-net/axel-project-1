using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PizzaStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaStore.DataAccess;

namespace PizzaStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly PizzaStoreDBContext _db;
     //   public IPizzaStoreRepository repo;

        public HomeController(PizzaStoreDBContext db)
        {
            _db = db;
        }

        public IActionResult Index([FromQuery]string search)
        {
            var userSearched = from user in _db.Users
                              select user;

            if (!string.IsNullOrEmpty(search))
            {
                userSearched = userSearched.Where(u => u.FirstName.Contains(search) || u.FirstName.Contains(search));

                Console.WriteLine(userSearched);
            }
            
            return View(userSearched);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
