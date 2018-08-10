using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetTime.Models
{
    public class Pet
    {
        public Pet()
        {
            this.PetCartProducts = new HashSet<PetCartProduct>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string ImagePath { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime DateTime { get; set; }
        public int? EventType { get; set; }
        public int? AnimalCount { get; set; }
        public int? Length { get; set; }
        public int Age { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public CategoryModel Category { get; set; }
        public string CategoryModelName { get; set; }
        public ICollection<PetCartProduct> PetCartProducts { get; set; }
    }
}
