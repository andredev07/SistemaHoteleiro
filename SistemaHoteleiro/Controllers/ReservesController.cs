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

    public class ReservesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reserves
        public async Task<IActionResult> Index()
        {
            var reserves = await _context.Reserves.Where(x => x.Active)
            .Include(x => x.Guest)
            .Include(x => x.Room)
            .Include(x => x.Room.CategoryRoom)
            .ToListAsync();

            return View(reserves);
        }
        

        // GET: Reserves/Details/5
       public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserve = await _context.Reserves
            .Include(x => x.Guest)
            .Include(x => x.Room)
            .Include(x => x.Room.CategoryRoom)
            .FirstOrDefaultAsync(m => m.Id == id);

            var sales = await _context.Sales
                .Where(x => x.ReserveId == reserve.Id)
                .Include(x => x.Product)
                .ToListAsync();

            reserve.Sales = sales;

            if (reserve == null)
            {
                return NotFound();
            }

            return View(reserve);
        }

        // GET: Reserves/Create
        public async Task<IActionResult> Create()
        {
            var guests = await _context.Guests.ToListAsync();
            var rooms = await _context.Rooms.ToListAsync();

            ViewBag.Guests = guests.Select(guest => new SelectListItem 
            {
                Value = guest.Id.ToString(),
                Text = guest.Name
            });
            ViewBag.Rooms = rooms.Select(room => new SelectListItem 
            {
                Value = room.Id.ToString(),
                Text = room.Name
            });

            return View();
        }

        // POST: Reserves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GuestId,RoomId,Fone,Cpf,Email,Cidade,NumberPeople,DataInicio,DataFim,Id,CreatedAt,Active")] Reserve reserve)
        {
            if (ModelState.IsValid == true)
            {
                List<Reserve> reserveAlreadyExist = await _context.Reserves.Where(x => x.RoomId == reserve.RoomId &&
                                                                                            x.DataInicio >= reserve.DataInicio &&
                                                                                           x.DataFim <= reserve.DataFim).ToListAsync();
                if (reserveAlreadyExist.Count > 0){
                    ModelState.AddModelError("already-exists", "Já existe uma reserva neste mesmo período para este quarto.");
                    return View(reserve);
                }

                var room = await _context.Rooms
                    .Include(x => x.CategoryRoom)
                    .FirstOrDefaultAsync(x => x.Id == reserve.RoomId);

                int differenceInDays = (reserve.DataFim - reserve.DataInicio).Days;

                reserve.TotalPrice = room.CategoryRoom.Price * differenceInDays;

                _context.Add(reserve);
                await _context.SaveChangesAsync();

                ViewBag.Message = string.Format
                    ("Usuário criado com sucesso");

                return View(reserve);
            }

            return View(reserve);
        }

        // GET: Reserves/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
        
            if (id == null)
            {
                return NotFound();
            }

            var reserve = await _context.Reserves.FindAsync(id);

            
            if (reserve == null)
            {
                return NotFound();
            }

            var guests = await _context.Guests.Where(x => x.Active).ToListAsync();
            var rooms = await _context.Rooms.Where(x => x.Active).ToListAsync();

            ViewBag.Guests = guests.Select(guest => new SelectListItem 
            {
                Value = guest.Id.ToString(),
                Text = guest.Name
            });
            ViewBag.Rooms = rooms.Select(room => new SelectListItem 
            {
                Value = room.Id.ToString(),
                Text = room.Name
            });

            return View(reserve);
        }

        // POST: Reserves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GuestId,RoomId,Fone,Cpf,Email,Cidade,NumberPeople,DataInicio,DataFim,Id,CreatedAt,Active")] Reserve reserve)
        {
            if (id != reserve.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserve);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReserveExists(reserve.Id))
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
            return View(reserve);
        }

        // GET: Reserves/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserve = await _context.Reserves
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserve == null)
            {
                return NotFound();
            }

            return View(reserve);
        }

        // POST: Reserves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserve = await _context.Reserves.FindAsync(id);
            reserve.Deactivate();
            _context.Reserves.Update(reserve);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReserveExists(int id)
        {
            return _context.Reserves.Any(e => e.Id == id);
        }
    }
}
