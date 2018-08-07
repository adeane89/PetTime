using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetTime.Models;
using Microsoft.EntityFrameworkCore;
using PetTime.Data;

namespace PetTime.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            PetCart model = null;
            if (Request.Cookies.ContainsKey("cart_id"))
            {
                int existingCartID = int.Parse(Request.Cookies["cart_id"]);
                model = _context.PetCarts.Include(x => x.PetCartProducts).ThenInclude(x => x.Pet).FirstOrDefault(x => x.ID == existingCartID);
            }
            else
            {
                model = new PetCart();
            }

            return View(model);
        }

        public IActionResult Remove(int id)
        {
            //look through the card items to remove the id that is removed 
            return RedirectToAction("Index");
        }
    }
}