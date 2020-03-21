using System.Collections.Generic;
using ASP01.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ASP01.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ASP01.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ASP01.Models.ApplicationDbContext";
        }

        protected override void Seed(ASP01.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var random = new Random();


            var addresses = new List<Address>();

            for (int i = 1; i < 1000; i++)
            {
                addresses.Add(
                    new Address
                    {
                        AddressId = i,
                        Street = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10).Select(s => s[random.Next(s.Length)]).ToArray()),
                        PostalCode = new string(Enumerable.Repeat("123456789", 4).Select(s => s[random.Next(s.Length)]).ToArray()),
                        City = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10).Select(s => s[random.Next(s.Length)]).ToArray()),
                        Country = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10).Select(s => s[random.Next(s.Length)]).ToArray()),
                    }
                );
            }
            context.Addresses.AddOrUpdate(addresses.ToArray());

            var entities = new List<Customer>();

            for (int i = 1; i < 1000; i++)
            {
                entities.Add(
                    new Customer
                    {
                        CustomerId = i,
                        LName = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10) .Select(s => s[random.Next(s.Length)]).ToArray()),
                        FName = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10) .Select(s => s[random.Next(s.Length)]).ToArray()),
                        Birthday = new DateTime(2000, 12, 31),
                        Notes = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 50).Select(s => s[random.Next(s.Length)]).ToArray()),
                        AddressId = random.Next(1, 1000),
                    }
                );
            }

            context.Customers.AddOrUpdate(entities.ToArray());

            var entities2 = new List<Order>();

            for (int i = 1; i < 10; i++)
            {
                entities2.Add(
                    new Order
                    {
                        OrderId = i,
                        CustomerId = random.Next(1, 50),
                        Discount = 0.2f,
                        OrderDate = new DateTime(2000, 12, 31)
                    }
                );
            }

            context.Orders.AddOrUpdate(entities2.ToArray());

            var entities3 = new List<Product>();

            for (int i = 1; i < 1000; i++)
            {
                entities3.Add(
                    new Product
                    {
                        ProductId = i,
                        Name = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10).Select(s => s[random.Next(s.Length)]).ToArray()),
                        Description = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10).Select(s => s[random.Next(s.Length)]).ToArray()),
                        Price = random.Next(1, 10000) / 100
                    }
                );
            }

            context.Products.AddOrUpdate(entities3.ToArray());

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));


            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }


            if (!roleManager.RoleExists("Office"))
            {
                var role = new IdentityRole();
                role.Name = "Office";
                roleManager.Create(role);
            }


            if (!roleManager.RoleExists("Guest"))
            {
                var role = new IdentityRole();
                role.Name = "Guest";
                roleManager.Create(role);
            }
        }
    }
}
