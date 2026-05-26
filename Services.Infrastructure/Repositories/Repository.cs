using Microsoft.EntityFrameworkCore;
using Services.Application.Abstractions;
using Services.Infrastructure.Data;
using System.Linq.Expressions;

namespace Services.Infrastructure.Repositories;

internal class Repository<T, K> : IRepository<T, K>
    where T : class
{
    protected readonly ServicesDbContext _dbContext;
    protected readonly DbSet<T> _dbset;

    public Repository(ServicesDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbset = _dbContext.Set<T>();
    }

    public async Task AddAsync(T entity) => await _dbset.AddAsync(entity);

    public async Task<IEnumerable<T>> FindByFilterAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbset.Where(expression).AsNoTracking();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbset.AsNoTracking().ToListAsync();

    public Task<T?> GetByIdAsync(K id) => _dbset.FindAsync(id).AsTask();

    public Task SaveChangesAsync() => _dbContext.SaveChangesAsync();
}
