using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ChatApp.Core.Entities.Main;
using ChatApp.Core.Helpers;

namespace ChatApp.Core.Interfaces.Main
{
    public interface IGenericRepository<T> where T : Base
    {
        void Add(T entity);
        Task AddAsync(T entity);
        void AddRange(IEnumerable<T> entities);
        Task AddRangeAsync(IEnumerable<T> entities);
        T Get(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<T> GetIncludingAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties);
        Task<int> CountAsync(Expression<Func<T, bool>> filter = null);
        IEnumerable<T> GetAll(PaginationFilter paginationFilter = null);
        IQueryable<T> GetAllQ(PaginationFilter paginationFilter = null);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> expression, PaginationFilter paginationFilter = null);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task<PagedList<T>> GetAllPaginatedAsync(PaginationFilter paginationFilter);
        Task<PagedList<T>> GetAllPaginatedAsync(Expression<Func<T, bool>> expression, PaginationFilter paginationFilter);
        Task<IEnumerable<T>> GetAllIncludingAsync(Expression<Func<T, bool>> expression = null, PaginationFilter? paginationFilter = null, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetAllSelect();
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
    }
}
