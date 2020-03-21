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
    public class BasketItemsController : Controller
    {
        private readonly RepositoryManager _repository = new RepositoryManager();

        // GET: BasketItems
        public async Task<ActionResult> Index()
        {
            var id = User.Identity.GetUserId();
            var user = await _repository.User.Find(id);
            ViewData["Total"] = user.BasketItems.Sum(p => p.Sum);
            return View(user.BasketItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddProduct(int id)
        {
            var userId = User.Identity.GetUserId();
            var user = await _repository.User.Find(userId);
            var product = await _repository.Product.Find(id);

            BasketItem basketItem = await _repository.BasketItem.Find(b => b.ProductId == product.ProductId);

            if (basketItem != null)
            {
                basketItem.Amount++;
            }
            else
            {
                basketItem = new BasketItem
                {
                    User = user,
                    Product = product,
                    Amount = 1
                };

                _repository.BasketItem.Add(basketItem);
            }


            await _repository.Commit();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Order()
        {
            var userId = User.Identity.GetUserId();
            var user = await _repository.User.Find(userId);


            if (user.BasketItems.Count == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = new Order
            {
                // Customer = user.Customer,
                CustomerId = 1,
                OrderDate = DateTime.Now,
                Discount = 0
            };

            _repository.Order.Add(order);

            await _repository.Commit();


            var position = 1;

            foreach (var basketItem in user.BasketItems)
            {
                var orderPosition = new OrderPosition
                {
                    Position = position,
                    Order = order,
                    Amount = basketItem.Amount,
                    Product = basketItem.Product,
                    Price = basketItem.Product.Price,
                    Discount = 0
                };

                _repository.OrderPosition.Add(orderPosition);

                position++;
            }

            var basketItems = user.BasketItems.ToList();
            for (int i = basketItems.Count - 1; i >= 0; i--)
            {
                _repository.BasketItem.Remove(basketItems[i]);
            }

            await _repository.Commit();

            return RedirectToAction("Details", "Orders", new { id = order.OrderId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(List<BasketItem> items)
        {
            var userId = User.Identity.GetUserId();
            var user = await _repository.User.Find(userId);
            
            var basketItems = user.BasketItems.ToList();
            for (int i = basketItems.Count - 1; i >= 0; i--)
            {
                _repository.BasketItem.Remove(basketItems[i]);
            }

            await _repository.Commit();
            
            foreach (var item in items)
            {
                if (item.Amount > 0)
                {
                    var basketItem = new BasketItem
                    {
                        User = user,
                        ProductId = item.ProductId,
                        Amount = item.Amount
                    };

                    _repository.BasketItem.Add(basketItem);
                }
            }
            
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
