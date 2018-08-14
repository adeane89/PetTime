using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetTime.Models
{
    public class PetOrderProduct
    {
        public Guid ID { get; set; }

        public PetOrder PetOrder { get; set; }

        public Guid PetOrderID { get; set; }
        
        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public decimal ProductPrice { get; set; }

        public int? ProductID { get; set; }
        
        public DateTime? StartDate { get; set; }

        public int? ProductEventType { get; set; }

        public int ProductAnimalCount { get; set; }

        public int Quantity { get; set; }

        public int? ProductLength { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateLastModified { get; set; }
    }
}
