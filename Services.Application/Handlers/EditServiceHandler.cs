using MediatR;
using Services.Application.Abstractions;
using Services.Application.Commands;
using Services.Application.DTOs;
using Services.Application.Results;

namespace Services.Application.Handlers;

public class EditServiceHandler : IRequestHandler<EditServiceCommand, Result>
{
    private readonly IServiceRepository _repository;

    public EditServiceHandler(IServiceRepository repository)
    {
        _repository = repository;
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

        _repository.UpdateAsync(service);

        await _repository.SaveChangesAsync();

        return Result.Success();
    }
}
