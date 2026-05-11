using MediatR;
using Services.Application.Results;
using Services.Application.DTOs;

namespace Services.Application.Queries;

public sealed record ViewServiceQuery(Guid Id) 
    : IRequest<Result<ViewServiceDTO>>
{
}
