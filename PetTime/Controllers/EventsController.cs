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
    public class EventsController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private IEmailSender _emailSender;
        private IBraintreeGateway _braintreeGateway;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public EventsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender, IBraintreeGateway braintreeGateway, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
            _signInManager = signInManager;
            _braintreeGateway = braintreeGateway;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> Register ()
        {
            RegistrationModel model = new RegistrationModel();
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                model.Email = currentUser.Email;
            }
            ViewData["Message"] = "Register";
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                await _emailSender.SendEmailAsync(model.Email, "Your scheduled visit!",
                    "Thank you " + model.Name + " for registering for: " + model.RegistrationEvent);
                return RedirectToAction("Message");
            }
            return View();
        }

        public IActionResult Message()
        {
            return View();
        }


    }
}