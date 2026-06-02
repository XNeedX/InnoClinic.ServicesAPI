using Services.Application.Results;
using Services.Domain.Models;

namespace Services.Application.Abstractions;

public interface ISpecializationRepository : IRepository<Specialization, Guid>
{
    Task<Specialization?> GetByIdWithServiceAsync(Guid id, CancellationToken cancellationToken = default);
}
