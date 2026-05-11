using MediatR;
using Services.Application.Abstractions;
using Services.Application.Commands;
using Services.Application.Results;

namespace Services.Application.Handlers;

public class EditSpecializationHandler : IRequestHandler<EditSpecializationCommand, Result>
{
    private readonly ISpecializationRepository _repository;

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

        _repository.UpdateAsync(specialization);

        await _repository.SaveChangesAsync();

        return Result.Success();
    }
}