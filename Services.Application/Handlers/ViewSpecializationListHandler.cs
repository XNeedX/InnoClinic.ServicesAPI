using MediatR;
using Services.Application.Abstractions;
using Services.Application.DTOs;
using Services.Application.Models;
using Services.Application.Queries;
using Services.Application.Results;

namespace Services.Application.Handlers;

public class ViewSpecializationListHandler : IRequestHandler<ViewSpecizalizationListQuery, Result<PagedResult<ViewSpecializationListDTO>>>
{
    private readonly ISpecializationRepository _repository;
    public ViewSpecializationListHandler(ISpecializationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<PagedResult<ViewSpecializationListDTO>>> Handle(ViewSpecizalizationListQuery request, CancellationToken cancellationToken)
    {
        var pagedEntities = await _repository.GetAllPagedAsync(request.PageParams);

        var specializationDTOs = pagedEntities.Items.Select(s => new ViewSpecializationListDTO(
            Id: s.Id,
            Name: s.Name,
            Status: s.Status
        )).ToList();

        var pagedResult = new PagedResult<ViewSpecializationListDTO>(specializationDTOs, pagedEntities.TotalCount);

        return Result<PagedResult<ViewSpecializationListDTO>>.Success(pagedResult);
    }
}
