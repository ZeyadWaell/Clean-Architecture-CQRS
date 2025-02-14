using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ChatApp.Core.Entities.Main;
using ChatApp.Core.Interfaces.Main;
using ChatApp.Infrastructure.Data;
using ChatApp.Core.Helpers;

namespace ChatApp.Infrastructure.Repositories.Main
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : Base
    {
        protected readonly ChatDbContext _dbContext;
        public DbSet<T> EntitySet { get; }

        public GenericRepository(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
            EntitySet = _dbContext.Set<T>();
        }

        public void Add(T entity) => _dbContext.Add(entity);

        public async Task AddAsync(T entity) => await _dbContext.AddAsync(entity);

        public void AddRange(IEnumerable<T> entities) => _dbContext.AddRange(entities);

        public async Task AddRangeAsync(IEnumerable<T> entities) => await _dbContext.AddRangeAsync(entities);

        public T Get(Expression<Func<T, bool>> expression) => EntitySet.FirstOrDefault(expression);

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression) => await EntitySet.FirstOrDefaultAsync(expression);

        public Task<T> GetIncludingAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties)
        {
            var entities = IncludeProperties(includeProperties);
            return entities.FirstOrDefaultAsync(expression);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter == null)
                return await EntitySet.CountAsync();
            return await EntitySet.CountAsync(filter);
        }

        public IEnumerable<T> GetAll(PaginationFilter paginationFilter = null)
        {
            if (paginationFilter == null)
                return EntitySet.OrderByDescending(x => x.CreatedOn).ToList();
            int skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            return EntitySet.OrderByDescending(x => x.CreatedOn).Skip(skip).Take(paginationFilter.PageSize).ToList();
        }

        public IQueryable<T> GetAllQ(PaginationFilter paginationFilter = null)
        {
            IOrderedQueryable<T> query = EntitySet.OrderByDescending(x => x.CreatedOn);
            if (paginationFilter != null)
            {
                int skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
                query = (IOrderedQueryable<T>)query.Skip(skip).Take(paginationFilter.PageSize);
            }
            return query;
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression, PaginationFilter paginationFilter = null)
        {
            if (paginationFilter == null)
                return EntitySet.Where(expression).OrderByDescending(x => x.CreatedOn).ToList();
            int skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            return EntitySet.Where(expression).OrderByDescending(x => x.CreatedOn).Skip(skip).Take(paginationFilter.PageSize).ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await EntitySet.OrderByDescending(x => x.CreatedOn).ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression) => await EntitySet.Where(expression).OrderByDescending(x => x.CreatedOn).ToListAsync();

        public async Task<PagedList<T>> GetAllPaginatedAsync(PaginationFilter paginationFilter)
        {
            var queryable = EntitySet.OrderByDescending(x => x.CreatedOn);
            return await PagedList<T>.CreateAsync(queryable, paginationFilter.PageNumber, paginationFilter.PageSize);
        }

        public async Task<PagedList<T>> GetAllPaginatedAsync(Expression<Func<T, bool>> expression, PaginationFilter paginationFilter)
        {
            var queryable = EntitySet.Where(expression).OrderByDescending(x => x.CreatedOn);
            return await PagedList<T>.CreateAsync(queryable, paginationFilter.PageNumber, paginationFilter.PageSize);
        }

        public async Task<IEnumerable<T>> GetAllIncludingAsync(Expression<Func<T, bool>> expression = null, PaginationFilter? paginationFilter = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> entities = EntitySet;
            entities = IncludeProperties(includeProperties);
            if (expression != null)
                entities = entities.Where(expression);
            entities = entities.OrderByDescending(x => x.CreatedOn);
            if (paginationFilter == null)
                return await entities.ToListAsync();
            return await PagedList<T>.CreateAsync(entities, paginationFilter.PageNumber, paginationFilter.PageSize);
        }

        public async Task<IEnumerable<T>> GetAllSelect() => await _dbContext.Set<T>().ToListAsync();

        public void Remove(T entity) => _dbContext.Remove(entity);

        public void RemoveRange(IEnumerable<T> entities) => _dbContext.RemoveRange(entities);

        public void Update(T entity) => _dbContext.Update(entity);

        public void UpdateRange(IEnumerable<T> entities) => _dbContext.UpdateRange(entities);

        private IQueryable<T> IncludeProperties(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = EntitySet;
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return query;
        }
    }
}
