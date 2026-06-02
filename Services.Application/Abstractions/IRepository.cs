using MassTransit.Serialization;
using Services.Application.Models;
using System.Linq.Expressions;

namespace Services.Application.Abstractions;
public interface IRepository<T, K> where T : class
{
    Task<T?> GetByIdAsync(K id, CancellationToken cancellationToken = default);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<PagedResult<T>> GetAllPagedAsync(PageParams pageParams);
    Task<IEnumerable<T>> FindByFilterAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, Object>>[] includes);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
