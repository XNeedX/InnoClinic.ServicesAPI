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

    //public async Task<Result> ChangeStatus(Guid serviceId, Status newStatus)
    //{
    //    var service = await GetByIdAsync(serviceId);

    //    if (service == null)
    //        return ServiceErrors.ServiceNotFound;

    //    service.Status = newStatus;

    //    return Result.Success();
    //}
}
