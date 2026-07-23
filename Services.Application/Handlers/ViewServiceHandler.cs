using MediatR;
using Services.Application.Abstractions;
using Services.Application.DTOs;
using Services.Application.Queries;
using Services.Application.Results;

namespace Services.Application.Handlers;

public sealed class ViewServiceHandler : IRequestHandler<ViewServiceQuery, Result<ViewServiceDTO>>
{
    private readonly IServiceRepository _serviceRepository;

    public ViewServiceHandler(IServiceRepository serviceRepository) 
    {
        _serviceRepository = serviceRepository; 
    }

    public async Task<Result<ViewServiceDTO>> Handle(ViewServiceQuery query, CancellationToken cancellationToken)
    {
        var service  = await _serviceRepository.GetByIdAsync(query.Id);

        if (service == null)
            return ServiceErrors.ServiceNotFound;

        var dto = new ViewServiceDTO
        (
            Id: service.Id,
            Name: service.Name,
            Price: service.Price,
            Category: service.Category,
            Status: service.Status,
            SpecializationId: service.SpecializationId
        );

        return Result<ViewServiceDTO>.Success(dto);
    }
}
