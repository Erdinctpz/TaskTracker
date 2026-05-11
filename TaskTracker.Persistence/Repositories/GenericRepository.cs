using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Name;
using TaskTracker.Persistence.Context;

namespace TaskTracker.Persistence.Repositories
{
    public class GenericRepository<T>(
        AppDbContext dbContext
    ) : IGenericRepository<T> where T : class
    {
        public async Task<T> DeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
        {
            IQueryable<T> query = dbContext.Set<T>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var entity = await dbContext.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<T> InsertAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            dbContext.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}