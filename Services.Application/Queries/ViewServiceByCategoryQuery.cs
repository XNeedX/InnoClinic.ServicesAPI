using MediatR;
using Services.Application.DTOs;
using Services.Application.Results;
using Services.Domain.Models;

namespace Services.Application.Queries;

public sealed record ViewServiceByCategoryQuery(Category Category) 
    : IRequest<Result<ViewCategoryDataDTO>>;
