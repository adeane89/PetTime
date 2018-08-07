using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetTime.Data;
using PetTime.Models;

namespace PetTime.Controllers
{
    public class TherapyController : Controller
    {
        private ApplicationDbContext _context;

        public TherapyController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(TherapyCart model)
        {
            _context.TherapyCarts.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index", "Cart");
        }
    }
}