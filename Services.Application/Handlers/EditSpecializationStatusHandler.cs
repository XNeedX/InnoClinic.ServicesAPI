using InnoClinic.Contracts.Enums;
using InnoClinic.Contracts.Events.Services;
using MassTransit;
using MediatR;
using Services.Application.Abstractions;
using Services.Application.Commands;
using Services.Application.Results;

namespace Services.Application.Handlers;

public class EditSpecializationStatusHandler : IRequestHandler<EditSpecializationStatusCommand, Result>
{
    private readonly ISpecializationRepository _repository;
    private readonly IPublishEndpoint _publishEndpoint;

    public EditSpecializationStatusHandler(ISpecializationRepository repository,
        IPublishEndpoint publishEndpoint)
    {
        _repository = repository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Result> Handle(EditSpecializationStatusCommand command, CancellationToken cancellationToken)
    {
        var specialization = await _repository.GetByIdAsync(command.Id);

        if (specialization == null)
            return SpecializationErrors.SpecializationNotFound;

        specialization.Status = command.Status;

        await _repository.SaveChangesAsync();

        await _publishEndpoint.Publish<ISpecializationStatusUpdatedEvent>(new
        {
            Id = specialization.Id,
            Status = (ServiceStatus)specialization.Status
        }, cancellationToken);

        return Result.Success();
    }
}