using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetTime.Data;
using PetTime.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace PetTime.Controllers
{
    public class PetController : Controller
    {
        private ApplicationDbContext _context;

        private UserManager<ApplicationUser> _userManager;

        public PetController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public async Task<IActionResult> Index(string category)
        {
            if (_context.Pets.Count() == 0)
            {
                List<Pet> springerSpaniel = new List<Pet>();
                springerSpaniel.Add(new Pet { Name = "English Springer Spaniel", Description = "English Springer Spaniel", ImagePath = "/images/puppy3.jpg", Price = 5.00m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                _context.Categories.Add(new CategoryModel { Name = "English Springer Spaniel", Pets = springerSpaniel });

                List<Pet> retrievers = new List<Pet>();
                retrievers.Add(new Pet { Name = "Golden Retriever", Description = "Golden Retriever", ImagePath = "/images/golden.jpg", Price = 5.00m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                _context.Categories.Add(new CategoryModel { Name = "Golden Retriever", Pets = retrievers });

                List<Pet> corgis = new List<Pet>();
                corgis.Add(new Pet { Name = "Corgi", Description = "Corgi", ImagePath = "/images/puppy4.jpg", Price = 5.00m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                _context.Categories.Add(new CategoryModel { Name = "Corgi", Pets = corgis });

               await _context.SaveChangesAsync();
            }
            
            ViewBag.selectedCategory = category;
            List<Pet> model;
            if (String.IsNullOrEmpty(category))
            {
                model = await this._context.Pets.ToListAsync();
            }
            else
            {
                model = await this._context.Pets.Where(x => x.CategoryModelName == category).ToListAsync();
            }
            
            ViewData["Categories"] = await this._context.Categories.Select(x => x.Name).ToArrayAsync();

            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            Pet model = await _context.Pets.FindAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Details(int? id, int quantity, string breed, string length, DateTime startDate, int animalCount)
        {
            PetCart cart = null;
            if (User.Identity.IsAuthenticated)
            {
                //authenticated path
                var currentUser = _userManager.GetUserAsync(User).Result;
                cart = _context.PetCarts.Include(x => x.PetCartProducts).FirstOrDefault(x => x.ApplicationUserID == currentUser.Id);
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
                //make sure cart_id is cased properly everywhere
                if (Request.Cookies.ContainsKey("cart_id"))
                {
                    int existingCartID = int.Parse(Request.Cookies["cart_id"]);
                    //Using Microsoft.EntityFramework.Core (has the include)
                    cart = _context.PetCarts.Include(x => x.PetCartProducts).FirstOrDefault(x => x.ID == existingCartID);
                    //cart = _context.PetCarts.Find(existingCartID);
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

            //find the first product in the cart with the prodcut id we are looking for if none exists, return null
            PetCartProduct product = cart.PetCartProducts.FirstOrDefault(x => x.PetID == id);
            if (product == null)
            {
                product = new PetCartProduct
                {
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now,
                    PetID = id ?? 0,
                    Quantity = 0,
                    Length = length,
                    StartDate = startDate,
                    AnimalCount = 0
    };
                cart.PetCartProducts.Add(product);
            }
            product.Quantity += quantity;
            product.AnimalCount += animalCount;
            product.DateLastModified = DateTime.Now;
            product.StartDate = startDate;
            product.Length = length;
            
            await _context.SaveChangesAsync();


            if (!User.Identity.IsAuthenticated)
            {
                //at the end of this page, set the cookie!
                Response.Cookies.Append("cart_id", cart.ID.ToString(), new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Expires = DateTime.Now.AddYears(1)
                });
            }

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Corporate()
        {
            ViewData["Message"] = "Your corporate page.";
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Corporate(CorporateCart model)
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
                    cart = _context.PetCarts.Include(x => x.PetCartProducts).FirstOrDefault(x => x.ID == existingCartID);
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

            //find the first product in the cart with the prodcut id we are looking for if none exists, return null
            CorporateCart prod = cart.CorporateCart;
           
            if (prod == null)
            {
                prod = new CorporateCart
                {
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now,
                    AnimalCount = model.AnimalCount,
                    StartDate = model.StartDate,
                    EventType = model.EventType,
                    Length = model.Length
                };
                cart.CorporateCart = model;
            }
            prod.DateLastModified = DateTime.Now;
            prod.StartDate = model.StartDate;
            prod.Length = model.Length;
            prod.EventType = model.EventType;
            prod.AnimalCount = model.AnimalCount;

            if (!User.Identity.IsAuthenticated)
            {
                //at the end of this page, set the cookie!
                Response.Cookies.Append("cart_id", cart.ID.ToString(), new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Expires = DateTime.Now.AddYears(1)
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Cart");
        }
         public IActionResult Therapy()
        {
            ViewData["Message"] = "Your therapy page.";
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Therapy(TherapyCart model)
        {
            PetCart cart = null;
            if (User.Identity.IsAuthenticated)
            {
                //authenticated path
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
                    cart = _context.PetCarts.Include(x => x.PetCartProducts).FirstOrDefault(x => x.ID == existingCartID);
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

            TherapyCart product = cart.TherapyCart;
           
            if (product == null)
            {
                product = new TherapyCart
                {
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now,
                    AnimalCount = model.AnimalCount,
                    StartDate = model.StartDate,
                    EventType = model.EventType,
                    Length = model.Length
                };
                cart.TherapyCart = model;
            }
            product.DateLastModified = DateTime.Now;
            product.StartDate = model.StartDate;
            product.Length = model.Length;
            product.EventType = model.EventType;
            product.AnimalCount = model.AnimalCount;

            if (!User.Identity.IsAuthenticated)
            {
                //at the end of this page, set the cookie!
                Response.Cookies.Append("cart_id", cart.ID.ToString(), new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Expires = DateTime.Now.AddYears(1)
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Cart");
        }
    }
}
