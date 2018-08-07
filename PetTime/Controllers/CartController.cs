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
                Pets = null
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