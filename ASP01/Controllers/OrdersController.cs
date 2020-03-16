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
using ASP01.Models.ViewModels;
using ASP01.Repositories;

namespace ASP01.Controllers
{
    public class OrdersController : Controller
    {
        private readonly RepositoryManager _repository = new RepositoryManager();

        // GET: Orders
        public async Task<ActionResult> Index(int page = 1, int pageSize = 25)
        {
            return View(await _repository.Order.GetAllPaginated(from o in _repository.Order.GetAllQuery() orderby o.CustomerId select o, page, pageSize));
        }

        // GET: Orders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var order = await _repository.Order.Find(id);

            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.CustomerId = new SelectList(await _repository.Customer.GetAll(), "CustomerId", "FName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrderId,OrderDate,CustomerId,Discount")] Order order)
        {
            if (ModelState.IsValid)
            {
                _repository.Order.Add(order);
                await _repository.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(await _repository.Customer.GetAll(), "CustomerId", "FName", order.CustomerId);
            return View(order);
        }

        // GET: Orders/EditModal/5
        public async Task<ActionResult> EditModal(int? id)
        {
            var order = await _repository.Order.FindEditView(id);


            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(await _repository.Customer.GetAll(), "CustomerId", "FName", order.CustomerId);

            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            var order = await _repository.Order.FindEditView(id);

            if (order == null)
            {
                return HttpNotFound();
            }

            ViewBag.CustomerId = new SelectList(await _repository.Customer.GetAll(), "CustomerId", "FName", order.CustomerId);
            ViewBag.Products = await _repository.Product.GetAll();
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(OrderEditView order, List<OrderPositionEditView> positions)
        {
            order.Positions = positions;
            if (ModelState.IsValid)
            {
                var dbOrder = await _repository.Order.Find(order.OrderId);
                dbOrder.CustomerId = order.CustomerId;
                dbOrder.OrderDate = order.OrderDate;
                dbOrder.Discount = order.Discount;

                foreach (var position in positions)
                {
                    var found = false;

                    foreach (var dbPosition in dbOrder.Positions)
                    {
                        if (position.Position == dbPosition.Position)
                        {
                            dbPosition.ProductId = position.ProductId;
                            dbPosition.Amount = position.Amount;
                            dbPosition.Price = position.Price;
                            dbPosition.Discount = position.Discount;
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        dbOrder.Positions.Add(new OrderPosition
                        {
                            Position = position.Position,
                            ProductId = position.ProductId,
                            Amount = position.Amount,
                            Price = position.Price,
                            Discount = position.Discount,
                        });
                    }
                }


                await _repository.Commit();
                return RedirectToAction("Details", new { id = order.OrderId });
            }
            ViewBag.CustomerId = new SelectList(await _repository.Customer.GetAll(), "CustomerId", "FName", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            var order = await _repository.Order.Find(id);

            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var order = await _repository.Order.Find(id);
            _repository.Order.Remove(order);
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
