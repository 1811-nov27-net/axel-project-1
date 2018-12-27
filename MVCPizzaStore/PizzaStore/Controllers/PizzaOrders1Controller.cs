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
    public class PizzaOrders1Controller : Controller
    {
        private readonly PizzaStoreDBContext _context;

        public PizzaOrders1Controller(PizzaStoreDBContext context)
        {
            _context = context;
        }

        // GET: PizzaOrders1
        public async Task<IActionResult> Index()
        {
            var pizzaStoreDBContext = _context.PizzaOrder.Include(p => p.Order);
            return View(await pizzaStoreDBContext.ToListAsync());
        }

        // GET: PizzaOrders1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaOrder = await _context.PizzaOrder
                .Include(p => p.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizzaOrder == null)
            {
                return NotFound();
            }

            return View(pizzaOrder);
        }

        // GET: PizzaOrders1/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            return View();
        }

        // POST: PizzaOrders1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantity,OrderId")] PizzaOrder pizzaOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pizzaOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", pizzaOrder.OrderId);
            return View(pizzaOrder);
        }

        // GET: PizzaOrders1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaOrder = await _context.PizzaOrder.FindAsync(id);
            if (pizzaOrder == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", pizzaOrder.OrderId);
            return View(pizzaOrder);
        }

        // POST: PizzaOrders1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantity,OrderId")] PizzaOrder pizzaOrder)
        {
            if (id != pizzaOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pizzaOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PizzaOrderExists(pizzaOrder.Id))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", pizzaOrder.OrderId);
            return View(pizzaOrder);
        }

        // GET: PizzaOrders1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaOrder = await _context.PizzaOrder
                .Include(p => p.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizzaOrder == null)
            {
                return NotFound();
            }

            return View(pizzaOrder);
        }

        // POST: PizzaOrders1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pizzaOrder = await _context.PizzaOrder.FindAsync(id);
            _context.PizzaOrder.Remove(pizzaOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PizzaOrderExists(int id)
        {
            return _context.PizzaOrder.Any(e => e.Id == id);
        }
    }
}
