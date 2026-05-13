using MediatR;
using Services.Application.Abstractions;
using Services.Application.Commands;
using Services.Application.Results;
using Services.Domain.Models;

namespace Services.Application.Handlers;

public class EditStatusHandler<TEntity> : IRequestHandler<EditStatusCommand<TEntity>, Result>
    where TEntity : Entity
{
    private readonly IRepository<TEntity, Guid> _repository;
    public EditStatusHandler(IRepository<TEntity, Guid> repository)
    {
        _repository = repository;
    }
    public async Task<Result> Handle(EditStatusCommand<TEntity> command, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(command.Id);

        if (entity == null)
            return Result.Failure(new Error("Not found", $"Entity of type {typeof(TEntity).Name} with ID {command.Id} was not found."));

        entity.Status = command.Status;

        await _repository.SaveChangesAsync();

        return Result.Success();
    }
}