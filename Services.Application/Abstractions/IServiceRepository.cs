using Services.Domain.Models;

namespace Services.Application.Abstractions;

public interface IServiceRepository : IRepository<Service, Guid>
{
}
