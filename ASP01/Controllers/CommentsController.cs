using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ASP01.Models;
using ASP01.Models.ViewModels;
using ASP01.Repositories;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace ASP01.Controllers
{
    public class CommentsController : BaseController
    {
        private readonly RepositoryManager _repository = new RepositoryManager();

        // GET: Comments
        public PartialViewResult Index(int? id) // PartialViewResult
        {
            if (!id.HasValue)
            {
                return null;
            }

            var products = _repository.Comment.GetAllSync(c => c.ProductId == id);

            return PartialView(products);
        }

        // GET: Comments/Create
        public ActionResult Create(int id, int? parentId)
        {
            ViewData["ProductId"] = id;
            ViewData["ParentId"] = parentId;
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductId,ParentId,Content")] CommentView commentView)
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment();
                comment.ProductId = commentView.ProductId;
                comment.ParentId = commentView.ParentId;
                comment.Content = commentView.Content;
                comment.CreatedAt = DateTime.Now;
                comment.UserId = User.Identity.GetUserId();

                _repository.Comment.Add(comment);
                await _repository.Commit();
                return RedirectToAction("Details", "Products", new { id = comment.ProductId });
            }

            ViewData["ProductId"] = commentView.ProductId;
            ViewData["ParentId"] = commentView.ParentId;

            return View(commentView);
        }


        /*
        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var product =  await _repository.Product.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            var product = await _repository.Product.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductId,Name,Description,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                var dbProduct = await _repository.Product.Find(product.ProductId);

                if (dbProduct == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                dbProduct.Name = product.Name;
                dbProduct.Description = product.Description;
                dbProduct.Price = product.Price;

                await _repository.Commit();

                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            var product = await _repository.Product.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var product = await _repository.Product.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            _repository.Product.Remove(product);
            await _repository.Commit();

            return RedirectToAction("Index");
        }
        */

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
