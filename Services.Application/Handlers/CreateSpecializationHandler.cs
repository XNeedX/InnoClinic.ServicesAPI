using Services.Application.Results;
using Services.Application.Commands;
using Services.Domain.Models;
using MediatR;
using Services.Application.Abstractions;
using MassTransit;
using InnoClinic.Contracts.Events.Services;
using InnoClinic.Contracts.Enums;

namespace Services.Application.Handlers;

public sealed class CreateSpecializationHandler : IRequestHandler<CreateSpecializationCommand, Result<Guid>>
{
    private readonly ISpecializationRepository _specializationRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateSpecializationHandler(ISpecializationRepository specializationRepository,
        IPublishEndpoint publishEndpoint)
    {
        _specializationRepository = specializationRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Result<Guid>> Handle(CreateSpecializationCommand command, CancellationToken cancellationToken)
    {
        var specialization = new Specialization()
        {
            Name = command.Name,
            Price = command.Price,
            Status = command.Status,
            Category = command.Category
        };

        await _specializationRepository.AddAsync(specialization);

        await _specializationRepository.SaveChangesAsync();

        await _publishEndpoint.Publish<ISpecializationCreatedEvent>(new
        {
            Id = specialization.Id,
            Name = specialization.Name,
            Price = specialization.Price,
            Category = (ServiceCategory)specialization.Category,
            Status = (ServiceStatus)specialization.Status
        });

        return Result<Guid>.Success(specialization.Id);
    }
}
