﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetTime.Models;
using Microsoft.EntityFrameworkCore;
using PetTime.Data;
using Microsoft.AspNetCore.Identity;

namespace PetTime.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public CartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            PetCart model = null;
            if(User.Identity.IsAuthenticated)
            {
                var currentUser = _userManager.GetUserAsync(User).Result;
                model = _context.PetCarts.Include(x => x.PetCartProducts).ThenInclude(x => x.Pet).Include(x => x.CorporateCart).Include(x => x.TherapyCart).Single(x => x.ApplicationUserID == currentUser.Id);
            }
            else if (Request.Cookies.ContainsKey("cart_id"))
            {
                int existingCartID = int.Parse(Request.Cookies["cart_id"]);
                model = _context.PetCarts.Include(x => x.PetCartProducts).ThenInclude(x => x.Pet).Include(x => x.CorporateCart).Include(x => x.TherapyCart).FirstOrDefault(x => x.ID == existingCartID);
            
            }
            else
            {
                model = new PetCart();
            }

            return View(model);
        }

        public async Task<IActionResult> Remove(int? id, int quantity, string breed, string length, int animalCount)
        {
            PetCart cart = null;
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                cart = await _context.PetCarts.Include(x => x.PetCartProducts).FirstOrDefaultAsync(x => x.ApplicationUserID == currentUser.Id);
                if (cart == null)
                {
                    cart = new PetCart();
                    cart.ApplicationUserID = currentUser.Id;
                    cart.DateCreated = DateTime.Now;
                    cart.DateLastModified = DateTime.Now;
                    _context.PetCarts.Add(cart);
                }
            }
            else
            {
                if (Request.Cookies.ContainsKey("cart_id"))
                {
                    int existingCartID = int.Parse(Request.Cookies["cart_id"]);
                    cart = await _context.PetCarts.Include(x => x.PetCartProducts).FirstOrDefaultAsync(x => x.ID == existingCartID);
                }

                if (cart == null)
                {
                    cart = new PetCart
                    {
                        DateCreated = DateTime.Now,
                        DateLastModified = DateTime.Now
                    };

                    _context.PetCarts.Add(cart);
                }
            }

            PetCartProduct product = cart.PetCartProducts.FirstOrDefault(x => x.PetID == id);
            if (product != null)
            {
                product = new PetCartProduct
                {
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now,
                    PetID = id ?? 0,
                    Quantity = 0,
                    Length = length,
                    AnimalCount = 0
                };

                cart.PetCartProducts.Remove(product);
            }
            //product.Quantity= 0;
            //product.AnimalCount = animalCount;
            //product.DateLastModified = DateTime.Now;
            //product.Length = length;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Cart");
        }


        //public IActionResult Remove(/*int id, int quantity, int animalCount, DateTime dateLastModified*/)
        //{
        //    //PetCart cart = null;
        //    //PetCartProduct product = cart.PetCartProducts.FirstOrDefault(x => x.PetID == id);
        //    //if (product != null)
        //    //{
        //    //    cart.PetCartProducts.Remove(product);
        //    //}
        //    //product.Quantity = 0;
        //    //product.AnimalCount = 0;
        //    //product.DateLastModified = DateTime.Now;

        //    //await _context.SaveChangesAsync();

        //    return RedirectToAction("Index", "Cart");
        //}
    }
}