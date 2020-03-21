using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using ASP01.Models;

namespace ASP01.Repositories.BasketItems
{
    public class BasketItemRepository : BaseRepository<BasketItem>, IBasketItemRepository
    {
        public BasketItemRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}