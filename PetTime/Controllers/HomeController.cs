using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetTime.Models;

namespace PetTime.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "About Us";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult About(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                //todo: need to send this to an email address?
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
