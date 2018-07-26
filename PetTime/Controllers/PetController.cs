using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetTime.Models;

namespace PetTime.Controllers
{
    public class PetController : Controller
    {
        public IActionResult Index(string category)
        {
            ViewBag.selected = category;
            //make sure to put your using statement at the top
            List<Pet> model = new List<Pet>();
            if (string.IsNullOrEmpty(category))
            {
                ViewData["Title"] = "All Products";
                model.Add(new Pet { ID = 1, Name = "Puppy", Description = "Come cuddle with an adorable puppy and forget about your stress!", ImagePath = "./images/puppy.jpg" });
                model.Add(new Pet { ID = 2, Name = "Dog", Description = "Come cuddle with an older dog and relax your time away", ImagePath = "./images/dog.jpg" });
                model.Add(new Pet { ID = 3, Name = "Kitten", Description = "Come cuddle with kitten", ImagePath = "./images/kitten.jpg" });
                model.Add(new Pet { ID = 4, Name = "Cat", Description = "Come cuddle with cat", ImagePath = "./images/cat.jpg" });

                Console.WriteLine("Get All Products");
            }
            else if (category.ToLowerInvariant() == "dogs")
            {
                ViewData["Title"] = "Dog Services Avaliable";
                model.Add(new Pet { ID = 1, Name = "Puppy", Description = "Come cuddle with an adorable puppy and forget about your stress!", ImagePath = "./images/puppy.jpg" });
                model.Add(new Pet { ID = 2, Name = "Dog", Description = "Come cuddle with an older dog and relax your time away", ImagePath = "./images/dog.jpg" });
                Console.WriteLine("Get All Dogs");
            }
            else if (category.ToLowerInvariant() == "cats")
            {
                ViewData["Title"] = "Cat Services Avaliable";
                model.Add(new Pet { ID = 3, Name = "Kitten", Description = "Come cuddle with kitten", ImagePath = "./images/kitten.jpg" });
                model.Add(new Pet { ID = 4, Name = "Cat", Description = "Come cuddle with cat", ImagePath = "./images/cat.jpg" });
                Console.WriteLine("Get All Cats");
            }
            return View(model);
        }

        public IActionResult Details(int? id)
        {
            Pet model = new Pet
            {
                ID = 1,
                Name = "puppy",
                Description = "puppy",
                ImagePath = "./images/puppy.jpg"
            };
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
    }
}