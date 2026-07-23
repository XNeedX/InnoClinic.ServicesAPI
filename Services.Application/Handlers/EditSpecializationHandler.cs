using InnoClinic.Contracts.Enums;
using InnoClinic.Contracts.Events.Services;
using MassTransit;
using MediatR;
using Services.Application.Abstractions;
using Services.Application.Commands;
using Services.Application.Results;

namespace Services.Application.Handlers;

public class EditSpecializationHandler : IRequestHandler<EditSpecializationCommand, Result>
{
    private readonly ISpecializationRepository _repository;
    private readonly IPublishEndpoint _publishEndpoint;

    public EditSpecializationHandler(ISpecializationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(EditSpecializationCommand request, CancellationToken cancellationToken)
    {
        var specialization = await _repository.GetByIdAsync(request.Id);

        if (specialization == null)
            return SpecializationErrors.SpecializationNotFound;

        specialization.Name = request.Name;
        specialization.Price = request.Price;
        specialization.Status = request.Status;
        specialization.Category = request.Category;

        await _repository.SaveChangesAsync();

        await _publishEndpoint.Publish<ISpecializationUpdatedEvent>(new
        {
            Id = specialization.Id,
            Name = specialization.Name,
            Price = specialization.Price,
            Category = (ServiceCategory)specialization.Category,
            Status = (ServiceStatus)specialization.Status
        }, cancellationToken);

        return Result.Success();
    }
}