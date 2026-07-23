using FluentValidation;
using Services.Application.Commands;

namespace Services.Application.Validators;

public class EditSpecializationStatusCommandValidator : AbstractValidator<EditSpecializationStatusCommand>
{
    public EditSpecializationStatusCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Specialization ID is required");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid status");
    }
}