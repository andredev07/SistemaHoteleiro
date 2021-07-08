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

namespace SistemaHoteleiro.Controllers
{
    [Authorize]

    public class CategoryRoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryRoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CategoryRooms
        public async Task<IActionResult> Index()
        {
            var categoryRoom = await _context.CategoryRooms.Where(x => x.Active).ToListAsync();

            return View(categoryRoom);
        }

        // GET: CategoryRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryRoom = await _context.CategoryRooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryRoom == null)
            {
                return NotFound();
            }

            return View(categoryRoom);
        }

        // GET: CategoryRooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoryRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,Price,Active")] CategoryRoom categoryRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryRoom);
        }

        // GET: CategoryRooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryRoom = await _context.CategoryRooms.FindAsync(id);
            if (categoryRoom == null)
            {
                return NotFound();
            }
            return View(categoryRoom);
        }

        // POST: CategoryRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GuestId,Description,Price,Id,CreatedAt,Active")] CategoryRoom categoryRoom)
        {
            if (id != categoryRoom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryRoomExists(categoryRoom.Id))
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
            return View(categoryRoom);
        }

        // GET: CategoryRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryRoom = await _context.CategoryRooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryRoom == null)
            {
                return NotFound();
            }

            return View(categoryRoom);
        }

        // POST: CategoryRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryRoom = await _context.CategoryRooms.FindAsync(id);
            categoryRoom.Deactivate();
            _context.CategoryRooms.Update(categoryRoom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryRoomExists(int id)
        {
            return _context.CategoryRooms.Any(e => e.Id == id);
        }
    }
}
