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

            context.Customers.AddOrUpdate( new [] {

                    new Customer
                    {
                        CustomerId = 1,
                        LName = "Lastname 1",
                        FName = "Firstname 2",
                        Birthday = new DateTime(2000, 12, 31)
                    },

                    new Customer
                    {
                        CustomerId = 2,
                        LName = "Lastname 2",
                        FName = "Firstname 2",
                        Birthday = new DateTime(2000, 12, 31)
                    },

                    new Customer
                    {
                        CustomerId = 3,
                        LName = "Lastname 3",
                        FName = "Firstname 3",
                        Birthday = new DateTime(2000, 12, 31)
                    }
                }
                );

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
        }
    }
}
