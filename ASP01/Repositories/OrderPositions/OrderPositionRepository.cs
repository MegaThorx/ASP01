using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ASP01.Models;

namespace ASP01.Repositories.OrderPositions
{
    public class OrderPositionRepository : BaseRepository<OrderPosition>, IOrderPositionRepository
    {
        public OrderPositionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IList<OrderPosition>> GetAllWithOrderAndProduct()
        {
            return await _context.OrderPositions.Include(o => o.Order).Include(o => o.Product).ToListAsync();
        }
    }
}