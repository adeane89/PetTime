using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetTime.Models
{
    public class CorporateCart
    {
        public int ID { get; set; }
        public int? EventType { get; set; }
        public int? AnimalCount { get; set; }
        public int? Length { get; set; }
        public bool? IsRecurring { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
    }
}
