using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP01.Models;

namespace ASP01.Repositories.OrderPositions
{
    public interface IOrderPositionRepository : IBaseRepository<OrderPosition>
    {
        Task<IList<OrderPosition>> GetAllWithOrderAndProduct();
    }
}
