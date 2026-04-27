using Microsoft.EntityFrameworkCore;
using Services.Application.Abstractions;
using Services.Infrastructure.Data;

namespace Services.Infrastructure.Repositories;

internal class Repository<T> : IRepository<T> where T : class
{
    protected readonly ServicesDbContext _dbContext;
    protected readonly DbSet<T> _dbset;

    public Repository(ServicesDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbset = _dbContext.Set<T>();
    }

    public async Task AddAsync(T entity) => await _dbset.AddAsync(entity);

    public async Task<T> GetByIdAsync(Guid id) => await _dbset.FindAsync(id);

    public IQueryable<T> Query() => _dbset.AsQueryable();
}
