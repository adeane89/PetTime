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
            this.Pets = new HashSet<Pet>();
        }
        public int ID { get; set; }
        //Collection is more flexible than Pet[]
        //this will help in subsequent work to move to database
        public ICollection<Pet> Pets { get; set; }


    }
}
