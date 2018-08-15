using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetTime.Models;

using PetTime.Services;

namespace PetTime.Controllers
{
    public class HomeController : Controller
    {
        private Map _map;

        public HomeController(Map map)
        {
            this._map = map;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About(Map apiKey)
        {
            ViewBag.ApiKey = _map.MapKey;
            ViewData["Message"] = "About Us";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult About(ContactModel model)
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
