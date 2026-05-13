using Services.Application.Results;
using Services.Application.Commands;
using Services.Domain.Models;
using MediatR;
using Services.Application.Abstractions;

namespace Services.Application.Handlers;

public sealed class CreateSpecializationHandler : IRequestHandler<CreateSpecializationCommand, Result<Guid>>
{
    private readonly ISpecializationRepository _specializationRepository;

    public CreateSpecializationHandler(ISpecializationRepository specializationRepository)
    {
        _specializationRepository = specializationRepository;
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

        return Result<Guid>.Success(specialization.Id);
    }
}
