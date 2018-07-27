using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PetTime.Controllers
{
    public class TherapyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(int? id, int quantity)
        {
            //Console.WriteLine("User added" + id.ToString() + " , " + quantity.ToString());
            //TODO: Take the POSTED details and update the users cart
            return RedirectToAction("Index", "Cart");
        }
    }
}