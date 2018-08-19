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
                        Include(x => x.CorporateCart).Include(x => x.TherapyCart).FirstOrDefault(x => x.ApplicationUserID == currentUser.Id);
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

        //public async Task<IActionResult> Remove()
        //{
        //    if (Request.Cookies.ContainsKey("cart_id"))
        //    {
        //        int existingCartID = int.Parse(Request.Cookies["cart_id"]);
        //        var pet = await _context.PetCartProducts.FirstOrDefaultAsync(m => m.ID == existingCartID);

        //        if (pet == null)
        //        {
        //            _context.PetCartProducts.Remove(pet);
        //        }

        //        _context.PetCartProducts.Remove(pet);
        //    }
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        public async Task<IActionResult> Remove(int id, int quantity, string breed, int timeLength, DateTime startDate, int animalCount, decimal price, string length)
        {
            PetCart cart = null;
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                cart = await _context.PetCarts.Include(x => x.PetCartProducts).FirstOrDefaultAsync(x => x.ApplicationUserID == currentUser.Id);
                if (cart == null)
                {
                    cart = new PetCart();
                    cart.ApplicationUserID = currentUser.Id;
                    cart.DateCreated = DateTime.Now;
                    cart.DateLastModified = DateTime.Now;
                    _context.PetCarts.Add(cart);
                }
            }
            else
            {
                if (Request.Cookies.ContainsKey("cart_id"))
                {
                    int existingCartID = int.Parse(Request.Cookies["cart_id"]);
                    cart = await _context.PetCarts.Include(x => x.PetCartProducts).FirstOrDefaultAsync(x => x.ID == existingCartID);
                    cart.DateLastModified = DateTime.Now;
                }

                if (cart == null)
                {
                    cart = new PetCart
                    {
                        DateCreated = DateTime.Now,
                        DateLastModified = DateTime.Now
                    };

                    _context.PetCarts.Add(cart);
                }
            }
            //find the first product in the cart with the product id we are looking for if none exists, return null
            PetCartProduct product = cart.PetCartProducts.FirstOrDefault(x => x.ID == id);
            
            //had to change x.PetID to x.ID to get the right item to delete
            cart.PetCartProducts.Remove(product);
            if (product != null)
            {
                cart.PetCartProducts.Remove(product);
            }

            await _context.SaveChangesAsync();

            if (!User.Identity.IsAuthenticated)
            {
                Response.Cookies.Append("cart_id", cart.ID.ToString(), new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Expires = DateTime.Now.AddYears(1)
                });
            }

            return RedirectToAction("Index", "Cart");

        }

        public async Task<IActionResult> RemoveCorporate(CorporateCart model)
        {
            PetCart cart = null;
            if (User.Identity.IsAuthenticated)
            {
                //authenticated path
                var currentUser = _userManager.GetUserAsync(User).Result;
                cart = _context.PetCarts.Include(x => x.PetCartProducts).Include(x => x.CorporateCart).FirstOrDefault(x => x.ApplicationUserID == currentUser.Id);
                if (cart == null)
                {
                    cart = new PetCart();
                    cart.ApplicationUserID = currentUser.Id;
                    cart.DateCreated = DateTime.Now;
                    cart.DateLastModified = DateTime.Now;
                    _context.PetCarts.Add(cart);
                }
            }
            else
            {
                if (Request.Cookies.ContainsKey("cart_id"))
                {
                    int existingCartID = int.Parse(Request.Cookies["cart_id"]);
                    cart = _context.PetCarts.Include(x => x.PetCartProducts).Include(x => x.CorporateCart).FirstOrDefault(x => x.ID == existingCartID);
                    cart.DateLastModified = DateTime.Now;
                }

                if (cart == null)
                {
                    cart = new PetCart
                    {
                        DateCreated = DateTime.Now,
                        DateLastModified = DateTime.Now
                    };

                    _context.PetCarts.Add(cart);
                }
            }

            //at this point, the cart is not null = it's either newly created or existing
            if (cart.CorporateCart != null)
            {
                cart.CorporateCart = null; 
            }

            if (!User.Identity.IsAuthenticated)
            {
                Response.Cookies.Append("cart_id", cart.ID.ToString(), new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Expires = DateTime.Now.AddYears(1)
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Cart");
        }

        public async Task<IActionResult> RemoveTherapy(TherapyCart model)
        {
            PetCart cart = null;
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = _userManager.GetUserAsync(User).Result;
                cart = _context.PetCarts.Include(x => x.PetCartProducts).Include(x => x.TherapyCart).FirstOrDefault(x => x.ApplicationUserID == currentUser.Id);
                if (cart == null)
                {
                    cart = new PetCart();
                    cart.ApplicationUserID = currentUser.Id;
                    cart.DateCreated = DateTime.Now;
                    cart.DateLastModified = DateTime.Now;
                    _context.PetCarts.Add(cart);
                }
            }
            else
            {
                if (Request.Cookies.ContainsKey("cart_id"))
                {
                    int existingCartID = int.Parse(Request.Cookies["cart_id"]);
                    cart = _context.PetCarts.Include(x => x.PetCartProducts).Include(x => x.TherapyCart).FirstOrDefault(x => x.ID == existingCartID);
                    cart.DateLastModified = DateTime.Now;
                }

                if (cart == null)
                {
                    cart = new PetCart
                    {
                        DateCreated = DateTime.Now,
                        DateLastModified = DateTime.Now
                    };

                    _context.PetCarts.Add(cart);
                }
            }

            if (cart.TherapyCart != null)
            {
                //cart.PetCartProducts.Remove(product);
                cart.TherapyCart = null;
            }

            await _context.SaveChangesAsync();

            if (!User.Identity.IsAuthenticated)
            {
                Response.Cookies.Append("cart_id", cart.ID.ToString(), new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Expires = DateTime.Now.AddYears(1)
                });
            }

            return RedirectToAction("Index", "Cart");
        }
    }
}
