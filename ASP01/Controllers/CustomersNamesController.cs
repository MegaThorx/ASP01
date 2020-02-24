using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ASP01.Models;
using ASP01.Models.ViewModels;

namespace ASP01.Controllers
{
    public class CustomersNamesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var customers = from c in db.Customers
                orderby c.LName, c.FName
                select new CustomerNamesView
                {
                    CustomerId = c.CustomerId,
                    FullName = c.FName + " " + c.LName,
                };


            return View(customers.ToList());
        }

        [Route("🤖🤖🤖🤖🤖🤖🤖🤖🤖🤖🤖🤖🤖🤖🤖🤖🤖🤖🤖")]
        public async Task<ActionResult> Test()
        {
            var customers = from c in db.Customers
                orderby c.LName, c.FName
                select new CustomerNamesView
                {
                    CustomerId = c.CustomerId,
                    FullName = c.FName + " " + c.LName,
                };


            return View("../Customers/CustomersNames", await customers.ToListAsync());
        }
    }
}