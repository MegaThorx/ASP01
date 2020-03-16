using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ASP01.Models;

namespace ASP01.Repositories.Products
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<PaginatedList<Product>> GetAllPaginated(int page = 1, int pageSize = 50)
        {
            return await PaginatedList<Product>.CreateAsync(from c in _context.Products orderby c.ProductId select c, page, pageSize);
        }
    }
}