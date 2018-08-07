using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetTime.Data;
using PetTime.Models;
using Microsoft.EntityFrameworkCore;

namespace PetTime.Controllers
{
    public class PetController : Controller
    {
        private ApplicationDbContext _context;

        public PetController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index(string category)
        {
            if (_context.Pets.Count() == 0)
            {
                List<Pet> doodles = new List<Pet>();
                doodles.Add(new Pet { Name = "Puppy", Description = "Goldendoodle", ImagePath = "./images/puppy1.jpg" });
                _context.Categories.Add(new CategoryModel { Name = "Goldendoodle", Pets = doodles });

                List<Pet> retreivers = new List<Pet>();
                retreivers.Add(new Pet { Name = "Golden Retreiver", Description = "Golden Retreiver", ImagePath = "./images/puppy.jpg" });
                _context.Categories.Add(new CategoryModel { Name = "Golden Retreiver", Pets = retreivers });

                _context.SaveChanges();
            }
            
            ViewBag.selectedCategory = category;
            List<Pet> model;
            if (String.IsNullOrEmpty(category))
            {
                model = this._context.Pets.ToList();
            }
            else
            {
                model = this._context.Pets.Where(x => x.CategoryModelName == category).ToList();
            }
            
            ViewData["Categories"] = this._context.Categories.Select(x => x.Name).ToArray();

            return View(model);
        }

        public IActionResult Details(int? id)
        {
            Pet model = _context.Pets.Find(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Details(int? id, int quantity, string breed)
        {
            PetCart cart = null;
            //make sure cart_id is cased properly everywhere!

            if(Request.Cookies.ContainsKey("cart_id"))
            {
                int existingCartID = int.Parse(Request.Cookies["cart_id"]);
                //Using Microsoft.EntityFramework.Core (has the include)
                cart = _context.PetCarts.Include(x => x.PetCartProducts).FirstOrDefault(x => x.ID == existingCartID);
                //cart = _context.PetCarts.Find(existingCartID);
            }
            if(cart == null)
            {
                cart = new PetCart
                {
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now
                };

                _context.PetCarts.Add(cart);
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
                    Quantity = quantity
                };
                cart.PetCartProducts.Add(product);
            }
            product.Quantity += quantity;
            product.DateLastModified = DateTime.Now;
            
            _context.SaveChanges();

            //at the end of this page, set the cookie!
            Response.Cookies.Append("cart_id", cart.ID.ToString(), new Microsoft.AspNetCore.Http.CookieOptions
            {
                Expires = DateTime.Now.AddYears(1)
            });

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Corporate()
        {
            ViewData["Message"] = "Your corporate page.";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Corporate(CorporateCart model)
        {
            _context.CorporateCarts.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index", "Cart");
        }
    }
}
