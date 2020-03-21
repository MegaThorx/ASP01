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

namespace ASP01.Controllers
{
    public class OrderPositionsController : Controller
    {
        private readonly RepositoryManager _repository = new RepositoryManager();

        // GET: OrderPositions
        public async Task<ActionResult> Index()
        {
            return View(await _repository.OrderPosition.GetAllWithOrderAndProduct());
        }

        // GET: OrderPositions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var orderPosition = await _repository.OrderPosition.Find(id);

            if (orderPosition == null)
            {
                return HttpNotFound();
            }

            return View(orderPosition);
        }

        // GET: OrderPositions/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.OrderId = new SelectList(await _repository.Order.GetAll(), "OrderId", "OrderId");
            ViewBag.ProductId = new SelectList(await _repository.Product.GetAll(), "ProductId", "Name");
            return View();
        }

        // POST: OrderPositions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrderId,Position,ProductId,Amount,Price,Discount")] OrderPosition orderPosition)
        {
            if (ModelState.IsValid)
            {
                _repository.OrderPosition.Add(orderPosition);
                await _repository.Commit();

                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(await _repository.Order.GetAll(), "OrderId", "OrderId");
            ViewBag.ProductId = new SelectList(await _repository.Product.GetAll(), "ProductId", "Name");
            return View(orderPosition);
        }

        // GET: OrderPositions/CreateModal
        public async Task<ActionResult> CreateModal(int? orderId)
        {
            var order = await _repository.Order.Find(orderId);

            if (order == null)
            {
                return HttpNotFound();
            }

            var position = new OrderPosition();
            position.OrderId = order.OrderId;

            if (order.Positions.Count == 0)
            {
                position.Position = 1;
            }
            else
            {
                position.Position = order.Positions.Last().Position + 1;
            }

            ViewBag.ProductId = new SelectList(await _repository.Product.GetAll(), "ProductId", "Name");

            return PartialView("Partials/AddModal", position);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateModal([Bind(Include = "OrderId,Position,ProductId,Amount,Price,Discount")] OrderPosition orderPosition)
        {
            if (ModelState.IsValid)
            {
                _repository.OrderPosition.Add(orderPosition);
                await _repository.Commit();
                return RedirectToAction("EditModal", "Orders", new { id = orderPosition.OrderId });
            }
            // { id = orderPosition.OrderId }
            return RedirectToAction("EditModal", "Orders", new { id = orderPosition.OrderId });
        }


        // GET: OrderPositions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            var orderPosition = await _repository.OrderPosition.Find(id);

            if (orderPosition == null)
            {
                return HttpNotFound();
            }

            ViewBag.OrderId = new SelectList(await _repository.Order.GetAll(), "OrderId", "OrderId", orderPosition.OrderId);
            ViewBag.ProductId = new SelectList(await _repository.Product.GetAll(), "ProductId", "Name", orderPosition.ProductId);
            return View(orderPosition);
        }

        // POST: OrderPositions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrderId,Position,ProductId,Amount,Price,Discount")] OrderPosition orderPosition)
        {
            if (ModelState.IsValid)
            {
                var dbOrderPosition = await _repository.OrderPosition.Find(orderPosition.Position);

                if (dbOrderPosition == null)
                {
                    return HttpNotFound();
                }
                
                dbOrderPosition.ProductId = orderPosition.ProductId;
                dbOrderPosition.Amount = orderPosition.Amount;
                dbOrderPosition.Price = orderPosition.Price;
                dbOrderPosition.Discount = orderPosition.Discount;

                await _repository.Commit();

                return RedirectToAction("Index");
            }
            ViewBag.OrderId = new SelectList(await _repository.Order.GetAll(), "OrderId", "OrderId", orderPosition.OrderId);
            ViewBag.ProductId = new SelectList(await _repository.Product.GetAll(), "ProductId", "Name", orderPosition.ProductId);
            return View(orderPosition);
        }

        // GET: OrderPositions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            var orderPosition = await _repository.OrderPosition.Find(id);

            if (orderPosition == null)
            {
                return HttpNotFound();
            }

            return View(orderPosition);
        }

        // POST: OrderPositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var orderPosition = await _repository.OrderPosition.Find(id);
            _repository.OrderPosition.Remove(orderPosition);
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
