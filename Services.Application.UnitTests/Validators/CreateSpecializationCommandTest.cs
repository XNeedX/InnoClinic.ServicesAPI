using FluentValidation.TestHelper;
using MassTransit.Testing.Implementations;
using Services.Application.Commands;
using Services.Application.Validators;
using Services.Domain.Models;
using Xunit;

namespace Services.Application.UnitTests.Validators;

public class CreateSpecializationCommandTest
{
    private readonly CreateSpecializationCommandValidator _validator = new();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void WhenNameIsInvalid_ShouldHaveError(string invalidName)
    {
        var command = new CreateSpecializationCommand(
            invalidName,
            1000,
            Status.Active,
            Category.Analyses
        );
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name).WithErrorMessage("Please, enter the name");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-1000)]
    public void WhenPriceIsInvalid_ShouldHaveError(decimal invalidPrice)
    {
        var command = new CreateSpecializationCommand(
            "Uzi",
            invalidPrice,
            Status.Active,
            Category.Diagnostics
        );
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Price).WithErrorMessage("You've entered an invalid price");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-1000)]
    public void WhenStatusIsInvalid_ShouldHaveError(decimal invalidStatus)
    {
        var command = new CreateSpecializationCommand(
            "Uzi",
            100,
            (Status)invalidStatus,
            Category.Diagnostics
        );
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Status).WithErrorMessage("Invalid status");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-1000)]
    [InlineData(1000)]
    public void WhenCategoryIsInvalid_ShouldHaveError(decimal invalidCategory)
    {
        var command = new CreateSpecializationCommand(
            "Uzi",
            100,
            Status.Active,
            (Category)invalidCategory
        );
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Category).WithErrorMessage("Please, choose the service category");
    }
}
