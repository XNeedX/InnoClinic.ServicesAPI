using MediatR;
using Services.Application.Abstractions;
using Services.Application.DTOs;
using Services.Application.Queries;
using Services.Application.Results;

namespace Services.Application.Handlers;

public class ViewSpecializationListHandler : IRequestHandler<ViewSpecizalizationListQuery, Result<List<ViewSpecializationListDTO>>>
{
    private readonly ISpecializationRepository _repository;
    public ViewSpecializationListHandler(ISpecializationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<ViewSpecializationListDTO>>> Handle(ViewSpecizalizationListQuery request, CancellationToken cancellationToken)
    {
        var specializations = await _repository.GetAllAsync();

        if (specializations == null)
            return SpecializationErrors.SpecializationNotFound;

        var specializationDTOs = specializations.Select(s => new ViewSpecializationListDTO(
            Id: s.Id,
            Name: s.Name,
            Status: s.Status
        )).ToList();

        return Result<List<ViewSpecializationListDTO>>.Success(specializationDTOs);
    }
}
