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

    public class ReserveProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReserveProductsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Products
        public async Task<IActionResult> Index()
        {
            var reserveProduct = await _context.ReserveProducts
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

            var reserveProduct = await _context.ReserveProducts
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
            var reserveProduct = new ReserveProduct();

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
        public async Task<IActionResult> Create([Bind("ReserveId,ProductId,Id,CreatedAt,Active")] ReserveProduct reserveProduct)
        {
            reserveProduct.Id = 0;

            if (ModelState.IsValid)
            {
                _context.ReserveProducts.Add(reserveProduct);

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

            var reserveProduct = await _context.ReserveProducts.FindAsync(id);

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
        public async Task<IActionResult> Edit(int id, [Bind("ReserveId,ProductId,Id,CreatedAt,Active")] ReserveProduct reserveProduct)
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

            var reserveProduct = await _context.ReserveProducts
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
            var reserveProduct = await _context.ReserveProducts.FindAsync(id);

            reserveProduct.Deactivate();

            _context.ReserveProducts.Update(reserveProduct);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        private bool ReserveProductExists(int id)
        {
            return _context.ReserveProducts.Any(e => e.Id == id);
        }
    }
}