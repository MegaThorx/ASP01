using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using ASP01.Models;
using ASP01.Models.ViewModels;

namespace ASP01.Repositories.Orders
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<OrderEditView> FindEditView(int? id)
        {
            if (id == null)
            {
                return null;
            }


            return await(from o in _context.Orders.Include(p => p.Positions)
                select new OrderEditView
                {
                    OrderId = o.OrderId,
                    CustomerId = o.CustomerId,
                    Discount = o.Discount,
                    OrderDate = o.OrderDate,
                    Positions = o.Positions.Select(p => new OrderPositionEditView
                    {
                        ProductId = p.ProductId,
                        Amount = p.Amount,
                        Discount = p.Discount,
                        Position = p.Position,
                        Price = p.Price
                    }).ToList()
                }).FirstAsync();
        }
    }
}