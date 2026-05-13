using FluentValidation;
using Services.Application.Commands;
using Services.Domain.Models;

namespace Services.Application.Validators;

public class EditStatusCommandValidator<TEntity> : AbstractValidator<EditStatusCommand<TEntity>>
    where TEntity : Entity
{
    public EditStatusCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Entity ID is required");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid status");
    }
}