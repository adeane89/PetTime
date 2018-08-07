using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetTime.Models
{
    public class PetCartProduct
    {
        public int ID { get; set; }
        public PetCart PetCart { get; set; }
        public int PetCartID { get; set; }
        public Pet Pet { get; set; }
        public int PetProductID { get; set; }
        public int? Quantity { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
    }
}
