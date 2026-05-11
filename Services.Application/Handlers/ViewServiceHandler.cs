using Services.Application.Abstractions;
using Services.Application.Abstractions.Messaging;
using Services.Application.DTOs;
using Services.Application.Queries;
using Services.Application.Results;
using MediatR;

namespace Services.Application.Handlers;

internal sealed class ViewServiceHandler : IRequestHandler<ViewServiceQuery, Result<ViewServiceDTO>>
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
            Status: service.Status
        );

        return Result<ViewServiceDTO>.Success(dto);
    }
}
