using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ASP01.Models;
using ASP01.Repositories.Customers;
using ASP01.Repositories.OrderPositions;
using ASP01.Repositories.Orders;
using ASP01.Repositories.Products;

namespace ASP01.Repositories
{
    public class RepositoryManager : IDisposable
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        private ICustomerRepository _customer { get; set; }
        public ICustomerRepository Customer => _customer ?? (_customer = new CustomerRepository(_context));

        private IProductRepository _product { get; set; }
        public IProductRepository Product => _product ?? (_product = new ProductRepository(_context));

        private IOrderRepository _order { get; set; }
        public IOrderRepository Order => _order ?? (_order = new OrderRepository(_context));

        private IOrderPositionRepository _orderPosition { get; set; }
        public IOrderPositionRepository OrderPosition => _orderPosition ?? (_orderPosition = new OrderPositionRepository(_context));
        
        public async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}