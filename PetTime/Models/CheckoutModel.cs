using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PetTime.Models
{
    public class CheckoutModel
    {
        [Required]
        [EmailAddress]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$]")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
    }
}
