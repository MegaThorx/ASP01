using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace ASP01.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IList<T>> GetAll(Expression<Func<T, bool>> where = null);
        IList<T> GetAllSync(Expression<Func<T, bool>> where = null);
        IQueryable<T> GetAllQuery();
        Task<PaginatedList<T>> GetAllPaginated(IQueryable<T> query, int page = 1, int pageSize = 50);
        Task<PaginatedList<T>> GetAllPaginated(IOrderedQueryable<T> query, int page = 1, int pageSize = 50);
        Task<T> Find(params object[] keyValues);
        Task<T> Find(Expression<Func<T, bool>> predicate);
        Task<bool> Exists(params object[] keyValues);
        Task<bool> Exists(Expression<Func<T, bool>> predicate);
        Task<bool> Exists(T entity);
        Task<bool> Any();
        Task<int> Max(Expression<Func<T, int>> predicate);
        T Add(T entity);
        T Remove(T entity);
    }
}