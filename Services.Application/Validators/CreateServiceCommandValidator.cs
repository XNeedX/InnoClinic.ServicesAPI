using FluentValidation;
using Services.Application.Commands;

namespace Services.Application.Validators;

public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
{
    public CreateServiceCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Please, enter the name");

        RuleFor(x => x.Price)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Please, enter the price")
            .GreaterThan(0).WithMessage("You've entered an invalid price");

        RuleFor(x => x.Category)
            .IsInEnum().WithMessage("Please, choose the service category");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid status");

        RuleFor(x => x.SpecializationId)
            .NotEmpty().WithMessage("Specialization ID is required");
    }
}