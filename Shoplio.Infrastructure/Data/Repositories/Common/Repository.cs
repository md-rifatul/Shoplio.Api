using Microsoft.EntityFrameworkCore;
using Shoplio.Application.Interfaces.IRepository;
using System.Linq;
using System.Linq.Expressions;

namespace Shoplio.Infrastructure.Data.Repositories.Common
{
    public class Repository<T> : BaseRepository<T>, IReadRepository<T>, IWriteRepository<T>
        where T : class
    {
        public Repository(AppDbContext context) : base(context) { }

        // 🔍 Get by Id
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // 📋 Get All
        public async Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IQueryable<T>>? include = null,
            bool asNoTracking = true)
        {
            IQueryable<T> query = _dbSet;

            if (asNoTracking)
                query = query.AsNoTracking();

            // 🔥 APPLY FILTER
            if (filter != null)
                query = query.Where(filter);

            // 🔥 APPLY INCLUDE
            if (include != null)
                query = include(query);

            return await query.ToListAsync();
        }

        // 🔍 Filtered Get
        public async Task<List<T>> GetAsync(
            System.Linq.Expressions.Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null)
                query = include(query);

            return await query.Where(predicate).ToListAsync();
        }

        // 📄 Pagination
        public async Task<(List<T> Items, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            System.Linq.Expressions.Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            int totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        // ➕ Add
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        // ➕ Add Range
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        // ✏️ Update
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        // ❌ Delete
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}