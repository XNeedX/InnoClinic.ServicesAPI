using MediatR;
using Services.Application.Results;
using Services.Domain.Models;

namespace Services.Application.Commands;

public sealed record EditServiceStatusCommand(Guid Id, Status Status) : IRequest<Result>;