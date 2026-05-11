using MediatR;
using Services.Application.Results;
using Services.Domain.Models;

namespace Services.Application.Commands;

public sealed record EditServiceCommand(Guid id, string Name, Status Status, decimal Price, Category Category) 
    : IRequest<Result>
{
}
