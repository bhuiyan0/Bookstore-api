using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using OBSMVCApi.Controllers;
using OBSMVCApi.DTO;

namespace OBSMVCApi.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
        // Property add to AspNetUser Table
        #region Property
        [StringLength(20)]
        public string FirstName { get; set; }

        [StringLength(20)]
        public string LastName { get; set; }

        public DateTime DoB { get; set; }

        [StringLength(20)]
        public string Gender { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        public string ImageUrl { get; set; }

        public string UserType { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
        public ICollection<BookReview> BookReviews { get; set; }
        public ICollection<Cart> Carts { get; set; }

        #endregion

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.Entity<ApplicationUser>().Property(a => a.DoB).IsOptional();
            modelBuilder.Entity<Author>().Property(a => a.DoB).IsOptional();

            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
        }

        #region DbSets
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseLine> PurchaseLines { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<BookReview> BookReviews { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<Cart> Carts { get; set; }

        #endregion

    }
}