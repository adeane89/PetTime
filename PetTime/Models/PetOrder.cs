using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetTime.Models
{
    public class PetOrder
    { 
        public PetOrder()
        {
            this.PetOrderProducts = new HashSet<PetOrderProduct>();
        }
        public Guid ID { get; set; }
        public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public ICollection<PetOrderProduct> PetOrderProducts { get; set; }

    }
}
