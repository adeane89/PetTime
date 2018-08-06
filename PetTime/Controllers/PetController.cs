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
                List<Pet> newPets = new List<Pet>();
                newPets.Add(new Pet { Name = "Puppy", Description = "Golden Retreiver", ImagePath = "./images/puppy1.jpg" });
                newPets.Add(new Pet { Name = "Puppy1", Description = "Golden Retreiver", ImagePath = "./images/puppy1.jpg" });
                newPets.Add(new Pet { Name = "Puppy2", Description = "Golden Retreiver", ImagePath = "./images/puppy1.jpg" });
                newPets.Add(new Pet { Name = "Puppy3", Description = "Golden Retreiver", ImagePath = "./images/puppy1.jpg" });
                newPets.Add(new Pet { Name = "Puppy4", Description = "Golden Retreiver", ImagePath = "./images/puppy1.jpg" });
                newPets.Add(new Pet { Name = "Puppy5", Description = "Golden Retreiver", ImagePath = "./images/puppy1.jpg" });
                newPets.Add(new Pet { Name = "Puppy6", Description = "Golden Retreiver", ImagePath = "./images/puppy1.jpg" });
                //repeat this many times

                _context.Pets.AddRange(newPets); //add range does not immediately insert these records. it just queues them up

                _context.SaveChanges(); //anytime you update, delete, or add records, you need to call save changes afterwards. the sql statement wil run at this point, not before
            }

                List<Pet> model = this._context.Pets.ToList();
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
