using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetTime.Models
{
    public class TherapyCart
    {
        public int ID { get; set; }
        public string EventType { get; set; }
        public int? AnimalCount { get; set; }
        public string Length { get; set; }
        public bool? IsRecurring { get; set; }
        public DateTime? StartDate { get; set; }
        public string Instructions { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
    }
}
