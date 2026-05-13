using Services.Domain.Models;
using MediatR;
using Services.Application.Results;

namespace Services.Application.Commands;

public sealed record CreateServiceCommand(string Name, decimal Price, Status Status,  Category Category, Guid SpecializationId) 
    : IRequest<Result<Guid>>
{
}
