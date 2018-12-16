using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaStore.DataAccess;

namespace PizzaStore.Controllers
{
    public class UserLocationsController : Controller
    {
        private readonly PizzaStoreDBContext _db;

        public UserLocationsController(PizzaStoreDBContext context)
        {
            _db = context;
        }

        // GET: UserLocations
        public async Task<IActionResult> Index()
        {
            var pizzaStoreDBContext = _db.UserLocation.Include(u => u.User);
            return View(await pizzaStoreDBContext.ToListAsync());
        }

        // GET: UserLocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLocation = await _db.UserLocation
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userLocation == null)
            {
                return NotFound();
            }

            return View(userLocation);
        }

        // GET: UserLocations/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_db.Users, "Id", "FirstName");
            return View();
        }

        // POST: UserLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Address,State")] UserLocation userLocation)
        {
            if (ModelState.IsValid)
            {
                _db.Add(userLocation);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_db.Users, "Id", "FirstName", userLocation.UserId);
            return View(userLocation);
        }

        // GET: UserLocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLocation = await _db.UserLocation.FindAsync(id);
            if (userLocation == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_db.Users, "Id", "FirstName", userLocation.UserId);
            return View(userLocation);
        }

        // POST: UserLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Address,State")] UserLocation userLocation)
        {
            if (id != userLocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(userLocation);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserLocationExists(userLocation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_db.Users, "Id", "FirstName", userLocation.UserId);
            return View(userLocation);
        }

        // GET: UserLocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLocation = await _db.UserLocation
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userLocation == null)
            {
                return NotFound();
            }

            return View(userLocation);
        }

        // POST: UserLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userLocation = await _db.UserLocation.FindAsync(id);
            _db.UserLocation.Remove(userLocation);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserLocationExists(int id)
        {
            return _db.UserLocation.Any(e => e.Id == id);
        }
    }
}
