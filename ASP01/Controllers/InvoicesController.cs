using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP01.Models;
using ASP01.Repositories;
using Microsoft.AspNet.Identity;

namespace ASP01.Controllers
{
    [Authorize]
    public class InvoicesController : Controller
    {
        private readonly RepositoryManager _repository = new RepositoryManager();

        // GET: Invoices
        public async Task<ActionResult> Index(int page = 1, int pageSize = 50)
        {
            var id = User.Identity.GetUserId();
            var user = await _repository.User.Find(id);

            if (User.IsInRole("Admin") || User.IsInRole("Office"))
            {
                return View(await _repository.Invoice.GetAllPaginated(from o in _repository.Invoice.GetAllQuery() orderby o.InvoiceId descending select o, page, pageSize));
            }

            return View(await _repository.Invoice.GetAllPaginated(from o in _repository.Invoice.GetAllQuery() orderby o.InvoiceId descending where o.CustomerId == user.CustomerId select o, page, pageSize));
        }

        // GET: Invoices/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var invoice = await _repository.Invoice.Find(id);
            
            if (invoice == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userId = User.Identity.GetUserId();
            var user = await _repository.User.Find(userId);

            if (!User.IsInRole("Admin") && !User.IsInRole("Office"))
            {
                if (invoice.CustomerId == null || invoice.CustomerId != user.CustomerId)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            return View(invoice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Office")]
        public async Task<ActionResult> CreateInvoice(int id)
        {
            var order = await _repository.Order.Find(id);

            // Check if there is already a invoice
            if (order.Invoice != null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var invoice = new Invoice
            {
                InvoiceId = await _repository.Invoice.Any() ? await _repository.Invoice.Max(i => i.InvoiceId) + 1 : 1,

                OrderId = order.OrderId,
                Discount = 0,

                InvoiceDate = DateTime.Now,
                Copy = false,
                Canceled = false,

                CustomerId = order.Customer.CustomerId,
                Street = order.Customer.Address.Street,
                PostalCode = order.Customer.Address.PostalCode,
                City = order.Customer.Address.City,
                Country = order.Customer.Address.Country,
            };

            _repository.Invoice.Add(invoice);
            await _repository.Commit();

            order.InvoiceId = invoice.InvoiceId;
            // var db = new ApplicationDbContext();

            foreach (var position in order.Positions)
            {
                var invoicePosition = new InvoicePosition
                {
                    InvoiceId = invoice.InvoiceId,
                    Position = position.Position,

                    ProductId = position.Product.ProductId,
                    Amount = position.Amount,
                    Discount = position.Discount,
                    Price = position.Price,
                    ProductName = position.Product.Name,
                    ProductDescription = position.Product.Description,
                };


                _repository.InvoicePosition.Add(invoicePosition);
                // db.InvoicePositions.Add(invoicePosition);
            }

            await _repository.Commit();
            // await db.SaveChangesAsync();

            return RedirectToAction("Details", new { id = invoice.InvoiceId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Office")]
        public async Task<ActionResult> Print(int id)
        {
            var invoice = await _repository.Invoice.Find(id);

            if (invoice == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (invoice.Copy == false)
            {
                invoice.Copy = true;
                await _repository.Commit();
            }

            // TODO: Do some awesome printing logic;

            return RedirectToAction("Details", new { id = invoice.InvoiceId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Office")]
        public async Task<ActionResult> Cancel(int id)
        {
            var invoice = await _repository.Invoice.Find(id);

            if (invoice == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (invoice.Canceled == false)
            {
                invoice.Canceled = true;
                await _repository.Commit();
            }

            return RedirectToAction("Details", new { id = invoice.InvoiceId });
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
