using Services.Domain.Models;
using MediatR;
using Services.Application.Results;

namespace Services.Application.Commands;

public sealed record CreateSpecializationCommand(string Name, decimal Price, Status Status, Category Category) 
    : IRequest<Result<Guid>>
{
}