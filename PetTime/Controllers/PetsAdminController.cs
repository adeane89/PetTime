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
    public class PetsAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PetsAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PetsAdmin
        public async Task<IActionResult> Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                return View(await _context.Pets.ToListAsync());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: PetsAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .SingleOrDefaultAsync(m => m.ID == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // GET: PetsAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PetsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,Price,ImagePath,DateTime,Age,DateCreated,DateLastModified")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                pet.DateCreated = DateTime.Now;
                pet.DateLastModified = DateTime.Now;
                _context.Add(pet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pet);
        }

        // GET: PetsAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets.SingleOrDefaultAsync(m => m.ID == id);
            if (pet == null)
            {
                return NotFound();
            }
            return View(pet);
        }

        // POST: PetsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,Price,ImagePath,DateTime,Age,DateCreated,DateLastModified")] Pet pet)
        {
            if (id != pet.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    pet.DateLastModified = DateTime.Now;
                    _context.Update(pet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetExists(pet.ID))
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
            return View(pet);
        }

        // GET: PetsAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .SingleOrDefaultAsync(m => m.ID == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // POST: PetsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pet = await _context.Pets.SingleOrDefaultAsync(m => m.ID == id);
            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetExists(int id)
        {
            return _context.Pets.Any(e => e.ID == id);
        }
    }
}
