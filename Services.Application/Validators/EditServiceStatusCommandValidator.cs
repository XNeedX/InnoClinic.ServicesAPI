using FluentValidation;
using Services.Application.Commands;

namespace Services.Application.Validators;

public class EditServiceStatusCommandValidator : AbstractValidator<EditServiceStatusCommand>
{
    public EditServiceStatusCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Service ID is required");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid status");
    }
}