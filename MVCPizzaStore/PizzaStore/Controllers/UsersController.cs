using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;
using PizzaStore.Repos;

namespace PizzaStore.Controllers
{
    public class UsersController : Controller
    {
        public IPizzaStoreRepository Repo { get; }

        public UsersController(IPizzaStoreRepository repo)
        {
            Repo = repo;
        }

        // GET: Users 
        public ActionResult Index([FromQuery]string search = "")
        {
            List<Users> userSearched = Repo.GetUsersBySearch(search);

            return View(userSearched);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Users users = Repo.GetUsersById(id);

            return View(users);
        }
        [Route("PlaceOrder")]
        public ActionResult PlaceOrder(int id)
        {
            var usersLocation = from ul in Repo.GetAllUserLocations()
                               where ul.UserId == id
                               select ul;

        //    IEnumerable < Orders > usersorders = new List<Orders>();

            foreach (var items in usersLocation)
            {
               if(items.UserId == id)
                {
                    TempData["UserLocationId"] = id;

                    return RedirectToAction("Create", "PizzaOrders", id);
                }
            }
            return RedirectToAction("Create", "PizzaOrders");
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Create([Bind("Id,FirstName,LastName")] Users users)
        {
         
            if(ModelState.IsValid)
            {
                Repo.AddUsers(users);
            }
            Repo.Save();

            return View(users);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {

            Users user = Repo.GetUsersById(id);

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromRoute]int id, [Bind("FirstName,LastName")] Users users)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updated = Repo.GetUsersById(id);
                    updated.FirstName = users.FirstName;
                    updated.LastName = users.LastName;
                    Repo.UpdateUsers(updated);
                    Repo.Save();

                    return RedirectToAction(nameof(Index));
                }
                return View(users);
            }
           catch (DbUpdateConcurrencyException)
            {
                return View();
            }
          
        }
        //// GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            Users users = Repo.GetUsersById(id);

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, [BindNever] IFormCollection collection)
        {
            try
            {
                Repo.DeleteUsers(id);

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
