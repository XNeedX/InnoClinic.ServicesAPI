using Services.Application.Abstractions;
using Services.Application.Results;
using Services.Domain.Models;
using Services.Infrastructure.Data;

namespace Services.Infrastructure.Repositories;

internal sealed class SpecializationRepository : Repository<Specialization, Guid>, ISpecializationRepository
{
    public SpecializationRepository(ServicesDbContext dbContext)
        : base(dbContext) 
    { 
    }

    //public async Task<Result> ChangeStatus(Guid specializationId, string newStatus)
    //{
    //    var specialization = await GetByIdAsync(specializationId);

    //    if (specialization == null)
    //        return SpecializationErrors.SpecializationNotFound;

    //    return Result.Success();
    //}
}
