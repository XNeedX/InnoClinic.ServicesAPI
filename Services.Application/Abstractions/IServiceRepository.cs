using Services.Application.Results;
using Services.Domain.Models;
using System.Linq.Expressions;

namespace Services.Application.Abstractions;

public interface IServiceRepository : IRepository<Service, Guid>
{
}
