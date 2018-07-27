﻿using System;
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
                model.Add(new Pet { ID = 1, Name = "Puppy", Description = "Goldendoodle", ImagePath = "./images/puppy2.jpg" });
                model.Add(new Pet { ID = 2, Name = "Puppy", Description = "English springer spaniel", ImagePath = "./images/puppy3.jpg" });
                model.Add(new Pet { ID = 3, Name = "Puppy", Description = "Corgi", ImagePath = "./images/puppy4.jpg" });
                model.Add(new Pet { ID = 4, Name = "Puppy", Description = "Labrador Retriever", ImagePath = "./images/puppy5.jpg" });
                
                model.Add(new Pet { ID = 5, Name = "Kitten", Description = "Come cuddle with kitten", ImagePath = "./images/kitten.jpg" });
                model.Add(new Pet { ID = 6, Name = "Cat", Description = "Come cuddle with cat", ImagePath = "./images/cat.jpg" });

                Console.WriteLine("Get All Products");
            }
            else if (category.ToLowerInvariant() == "dogs")
            {
                ViewData["Title"] = "Dog Services Avaliable";
                model.Add(new Pet { ID = 1, Name = "Puppy", Description = "Goldendoodle", ImagePath = "./images/puppy2.jpg" });
                model.Add(new Pet { ID = 2, Name = "Puppy", Description = "English springer spaniel", ImagePath = "./images/puppy3.jpg" });
                model.Add(new Pet { ID = 3, Name = "Puppy", Description = "Corgi", ImagePath = "./images/puppy4.jpg" });
                model.Add(new Pet { ID = 4, Name = "Puppy", Description = "Labrador Retriever", ImagePath = "./images/puppy5.jpg" });
                Console.WriteLine("Get All Dogs");
            }
            else if (category.ToLowerInvariant() == "cats")
            {
                ViewData["Title"] = "Cat Services Avaliable";
                model.Add(new Pet { ID = 5, Name = "Kitten", Description = "Come cuddle with kitten", ImagePath = "./images/kitten.jpg" });
                model.Add(new Pet { ID = 6, Name = "Cat", Description = "Come cuddle with cat", ImagePath = "./images/cat.jpg" });
                Console.WriteLine("Get All Cats");
            }
            return View(model);
        }

        public IActionResult Details(int? id)
        {
            Pet model = new Pet
            {
                ID = 1,
                Name = "Animal",
                Description = "animal",
                ImagePath = "./images/puppycat.jpg"
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