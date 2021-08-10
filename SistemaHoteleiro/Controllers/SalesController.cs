using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaHoteleiro.Data;
using SistemaHoteleiro.Models;
//using SistemaHoteleiro.Models;

namespace SistemaHoteleiro.Controllers
{
    [Authorize]

    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Products
        public async Task<IActionResult> Index()
        {
            var reserveProduct = await _context.Sales
            .Where(x => x.Active)
            .Include(x => x.Product)
            .Include(x => x.Reserve.Room.CategoryRoom)
            .ToListAsync();

            return View(reserveProduct);
        }


        // GET: ReserveProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserveProduct = await _context.Sales
                .FirstOrDefaultAsync(m => m.Id == id);

            if (reserveProduct == null)
            {
                return NotFound();
            }

            return View(reserveProduct);
        }


        // GET: ReserveProducts/Create/4
        public async Task<IActionResult> Create(int id)
        {
            var reserveProduct = new Sale();

            reserveProduct.ReserveId = id;

            var products = await _context.Products.ToListAsync();

            ViewBag.Products = products.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            return View(reserveProduct);
        }


        // POST: ReserveProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ([Bind("ReserveId,ProductId,Id,CreatedAt,Active,Amount")] Sale reserveProduct)
        {
            reserveProduct.Id = 0;

            if (ModelState.IsValid)
            {
                var searchProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == reserveProduct.ProductId);

                if (searchProduct.Stock < reserveProduct.Amount)
                {
                    ModelState.AddModelError("sem-estoque", "O produto não possui estoque disponivel.");
                    return View(reserveProduct);
                } 

                _context.Sales.Add(reserveProduct);

                searchProduct.Stock = searchProduct.Stock - reserveProduct.Amount;

                var transaction = new Transaction();

                _context.Transactions.Add(transaction);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(reserveProduct);
        }


        // GET: ReserveProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserveProduct = await _context.Sales.FindAsync(id);

            if (reserveProduct == null)
            {
                return NotFound();
            }

            return View(reserveProduct);
        }



        // POST: ReserveProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReserveId,ProductId,Id,CreatedAt,Active")] Sale reserveProduct)
        {
            if (id != reserveProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserveProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReserveProductExists(reserveProduct.Id))
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
            return View(reserveProduct);
        }


        // GET: ReserveProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserveProduct = await _context.Sales
                .FirstOrDefaultAsync(m => m.Id == id);

            if (reserveProduct == null)
            {
                return NotFound();
            }

            return View(reserveProduct);
        }


        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserveProduct = await _context.Sales.FindAsync(id);

            reserveProduct.Deactivate();

            _context.Sales.Update(reserveProduct);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        private bool ReserveProductExists(int id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }
    }
}