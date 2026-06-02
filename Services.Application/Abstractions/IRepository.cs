using Services.Application.Models;
using System.Linq.Expressions;

namespace Services.Application.Abstractions;
public interface IRepository<T, K> where T : class
{
    Task<T> GetByIdAsync(K id);
    Task AddAsync(T entity);
    Task<PagedResult<T>> GetAllPagedAsync(PageParams pageParams);
    Task<IEnumerable<T>> FindByFilterAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, Object>>[] includes);
    Task SaveChangesAsync();
}
