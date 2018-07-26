using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetTime.Models;

namespace PetTime.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            PetCart model = new PetCart
            {
                ID = 1,
                Pets = new Pet[]
                {
                   new Pet
                   {
                       ID = 1,
                       Name = "Puppy",
                       Description = "Come cuddle with an adorable puppy and forget about your stress!",
                       ImagePath = "./images/puppy.jpg"
                   },

                   new Pet
                   {
                        ID = 2,
                        Name = "Dog",
                        Description = "Come cuddle with an older dog and relax your time away",
                        ImagePath = "./images/dog.jpg"
                   }
                }
            };
            return View(model);
        }

        public IActionResult Remove(int id)
        {
            //look through the card items to remove the id that is removed 
            return RedirectToAction("Index");

        }
    }
}