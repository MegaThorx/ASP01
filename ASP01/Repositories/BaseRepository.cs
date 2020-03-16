using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using ASP01.Models;

namespace ASP01.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        
        public virtual IQueryable<T> GetAllQuery()
        {
            return from c in _context.Set<T>() select c;
        }

        public virtual async Task<PaginatedList<T>> GetAllPaginated(IQueryable<T> query, int page = 1, int pageSize = 50)
        {
            return await PaginatedList<T>.CreateAsync(query, page, pageSize);
        }

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<IList<T>> GetAll(Expression<Func<T, int>> orderBy = null)
        {
            var dbSet = _context.Set<T>();

            if (orderBy != null)
            {
                return await dbSet.OrderBy(orderBy).ToListAsync();
            }

            return await dbSet.ToListAsync();
        }

        public virtual async Task<T> Find(params object[] keyValues)
        {
            return await _context.Set<T>().FindAsync(keyValues);
        }

        public async Task<T> Find(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).FirstAsync();
        }

        public async Task<bool> Exists(params object[] keyValues)
        {
            return await Find(keyValues) != null;
        }

        public async Task<bool> Exists(T entity)
        {
            return await _context.Set<T>().AnyAsync(e => e == entity);
        }

        public virtual T Add(T entity)
        {
            return _context.Set<T>().Add(entity);
        }

        public T Remove(T entity)
        {
            return _context.Set<T>().Remove(entity);
        }
    }
}