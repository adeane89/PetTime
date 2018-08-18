using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetTime.Models;
using Microsoft.EntityFrameworkCore;
using PetTime.Data;
using Microsoft.AspNetCore.Identity;

namespace PetTime.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public CartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            PetCart model = null;
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = _userManager.GetUserAsync(User).Result;
                model = _context.PetCarts.Include(x => x.PetCartProducts).ThenInclude(x => x.Pet).
                        Include(x => x.CorporateCart).Include(x => x.TherapyCart).Single(x => x.ApplicationUserID == currentUser.Id);
            }

            else if (Request.Cookies.ContainsKey("cart_id"))
            {
                int existingCartID = int.Parse(Request.Cookies["cart_id"]);
                model = _context.PetCarts.Include(x => x.PetCartProducts).ThenInclude(x => x.Pet).Include(x => x.CorporateCart).
                    Include(x => x.TherapyCart).FirstOrDefault(x => x.ID == existingCartID);

            }

            else
            {
                model = new PetCart();
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.PetCarts
                .SingleOrDefaultAsync(m => m.ID == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }
    }
}

//        // POST: PetsAdmin/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public IActionResult DeleteConfirmed()
//        {
//            //var pet = await _context.Pets.SingleOrDefaultAsync(m => m.ID == id);
//            //_context.Pets.Remove(pet);
//            //await _context.SaveChangesAsync();
//            //return RedirectToAction(nameof(Index));
//            PetCart cart = null;
//            if (Request.Cookies.ContainsKey("cart_id"))
//            {
//                int existingCartID = int.Parse(Request.Cookies["cart_id"]);
//                cart = _context.PetCarts.Include(x => x.PetCartProducts).ThenInclude(x => x.Pet).Include(x => x.CorporateCart).Include(x => x.TherapyCart).FirstOrDefault(x => x.ID == existingCartID);
//                _context.PetCarts.Remove(cart);
//                _context.SaveChanges();
//            }
//                return RedirectToAction(nameof(Index));
//        }

//        private bool PetExists(int existingCartID)
//        {
//                return _context.PetCarts.Include(x => x.PetCartProducts).ThenInclude(x => x.Pet).Include(x => x.CorporateCart).Include(x => x.TherapyCart).Any(x => x.ID == existingCartID);
//        }
//    }
//}