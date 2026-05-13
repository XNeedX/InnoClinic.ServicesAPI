using Services.Application.Abstractions;
using Services.Application.Results;
using Services.Domain.Models;
using Services.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Services.Infrastructure.Repositories;

internal sealed class SpecializationRepository : Repository<Specialization, Guid>, ISpecializationRepository
{
    public SpecializationRepository(ServicesDbContext dbContext)
        : base(dbContext) 
    { 
    }

    public Task<Specialization> GetByIdWithServiceAsync(Guid id)
    {
        return _dbset
            .Include(sp => sp.Services)
            .FirstOrDefaultAsync(sp => sp.Id == id);
    }
}
