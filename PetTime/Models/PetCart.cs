﻿using System;
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
        public ICollection<Pet> Pets { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
    }
}
