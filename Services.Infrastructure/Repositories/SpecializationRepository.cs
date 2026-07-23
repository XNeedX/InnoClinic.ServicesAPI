using Microsoft.EntityFrameworkCore;
using Services.Application.Abstractions;
using Services.Domain.Models;
using Services.Infrastructure.Data;

namespace Services.Infrastructure.Repositories;

internal sealed class SpecializationRepository : Repository<Specialization, Guid>, ISpecializationRepository
{
    public SpecializationRepository(ServicesDbContext dbContext)
        : base(dbContext) 
    { 
    }

    public Task<Specialization?> GetByIdWithServiceAsync(Guid id, CancellationToken cancellationToken = default) => 
        _dbset.AsNoTracking().Include(sp => sp.Services).FirstOrDefaultAsync(sp => sp.Id == id);
}
