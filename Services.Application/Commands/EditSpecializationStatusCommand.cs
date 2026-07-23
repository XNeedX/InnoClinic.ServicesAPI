using MediatR;
using Services.Application.Results;
using Services.Domain.Models;

namespace Services.Application.Commands;

public sealed record EditSpecializationStatusCommand(Guid Id, Status Status) : IRequest<Result>;