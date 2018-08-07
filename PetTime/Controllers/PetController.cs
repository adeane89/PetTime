using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetTime.Data;
using PetTime.Models;

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
        [ValidateAntiForgeryToken]
        public IActionResult Details(int? id, int quantity, string breed)
        {
            Console.WriteLine("User added" + id.ToString() + " , " + quantity.ToString());
            //TODO: Take the POSTED details and update the users cart
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Corporate()
        {
            ViewData["Message"] = "Your corporate page.";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Corporate(int? id, int quantity)
        {
            //Console.WriteLine("User added" + id.ToString() + " , " + quantity.ToString());
            //TODO: Take the POSTED details and update the users cart
            return RedirectToAction("Index", "Cart");
        }
    }
}
