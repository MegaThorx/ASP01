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

        public virtual async Task<PaginatedList<T>> GetAllPaginated(IOrderedQueryable<T> query, int page = 1, int pageSize = 50)
        {
            return await PaginatedList<T>.CreateAsync(query, page, pageSize);
        }

        public virtual async Task<PaginatedList<T>> GetAllPaginated(IQueryable<T> query, int page = 1, int pageSize = 50)
        {
            return await PaginatedList<T>.CreateAsync(query, page, pageSize);
        }

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<IList<T>> GetAll(Expression<Func<T, bool>> where = null)
        {
            var dbSet = _context.Set<T>();

            if (where != null)
            {
                return await dbSet.Where(where).ToListAsync();
            }

            return await dbSet.ToListAsync();
        }

        public virtual IList<T> GetAllSync(Expression<Func<T, bool>> where = null)
        {
            var dbSet = _context.Set<T>();

            if (where != null)
            {
                return dbSet.Where(where).ToList();
            }

            return dbSet.ToList();
        }

        public virtual async Task<T> Find(params object[] keyValues)
        {
            return await _context.Set<T>().FindAsync(keyValues);
        }

        public async Task<T> Find(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<bool> Exists(params object[] keyValues)
        {
            return await Find(keyValues) != null;
        }

        public async Task<bool> Exists(T entity)
        {
            return await _context.Set<T>().AnyAsync(e => e == entity);
        }

        public async Task<bool> Exists(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }

        public async Task<bool> Any()
        {
            return await _context.Set<T>().AnyAsync();
        }

        public async Task<int> Max(Expression<Func<T, int>> predicate)
        {
            return await _context.Set<T>().MaxAsync(predicate);
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