using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP01.Models;
using ASP01.Models.ViewModels;

namespace ASP01.Repositories.Orders
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<OrderEditView> FindEditView(int? id);
    }
}
