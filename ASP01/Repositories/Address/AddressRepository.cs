using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using ASP01.Models;
using ASP01.Models.ViewModels;

namespace ASP01.Repositories.Address
{
    public class AddressRepository : BaseRepository<Models.Address>, IAddressRepository
    {
        public AddressRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}