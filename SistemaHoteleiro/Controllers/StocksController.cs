using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaHoteleiro.Data;
using SistemaHoteleiro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaHoteleiro.Controllers
{
    public class StocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id is null)
                return NotFound();

            var product = await _context.Products                                      
                                        .FirstOrDefaultAsync(x => x.Id == id);

            return View(product);
        }


        [HttpPost]
        public async Task<IActionResult> Index([Bind("Id, Stock, Name")] Product product)
        {
            if (product is null)
                return NotFound();

            var stock = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == product.Id);

            stock.Stock = product.Stock;

            
            
            _context.SaveChanges();

            return Redirect("/Products");
        }
    }
}