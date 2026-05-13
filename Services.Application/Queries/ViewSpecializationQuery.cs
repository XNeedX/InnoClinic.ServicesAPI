using MediatR;
using Services.Application.DTOs;
using Services.Application.Results;

namespace Services.Application.Queries;

public sealed record ViewSpecializationQuery(Guid Id) 
    : IRequest<Result<ViewSpecializationDTO>>
{
}
