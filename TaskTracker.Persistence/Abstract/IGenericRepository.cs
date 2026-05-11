using System.Linq.Expressions;

namespace Name
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
    }
}