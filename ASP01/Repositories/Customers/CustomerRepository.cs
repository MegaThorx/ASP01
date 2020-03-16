using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ASP01.Models;
using ASP01.Models.ViewModels;

namespace ASP01.Repositories.Customers
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public new async Task<IList<CustomerNamesView>> GetAll()
        {
            return await (from c in _context.Customers
                orderby c.LName, c.FName
                select new CustomerNamesView
                {
                    CustomerId = c.CustomerId,
                    FullName = c.FName + " " + c.LName,
                }).ToListAsync();
        }

        /*
        public override async Task<PaginatedList<Customer>> GetAllPaginated(Expression<Func<Customer, int>> orderBy, int page = 1, int pageSize = 50)
        {
            return await PaginatedList<Customer>.CreateAsync(from c in _context.Customers orderby c.LName, c.FName select c, page, pageSize);
        }
        */


        public async Task<IList<CustomerNamesView>> GetAllView()
        {
            return await (from c in _context.Customers
                orderby c.LName, c.FName
                select new CustomerNamesView
                {
                    CustomerId = c.CustomerId,
                    FullName = c.FName + " " + c.LName
                }).ToListAsync();
        }

        public async Task<PaginatedList<CustomerNamesView>> GetAllViewPaginated(int page = 1, int pageSize = 50)
        {
            return await PaginatedList<CustomerNamesView>.CreateAsync(
                from c in _context.Customers
                orderby c.LName, c.FName
                select new CustomerNamesView
                {
                    CustomerId = c.CustomerId,
                    FullName = c.FName + " " + c.LName
                },
                page, pageSize);
        }

        public async Task<CustomerEditView> GetEditView(int id)
        {
            return await (from c in _context.Customers
                where c.CustomerId == id
                select new CustomerEditView
                {
                    CustomerId = c.CustomerId,
                    FName = c.FName,
                    LName = c.LName,
                }).FirstAsync();
        }
    }
}