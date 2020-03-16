using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ASP01.Models;
using ASP01.Models.ViewModels;

namespace ASP01.Repositories.Customers
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<IList<CustomerNamesView>> GetAllView();
        Task<PaginatedList<CustomerNamesView>> GetAllViewPaginated(int page = 1, int pageSize = 50);

        Task<CustomerEditView> GetEditView(int id);
    }
}