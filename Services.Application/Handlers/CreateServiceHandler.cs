using Services.Application.Abstractions;
using Services.Application.Commands;
using Services.Application.Results;
using Services.Domain.Models;
using MediatR;

namespace Services.Application.Handlers;

internal sealed class CreateServiceHandler : IRequestHandler<CreateServiceCommand, Result<Guid>>
{
    private readonly IServiceRepository _serviceRepository;

    public CreateServiceHandler(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository; 
    }

    public async Task<Result<Guid>> Handle(CreateServiceCommand command, CancellationToken cancellationToken)
    {
        var service = new Service() 
        {
            Name = command.Name,
            Price = command.Price,
            Status = command.Status,
            Category = command.Category,
        };

        await _serviceRepository.AddAsync(service);

        await _serviceRepository.SaveChangesAsync();

        return Result<Guid>.Success(service.Id);
    }
}
