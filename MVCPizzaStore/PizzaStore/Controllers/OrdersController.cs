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
    public class OrdersController : Controller
    {
        private readonly PizzaStoreDBContext _context;

        public OrdersController(PizzaStoreDBContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(string sortOrder)
        {
         

            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.TotalSortParm = sortOrder == "Total" ? "total_desc" : "Total";
            var orders = from o in _context.Orders
                         where o.UserLocation != null && o.Shop != null
                         select o;
            //var orders = _context.Orders.Include(o => o.Shop).Include(o => o.UserLocation).FirstOrDefault();
            switch (sortOrder)
            {
          
                case "Date":
                    orders = orders.OrderBy(o => o.OrderTime);
                    break;
                case "date_desc":
                    orders = orders.OrderByDescending(o => o.OrderTime);
                    break;
                case "Total":
                    orders = orders.OrderBy(o => o.TotalDue);
                    break;
                case "total_desc":
                    orders = orders.OrderByDescending(o => o.TotalDue);
                    break;
                default:
                    orders = orders.OrderBy(o => o.Id);
                    break;
            }
            return View(await orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Shop)
                .Include(o => o.UserLocation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["ShopId"] = new SelectList(_context.Store, "Id", "Address");
            ViewData["UserLocationId"] = new SelectList(_context.UserLocation, "Id", "Address");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserLocationId,ShopId,OrderTime,TotalDue")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                orders.OrderTime = DateTime.Now;
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["ShopId"] = new SelectList(_context.Store, "Id", "Address", orders.ShopId);
            ViewData["UserLocationId"] = new SelectList(_context.UserLocation, "Id", "Address", orders.UserLocationId);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["ShopId"] = new SelectList(_context.Store, "Id", "Address", orders.ShopId);
            ViewData["UserLocationId"] = new SelectList(_context.UserLocation, "Id", "Address", orders.UserLocationId);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserLocationId,ShopId,OrderTime,TotalDue")] Orders orders)
        {
            if (id != orders.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.Id))
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
            ViewData["ShopId"] = new SelectList(_context.Store, "Id", "Address", orders.ShopId);
            ViewData["UserLocationId"] = new SelectList(_context.UserLocation, "Id", "Address", orders.UserLocationId);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Shop)
                .Include(o => o.UserLocation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
