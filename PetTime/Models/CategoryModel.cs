using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetTime.Models
{
    public class CategoryModel
    {
        public CategoryModel()
        {
            this.Pets = new HashSet<Pet>();
        }

        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public ICollection<Pet> Pets { get; set; }
    }
}
