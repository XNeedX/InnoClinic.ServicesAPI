using System.Linq.Expressions;

namespace Services.Application.Abstractions;
public interface IRepository<T, K> where T : class
{
    Task<T> GetByIdAsync(K id);
    Task AddAsync(T entity);
    void UpdateAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindByFilterAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, Object>>[] includes);
    Task SaveChangesAsync();
}
