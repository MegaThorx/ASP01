using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ASP01.Models
{
    public class ApplicationRole : IdentityRole
    {
        [StringLength(200)]
        public string Notes { get; set; }
    }


    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Display(Name = "Vorname")]
        [StringLength(100, MinimumLength = 1)]
        public string FName { get; set; }

        [Display(Name = "Nachname")]
        [StringLength(100, MinimumLength = 1)]
        public string LName { get; set; }

        [Display(Name = "Kunde")]
        public int? CustomerId { get; set; }

        public Customer Customer { get; set; }

        public virtual ICollection<BasketItem> BasketItems { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        // TODO: DbSets here

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderPosition> OrderPositions { get; set; }

        public DbSet<BasketItem> BasketItems { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<InvoicePosition> InvoicePositions { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderPosition>() // Fluent API
                .HasKey(k => new {k.OrderId, k.Position});

            /*
            modelBuilder.Entity<Invoice>().HasRequired(i => i.Order)
                .WithRequiredDependent().WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>().HasRequired(o => o.Invoice)
                .WithRequiredPrincipal();
                */


            modelBuilder.Entity<Invoice>()
                .HasKey(t => t.InvoiceId);

            modelBuilder.Entity<Order>()
                .HasRequired(t => t.Invoice)
                .WithRequiredPrincipal(t => t.Order);
        }
    }
}