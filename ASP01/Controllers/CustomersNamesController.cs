using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ASP01.Models;
using ASP01.Models.ViewModels;
using ASP01.Repositories;

namespace ASP01.Controllers
{
    [Authorize(Roles = "Office,Admin")]
    public class CustomersNamesController : Controller
    {
        private readonly RepositoryManager _repository = new RepositoryManager();

        public async Task<ActionResult> Index()
        {
            return View(await _repository.Customer.GetAllView());
        }

        [Route("🤖🤖🤖🤖🤖🤖🤖🤖🤖🤖🤖🤖🤖🤖🤖🤖🤖🤖🤖")]
        public async Task<ActionResult> Test()
        {
            return View("../Customers/CustomersNames", await _repository.Customer.GetAllView());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}