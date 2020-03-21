using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ASP01.Models;
using ASP01.Models.ViewModels;
using ASP01.Repositories;

namespace ASP01.Controllers
{
    [Authorize(Roles = "Office,Admin")]
    public class CustomersController : Controller
    {
        private readonly RepositoryManager _repository = new RepositoryManager();

        // GET: Customers
        public async Task<ActionResult> Index(int page = 1, int pageSize = 25, string orderBy = "", string direction = "asc", string lname = "")
        {
            var ascending = direction != "desc";

            ViewData["fname_dir"] = "asc";
            ViewData["lname_dir"] = "asc";
            ViewData["birthday_dir"] = "asc";

            var customers = from c in _repository.Customer.GetAllQuery()
                select c;

            if (!string.IsNullOrEmpty(lname))
            {
                customers = customers.Where(c => c.LName.Contains(lname));
            }

            switch (orderBy)
            {
                case "fname":
                    customers = @ascending ? customers.OrderBy(c => c.FName) : customers.OrderByDescending(c => c.FName);
                    ViewData["fname_dir"] = ascending ? "desc" : "asc";
                    break;

                case "lname":
                    customers = @ascending ? customers.OrderBy(c => c.LName) : customers.OrderByDescending(c => c.LName);
                    ViewData["lname_dir"] = ascending ? "desc" : "asc";
                    break;

                case "birthday":
                    customers = @ascending ? customers.OrderBy(c => c.Birthday) : customers.OrderByDescending(c => c.Birthday);
                    ViewData["birthday_dir"] = ascending ? "desc" : "asc";
                    break;
                default:
                    customers = customers.OrderBy(c => c.CustomerId);
                    break;
            }

            return View(await _repository.Customer.GetAllPaginated(customers, page, pageSize)); 
        }

        // GET: Customers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var customer = await _repository.Customer.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CustomerId,FName,LName,Birthday,Discount")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _repository.Customer.Add(customer);
                await _repository.Commit();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            var customer = await _repository.Customer.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "CustomerId,FName,LName,Birthday,Discount")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var dbCustomer = await _repository.Customer.Find(customer.CustomerId);

                dbCustomer.FName = customer.FName;
                dbCustomer.LName = customer.LName;
                dbCustomer.Birthday = customer.Birthday;
                dbCustomer.Discount = customer.Discount;
                await _repository.Commit();

                return RedirectToAction("Index");
            }
            return View(customer);
        }



        // GET: Customers/EditName/5
        [Authorize(Roles = "Office")]
        public async Task<ActionResult> EditName(int id, string returnUrl)
        {
            var customer = await _repository.Customer.GetEditView(id);
            
            if (customer == null)
            {
                return HttpNotFound();
            }

            // Request.UrlReferrer.LocalPath

            Session["ReturnUrl"] = returnUrl;

            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Office")]
        public async Task<ActionResult> EditName([Bind(Include = "CustomerId,FName,LName")] CustomerEditView customer)
        {
            if (ModelState.IsValid)
            {
                var dbCustomer = await _repository.Customer.GetEditView(customer.CustomerId);

                if (dbCustomer == null)
                {
                    return HttpNotFound();
                }

                dbCustomer.FName = customer.FName;
                dbCustomer.LName = customer.LName;
                await _repository.Commit();

                var returnUrl = (string)Session["ReturnUrl"];
                if (returnUrl != "")
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            var customer = await _repository.Customer.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var customer = await _repository.Customer.Find(id);
            _repository.Customer.Remove(customer);
            await _repository.Commit();

            return RedirectToAction("Index");
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
