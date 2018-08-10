using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetTime.Models;

namespace PetTime.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Pet> Pets { get; set; }

        public DbSet<CategoryModel> Categories { get; set; }

        public DbSet<PetCart> PetCarts { get; set; }

        public DbSet<PetCartProduct> PetCartProducts { get; set; }

        public DbSet<CorporateCart> CorporateCarts { get; set; }

        public DbSet<TherapyCart> TherapyCarts { get; set; }

        public DbSet<PetOrder> PetOrders { get; set; }

        public DbSet<PetOrderProduct> PetOrderProducts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CategoryModel>().HasKey(x => x.Name);

            builder.Entity<CategoryModel>().Property(x => x.DateCreated).HasDefaultValueSql("GetDate()");
            builder.Entity<CategoryModel>().Property(x => x.DateLastModified).HasDefaultValueSql("GetDate()");
            builder.Entity<CategoryModel>().Property(x => x.Name).HasMaxLength(100);

            builder.Entity<ApplicationUser>().HasOne(x => x.PetCart).WithOne(x => x.ApplicationUser).HasForeignKey<PetCart>(x => x.ApplicationUserID);
        }
    }
}
