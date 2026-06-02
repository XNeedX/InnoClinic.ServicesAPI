using Services.Application.Abstractions;
using Services.Application.Results;
using Services.Domain.Models;
using Services.Infrastructure.Data;

namespace Services.Infrastructure.Repositories;

internal sealed class ServiceRepository : Repository<Service, Guid>, IServiceRepository
{
    public ServiceRepository(ServicesDbContext dbContext) 
        : base(dbContext)
    {
    }
}
