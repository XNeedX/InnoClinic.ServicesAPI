using MediatR;
using Services.Application.Queries;
using Services.Application.Results;
using Services.Application.DTOs;
using Services.Application.Abstractions;

namespace Services.Application.Handlers;

public class ViewSpecializationHandler : IRequestHandler<ViewSpecializationQuery, Result<ViewSpecializationDTO>>
{
    private readonly ISpecializationRepository _repository;
    public ViewSpecializationHandler(ISpecializationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<ViewSpecializationDTO>> Handle(ViewSpecializationQuery query, CancellationToken cancellationToken)
    {
        var specialization = await _repository.GetByIdWithServiceAsync(query.Id);

        if (specialization == null)
            return SpecializationErrors.SpecializationNotFound;

        var dto = new ViewSpecializationDTO(
            Id: specialization.Id,
            Name: specialization.Name,
            Status: specialization.Status,
            Services: specialization.Services.Select(s => new ViewSpecializationServiceDTO(
                Id: s.Id,
                Name: s.Name,
                Price: s.Price,
                Category: s.Category,
                Status: s.Status
            )).ToList()
        );

        return Result<ViewSpecializationDTO>.Success(dto);
    }
}
