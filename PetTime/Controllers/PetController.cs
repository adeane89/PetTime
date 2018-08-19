using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetTime.Data;
using PetTime.Models;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using PetTime.Services;

namespace PetTime.Controllers
{
    public class PetController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private DataScraper _dataScraper;

        public PetController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, DataScraper dataScraper)
        {
            this._context = context;
            this._userManager = userManager;
            this._dataScraper = dataScraper;
        }

        public async Task<IActionResult> Index(string category)
        {
            if (_context.Pets.Count() == 0)
            {
                foreach(var dog in _dataScraper.Scrape())
                {
                    
                    foreach(var breed in dog.breeds)
                    {
                        CategoryModel cat = await _context.Categories.FirstOrDefaultAsync(x => x.Name == breed.name);
                        if(cat == null)
                        {
                            cat = new CategoryModel { Name = breed.name };
                            _context.Categories.Add(cat);
                        }
                        Pet p = new Pet
                        {
                            Name = breed.name,
                            Description = breed.name,
                            ImagePath = dog.url,
                            Price = 5.00m,
                            DateCreated = DateTime.Now,
                            DateLastModified = DateTime.Now,
                            Category = cat
                        };

                        _context.Pets.Add(p);
                        

                    }

                }
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
        public async Task<IActionResult> Details(int? id, int quantity, string breed, int timeLength, DateTime startDate, int animalCount, decimal price, string length)
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
                //make sure cart_id is cased properly everywhere
                if (Request.Cookies.ContainsKey("cart_id"))
                {
                    int existingCartID = int.Parse(Request.Cookies["cart_id"]);
                    cart = await _context.PetCarts.Include(x => x.PetCartProducts).FirstOrDefaultAsync(x => x.ID == existingCartID);
                    //cart.DateLastModified = DateTime.Now;
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
                    TimeLength = timeLength,
                    StartDate = startDate,
                    AnimalCount = 0,
                    Length = length
                 };

                cart.PetCartProducts.Add(product);
            }
            product.Quantity += quantity;
            product.AnimalCount += animalCount;
            product.DateLastModified = DateTime.Now;
            product.StartDate = startDate;
            product.Length = length;

            await _context.SaveChangesAsync ();


            if (!User.Identity.IsAuthenticated)
            {
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
                    //TimeLength = model.TimeLength,
                    Length = model.Length,
                    Price = 10.00m
                };
                cart.CorporateCart = model;
            }
            prod.DateLastModified = DateTime.Now;
            prod.StartDate = model.StartDate;
            //prod.TimeLength = model.TimeLength;
            prod.EventType = model.EventType;
            prod.AnimalCount = model.AnimalCount;
            prod.Price = model.Price;
            prod.Length = model.Length;

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
                    //TimeLength = model.TimeLength,
                    Price = 10.00m
                };
                cart.TherapyCart = model;
            }
            product.DateLastModified = DateTime.Now;
            product.StartDate = model.StartDate;
            //product.TimeLength = model.TimeLength;
            product.EventType = model.EventType;
            product.AnimalCount = model.AnimalCount;
            product.Price = model.Price;
            product.Length = model.Length;

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
