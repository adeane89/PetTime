using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Braintree;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetTime.Data;
using PetTime.Models;
using PetTime.Services;

namespace PetTime.Controllers
{
    public class CheckoutController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private IEmailSender _emailSender;
        private IBraintreeGateway _braintreeGateway;

        public CheckoutController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender, IBraintreeGateway braintreeGateway)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
            _braintreeGateway = braintreeGateway;
        }

        public async Task<IActionResult> Index()
        {
            CheckoutModel model = new CheckoutModel();
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                model.Email = currentUser.Email;
            }

            ViewBag.ClientAuthorization = await _braintreeGateway.ClientToken.GenerateAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CheckoutModel model, string nonce)
        {
            if(ModelState.IsValid)
            {
                PetOrder order = new PetOrder
                {
                    City = model.City,
                    State = model.State,
                    Email = model.Email,
                    StreetAddress = model.StreetAddress,
                    ZipCode = model.ZipCode,
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now

                };

                PetCart cart = null;

                if (User.Identity.IsAuthenticated)
                {
                    var currentUser = _userManager.GetUserAsync(User).Result;
                    cart = _context.PetCarts.Include(x => x.PetCartProducts).ThenInclude(x => x.Pet).Single(x => x.ApplicationUserID == currentUser.Id);
                }

                else if (Request.Cookies.ContainsKey("cart_id"))
                {
                    int existingCartID = int.Parse(Request.Cookies["cart_id"]);
                    cart = _context.PetCarts.Include(x => x.PetCartProducts).ThenInclude(x => x.Pet).FirstOrDefault(x => x.ID == existingCartID);
                }

                foreach (var cartItem in cart.PetCartProducts)
                {
                    order.PetOrderProducts.Add(new PetOrderProduct
                    {
                        DateCreated = DateTime.Now,
                        DateLastModified = DateTime.Now,
                        Quantity = cartItem.Quantity ?? 1,
                        ProductID = cartItem.PetID,
                        ProductDescription = cartItem.Pet.Description,
                        ProductName = cartItem.Pet.Name,
                        ProductPrice = cartItem.Pet.Price ?? 0,
                    });
                }

                _context.PetCartProducts.RemoveRange(cart.PetCartProducts);
                _context.PetCarts.Remove(cart);
                if (Request.Cookies.ContainsKey("cart_id"))
                {
                    Response.Cookies.Delete("cart_id");
                }
                _context.PetOrders.Add(order);
                _context.SaveChanges();

                var result = await _braintreeGateway.Transaction.SaleAsync(new TransactionRequest
                {
                    Amount = order.PetOrderProducts.Sum(x => (x.Quantity * x.ProductPrice)),
                    PaymentMethodNonce = nonce
                });

                await _emailSender.SendEmailAsync(model.Email, "Your order " + order.ID, 
                    "Thanks for ordering! You scheduled :" + 
                    String.Join(",", order.PetOrderProducts.Select(x => x.ProductName)) + 
                    String.Join(",", order.PetOrderProducts.Select(x => x.Quantity)) + 
                    String.Join(",", order.PetOrderProducts.Select(x => x.ProductDescription)));
                
                return RedirectToAction("Index", "Receipt", new { id = order.ID });
            }
            return View();
        }
    }
}