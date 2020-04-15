using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ASP01.Models;
using ASP01.Repositories.Address;
using ASP01.Repositories.BasketItems;
using ASP01.Repositories.Comments;
using ASP01.Repositories.Customers;
using ASP01.Repositories.InvoicePositions;
using ASP01.Repositories.Invoices;
using ASP01.Repositories.OrderPositions;
using ASP01.Repositories.Orders;
using ASP01.Repositories.Products;
using ASP01.Repositories.Users;

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

        private IUserRepository _user { get; set; }
        public IUserRepository User => _user ?? (_user = new UserRepository(_context));

        private IBasketItemRepository _basketItem { get; set; }
        public IBasketItemRepository BasketItem => _basketItem ?? (_basketItem = new BasketItemRepository(_context));

        private IInvoiceRepository _invoice { get; set; }
        public IInvoiceRepository Invoice => _invoice ?? (_invoice = new InvoiceRepository(_context));

        private IInvoicePositionRepository _invoicePosition { get; set; }
        public IInvoicePositionRepository InvoicePosition => _invoicePosition ?? (_invoicePosition = new InvoicePositionRepository(_context));

        private IAddressRepository _address { get; set; }
        public IAddressRepository Address => _address ?? (_address = new AddressRepository(_context));

        private ICommentRepository _comment { get; set; }
        public ICommentRepository Comment => _comment ?? (_comment = new CommentRepository(_context));

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