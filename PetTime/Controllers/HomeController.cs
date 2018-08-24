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
        private IEmailSender _emailSender;
        private Map _map;

        public HomeController(Map map, IEmailSender emailSender)
        {
            this._map = map;
            this._emailSender = emailSender;
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
        public async Task<IActionResult> About(ContactModel model)
        {
            await _emailSender.SendEmailAsync("adeane89@gmail.com", "Question from " + model.Email + " " + model.Name, model.Question);
            return View("Message");
        }

        public IActionResult Message()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
