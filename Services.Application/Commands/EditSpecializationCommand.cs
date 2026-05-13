using MediatR;
using Services.Application.Results;
using Services.Domain.Models;

namespace Services.Application.Commands;

public sealed record EditSpecializationCommand(Guid Id, string Name, decimal Price, Status Status, Category Category) :
    IRequest<Result>
{
}
