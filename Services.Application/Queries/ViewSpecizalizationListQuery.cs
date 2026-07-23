using MediatR;
using Services.Application.DTOs;
using Services.Application.Models;
using Services.Application.Results;

namespace Services.Application.Queries;

public sealed record ViewSpecizalizationListQuery(PageParams PageParams)
    : IRequest<Result<PagedResult<ViewSpecializationListDTO>>>;
