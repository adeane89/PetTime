using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetTime.Data;
using PetTime.Models;

namespace PetTime.Controllers
{
    public class ReceiptController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReceiptController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: Receipt/Details/5
        public async Task<IActionResult> Index (Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petOrder = await _context.PetOrders.Include(x => x.PetOrderProducts)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (petOrder == null)
            {
                return NotFound();
            }

            return View(petOrder);
        }
    }
}
