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
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CheckoutController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender, IBraintreeGateway braintreeGateway, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
            _signInManager = signInManager;
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
        public async Task<IActionResult> Index(CheckoutModel model, string nonce, CorporateCart mod, TherapyCart models)
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
                    cart = _context.PetCarts.Include(x => x.PetCartProducts).ThenInclude(x => x.Pet).Include(x => x.CorporateCart).Include(x => x.TherapyCart).Single(x => x.ApplicationUserID == currentUser.Id);
                }

                else if (Request.Cookies.ContainsKey("cart_id"))
                {
                    int existingCartID = int.Parse(Request.Cookies["cart_id"]);
                    cart = _context.PetCarts.Include(x => x.PetCartProducts).ThenInclude(x => x.Pet).Include(x => x.CorporateCart).Include(x => x.TherapyCart).FirstOrDefault(x => x.ID == existingCartID);
                }

                if(cart.PetCartProducts != null)
                {
                    foreach (var cartItem in cart.PetCartProducts)
                    {
                        order.PetOrderProducts.Add(new PetOrderProduct
                        {
                            DateCreated = DateTime.Now,
                            DateLastModified = DateTime.Now,
                            Quantity = cartItem.Quantity ?? 1,
                            ProductID = cartItem.PetID,
                            ProductDescription = cartItem.Length,
                            ProductName = cartItem.Pet.Name,
                            ProductPrice = ((decimal)(cartItem.Pet.Price * cartItem.AnimalCount)),
                            ProductAnimalCount = cartItem.AnimalCount,
                            StartDate = cartItem.StartDate,
                            ProductEventType = "Individual"
                        });
                    }
                }

                if(cart.CorporateCart != null)
                {
                    order.PetOrderProducts.Add(new PetOrderProduct
                    {
                        DateCreated = DateTime.Now,
                        DateLastModified = DateTime.Now,
                        ProductID = cart.CorporateCartID,
                        ProductPrice = ((decimal)(10 * cart.CorporateCart.AnimalCount)),
                        ProductAnimalCount = cart.CorporateCart.AnimalCount ?? 1,
                        ProductEventType = cart.CorporateCart.EventType,
                        ProductLength = cart.CorporateCart.TimeLength,
                        StartDate = cart.CorporateCart.StartDate
                    });
                }

                if (cart.TherapyCart != null)
                {
                    order.PetOrderProducts.Add(new PetOrderProduct
                    {
                        DateCreated = DateTime.Now,
                        DateLastModified = DateTime.Now,
                        ProductID = cart.TherapyCartID,
                        ProductPrice = ((decimal)(10 * cart.TherapyCart.AnimalCount)),
                        ProductAnimalCount = cart.TherapyCart.AnimalCount ?? 1,
                        ProductEventType = cart.TherapyCart.EventType,
                        ProductLength = cart.TherapyCart.TimeLength,
                        StartDate = cart.TherapyCart.StartDate
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

                await _emailSender.SendEmailAsync(model.Email, "Your scheduled visit!" ,
                    "Thanks for ordering! Your order number is: " + order.ID + " You scheduled : " + 
                    String.Join(" , ", order.PetOrderProducts.Select(x => x.ProductName)) + ", " +
                    String.Join(" , ", order.PetOrderProducts.Select(x => x.ProductEventType)) + ", " +
                    String.Join(" , ", order.PetOrderProducts.Select(x => x.StartDate)) + ", " +
                    String.Join(" , ", order.PetOrderProducts.Select(x => x.ProductLength)) + ", " +
                    String.Join(" , ", order.PetOrderProducts.Select(x => x.ProductAnimalCount)) + "puppies." +
                    " Your total payment is : " + 
                    String.Join(" , ", order.PetOrderProducts.Sum((x => ((x.Quantity * x.ProductPrice) + (x.ProductAnimalCount * x.ProductPrice)))).ToString("c")));
                
                return RedirectToAction("Index", "Receipt", new { id = order.ID });
            }
            return View();
        }
    }
}