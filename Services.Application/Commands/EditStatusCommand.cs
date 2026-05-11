using MediatR;
using Services.Application.DTOs;
using Services.Application.Results;
using Services.Domain.Models;

namespace Services.Application.Commands;

public sealed record EditStatusCommand<TEntity>(Guid Id, Status Status) : IRequest<Result>
    where TEntity : Entity
{
}
