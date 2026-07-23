using InnoClinic.Contracts.Enums;
using InnoClinic.Contracts.Events.Services;
using MassTransit;
using MediatR;
using Services.Application.Abstractions;
using Services.Application.Commands;
using Services.Application.Results;
using Services.Domain.Models;

namespace Services.Application.Handlers;

public sealed class CreateServiceHandler : IRequestHandler<CreateServiceCommand, Result<Guid>>
{
    private readonly IServiceRepository _serviceRepository;
    private readonly ISpecializationRepository _specializationRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateServiceHandler(IServiceRepository serviceRepository, 
        ISpecializationRepository specializationRepository,
        IPublishEndpoint publishEndpoint)
    {
        _serviceRepository = serviceRepository;
        _specializationRepository = specializationRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Result<Guid>> Handle(CreateServiceCommand command, CancellationToken cancellationToken)
    {
        var specialization = await _specializationRepository.GetByIdAsync(command.SpecializationId);

        if (specialization == null)
            return Result<Guid>.Failure(SpecializationErrors.SpecializationNotFound);

        var service = new Service() 
        {
            Name = command.Name,
            Price = command.Price,
            Status = command.Status,
            Category = command.Category,
            SpecializationId = command.SpecializationId
        };

        await _serviceRepository.AddAsync(service);

        await _serviceRepository.SaveChangesAsync();

        await _publishEndpoint.Publish<IServiceCreatedEvent>(new
        {
            Id = service.Id,
            Name = service.Name,
            Price = service.Price,
            Category = (ServiceCategory)service.Category, 
            Status = (ServiceStatus)service.Status,      
            SpecializationId = service.SpecializationId
        }, cancellationToken);

        return Result<Guid>.Success(service.Id);
    }
}
