using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetTime.Models;

namespace PetTime.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CheckoutModel model)
        {
            if(ModelState.IsValid)
            {
                //todo: save this info to the database so we can ship it
                return RedirectToAction("Index", "Receipt", new { id = Guid.NewGuid() });
            }
            return View();
        }
    }
}