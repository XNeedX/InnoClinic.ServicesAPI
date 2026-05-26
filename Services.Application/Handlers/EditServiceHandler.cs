using InnoClinic.Contracts.Enums;
using InnoClinic.Contracts.Events.Services;
using MassTransit;
using MediatR;
using Services.Application.Abstractions;
using Services.Application.Commands;
using Services.Application.Results;

namespace Services.Application.Handlers;

public class EditServiceHandler : IRequestHandler<EditServiceCommand, Result>
{
    private readonly IServiceRepository _repository;
    private readonly IPublishEndpoint _publishEndpoint;

    public EditServiceHandler(IServiceRepository repository,
        IPublishEndpoint publishEndpoint)
    {
        _repository = repository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Result> Handle(EditServiceCommand request, CancellationToken cancellationToken)
    {
        var service = await _repository.GetByIdAsync(request.id);
        if (service == null)
            return ServiceErrors.ServiceNotFound;

        service.Name = request.Name;
        service.Price = request.Price;
        service.Status = request.Status;
        service.Category = request.Category;

        await _repository.SaveChangesAsync();

        await _publishEndpoint.Publish<IServiceUpdatedEvent>(
            new
        {
            Id = service.Id,
            Name = service.Name,
            Price = service.Price,
            Category = (ServiceCategory)service.Category,
            Status = (ServiceStatus)service.Status,
            SpecializationId = service.SpecializationId
        });

        return Result.Success();
    }
}
