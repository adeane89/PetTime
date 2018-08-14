﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using PetTime.Data;
using System;

namespace PetTime.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("PetTime.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<int>("PetCartID");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("PetTime.Models.CategoryModel", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GetDate()");

                    b.Property<DateTime?>("DateLastModified")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GetDate()");

                    b.HasKey("Name");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("PetTime.Models.CorporateCart", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AnimalCount");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateLastModified");

                    b.Property<string>("EventType");

                    b.Property<bool?>("IsRecurring");

                    b.Property<string>("Length");

                    b.Property<DateTime?>("StartDate");

                    b.HasKey("ID");

                    b.ToTable("CorporateCarts");
                });

            modelBuilder.Entity("PetTime.Models.Pet", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<string>("CategoryModelName");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateLastModified");

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Description");

                    b.Property<string>("ImagePath");

                    b.Property<string>("Name");

                    b.Property<decimal?>("Price");

                    b.HasKey("ID");

                    b.HasIndex("CategoryModelName");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("PetTime.Models.PetCart", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserID");

                    b.Property<int?>("CorporateCartID");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateLastModified");

                    b.Property<int?>("PetID");

                    b.Property<int?>("TherapyCartID");

                    b.HasKey("ID");

                    b.HasIndex("ApplicationUserID")
                        .IsUnique()
                        .HasFilter("[ApplicationUserID] IS NOT NULL");

                    b.HasIndex("CorporateCartID");

                    b.HasIndex("PetID");

                    b.HasIndex("TherapyCartID");

                    b.ToTable("PetCarts");
                });

            modelBuilder.Entity("PetTime.Models.PetCartProduct", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnimalCount");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateLastModified");

                    b.Property<string>("Length");

                    b.Property<int>("PetCartID");

                    b.Property<int>("PetID");

                    b.Property<int?>("Quantity");

                    b.Property<DateTime?>("StartDate");

                    b.HasKey("ID");

                    b.HasIndex("PetCartID");

                    b.HasIndex("PetID");

                    b.ToTable("PetCartProducts");
                });

            modelBuilder.Entity("PetTime.Models.PetOrder", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateLastModified");

                    b.Property<string>("Email");

                    b.Property<string>("State");

                    b.Property<string>("StreetAddress");

                    b.Property<string>("ZipCode");

                    b.HasKey("ID");

                    b.ToTable("PetOrders");
                });

            modelBuilder.Entity("PetTime.Models.PetOrderProduct", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateLastModified");

                    b.Property<Guid>("PetOrderID");

                    b.Property<int>("ProductAnimalCount");

                    b.Property<string>("ProductDescription");

                    b.Property<int?>("ProductEventType");

                    b.Property<int?>("ProductID");

                    b.Property<int?>("ProductLength");

                    b.Property<string>("ProductName");

                    b.Property<decimal>("ProductPrice");

                    b.Property<int>("Quantity");

                    b.Property<DateTime?>("StartDate");

                    b.HasKey("ID");

                    b.HasIndex("PetOrderID");

                    b.ToTable("PetOrderProducts");
                });

            modelBuilder.Entity("PetTime.Models.TherapyCart", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AnimalCount");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateLastModified");

                    b.Property<string>("EventType");

                    b.Property<string>("Instructions");

                    b.Property<bool?>("IsRecurring");

                    b.Property<string>("Length");

                    b.Property<DateTime?>("StartDate");

                    b.HasKey("ID");

                    b.ToTable("TherapyCarts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("PetTime.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PetTime.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PetTime.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("PetTime.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PetTime.Models.Pet", b =>
                {
                    b.HasOne("PetTime.Models.CategoryModel", "Category")
                        .WithMany("Pets")
                        .HasForeignKey("CategoryModelName");
                });

            modelBuilder.Entity("PetTime.Models.PetCart", b =>
                {
                    b.HasOne("PetTime.Models.ApplicationUser", "ApplicationUser")
                        .WithOne("PetCart")
                        .HasForeignKey("PetTime.Models.PetCart", "ApplicationUserID");

                    b.HasOne("PetTime.Models.CorporateCart", "CorporateCart")
                        .WithMany()
                        .HasForeignKey("CorporateCartID");

                    b.HasOne("PetTime.Models.Pet", "Pet")
                        .WithMany()
                        .HasForeignKey("PetID");

                    b.HasOne("PetTime.Models.TherapyCart", "TherapyCart")
                        .WithMany()
                        .HasForeignKey("TherapyCartID");
                });

            modelBuilder.Entity("PetTime.Models.PetCartProduct", b =>
                {
                    b.HasOne("PetTime.Models.PetCart", "PetCart")
                        .WithMany("PetCartProducts")
                        .HasForeignKey("PetCartID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PetTime.Models.Pet", "Pet")
                        .WithMany("PetCartProducts")
                        .HasForeignKey("PetID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PetTime.Models.PetOrderProduct", b =>
                {
                    b.HasOne("PetTime.Models.PetOrder", "PetOrder")
                        .WithMany("PetOrderProducts")
                        .HasForeignKey("PetOrderID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
