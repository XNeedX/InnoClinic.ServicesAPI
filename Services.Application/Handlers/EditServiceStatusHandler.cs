using InnoClinic.Contracts.Enums;
using InnoClinic.Contracts.Events.Services;
using MassTransit;
using MediatR;
using Services.Application.Abstractions;
using Services.Application.Commands;
using Services.Application.Results;

namespace Services.Application.Handlers;

public class EditServiceStatusHandler : IRequestHandler<EditServiceStatusCommand, Result>
{
    private readonly IServiceRepository _repository;
    private readonly IPublishEndpoint _publishEndpoint;

    public EditServiceStatusHandler(IServiceRepository repository,
        IPublishEndpoint publishEndpoint)
    {
        _repository = repository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Result> Handle(EditServiceStatusCommand command, CancellationToken cancellationToken)
    {
        var service = await _repository.GetByIdAsync(command.Id);

        if (service == null)
            return ServiceErrors.ServiceNotFound;

        service.Status = command.Status;

        await _repository.SaveChangesAsync();

        await _publishEndpoint.Publish<IServiceStatusUpdatedEvent>(new
        {
            Id = service.Id,
            Status = (ServiceStatus)service.Status
        }, cancellationToken);

        return Result.Success();
    }
}