using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetTime.Data;
using PetTime.Models;

namespace PetTime.Controllers
{
    public class CheckoutController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public CheckoutController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            CheckoutModel model = new CheckoutModel();
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = _userManager.GetUserAsync(User).Result;
                model.Email = currentUser.Email;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CheckoutModel model)
        {
            if(ModelState.IsValid)
            {
                PetOrder order = new PetOrder
                {
                    City = model.City,
                    State = model.State,
                    Email = model.Email,
                    StreetAddress = model.StreetAddress,
                    ZipCode = model.ZipCode,
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now

                };

                PetCart cart = null;

                if (User.Identity.IsAuthenticated)
                {
                    var currentUser = _userManager.GetUserAsync(User).Result;
                    cart = _context.PetCarts.Include(x => x.PetCartProducts).ThenInclude(x => x.Pet).Single(x => x.ApplicationUserID == currentUser.Id);
                }

                else if (Request.Cookies.ContainsKey("cart_id"))
                {
                    int existingCartID = int.Parse(Request.Cookies["cart_id"]);
                    cart = _context.PetCarts.Include(x => x.PetCartProducts).ThenInclude(x => x.Pet).FirstOrDefault(x => x.ID == existingCartID);
                }

                foreach (var cartItem in cart.PetCartProducts)
                {
                    order.PetOrderProducts.Add(new PetOrderProduct
                    {
                        DateCreated = DateTime.Now,
                        DateLastModified = DateTime.Now,
                        ProductID = cartItem.PetID,
                        ProductDescription = cartItem.Pet.Description,
                        ProductName = cartItem.Pet.Name,
                        ProductPrice = cartItem.Pet.Price ?? 0,
                        ProductAnimalCount = cartItem.Pet.AnimalCount,
                        ProductEventType = cartItem.Pet.EventType,
                        ProductLength = cartItem.Pet.Length
                    });
                }

                _context.PetCartProducts.RemoveRange(cart.PetCartProducts);
                _context.PetCarts.Remove(cart);
                if (Request.Cookies.ContainsKey("cart_id"))
                {
                    Response.Cookies.Delete("cart_id");
                }
                _context.PetOrders.Add(order);
                _context.SaveChanges();

                //todo: save this info to the database so we can ship it
                return RedirectToAction("Index", "Receipt", new { id = order.ID });
            }
            return View();
        }
    }
}