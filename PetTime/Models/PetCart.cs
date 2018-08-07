using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetTime.Models
{
    public class PetCart
    {
        public PetCart()
        {
            this.PetCartProducts = new HashSet<PetCartProduct>();
        }

        public CorporateCart CorporateCart { get; set; }

        public int? CorporateCartID { get; set; }

        public TherapyCart TherapyCart { get; set; }

        public int? TherapyCartID { get; set; }

        public int ID { get; set; }
        public ICollection<PetCartProduct> PetCartProducts { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
    }
}
