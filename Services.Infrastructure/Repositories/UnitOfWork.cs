using Services.Application.Abstractions;
using Services.Infrastructure.Data;

namespace Services.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ServicesDbContext _dbContext;

    public UnitOfWork(ServicesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Dispose()
    {
        bool disposed = false;
        if (!disposed)
        {
            _dbContext.Dispose();
            disposed = true;
        }
    }

    public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}