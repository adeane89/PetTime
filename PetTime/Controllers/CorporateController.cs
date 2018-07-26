using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PetTime.Controllers
{
    public class CorporateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

//what kind of visit is this? 
//school, college, business, party?
//what kind of animals?
//checkbox cats or dogs?
//how many animals?
//1-10?
//how long of an event?
//1-3 hrs
//is this a one time or recurring event?
//checkbox? one time or recurring
//pick a time?
//time and date?
//price based on animals, time length