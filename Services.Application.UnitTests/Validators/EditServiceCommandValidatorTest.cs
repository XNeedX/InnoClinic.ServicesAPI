using Xunit;
using FluentValidation.TestHelper;
using Services.Application.Commands;
using Services.Domain.Models;
using Services.Application.Validators;
using System;

namespace Services.Application.UnitTests.Validators;

public class EditServiceCommandValidatorTest
{
    private readonly EditServiceCommandValidator _validator = new();

    [Fact]
    public void WhenGuidIdIsInvalid_ShouldHaveError()
    {
        var command = new EditServiceCommand(Guid.Empty, "Name", Status.Active, 100, Category.Analyses);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Id).WithErrorMessage("Service ID is required");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\n")]
    public void WhenNameIsInvalid_ShouldHaveError(string invalidName)
    {
        var command = new EditServiceCommand(Guid.NewGuid(), invalidName, Status.Active, 100, Category.Analyses);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name).WithErrorMessage("Please, enter the name");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-0.01)]
    [InlineData(-9999.99)]
    public void WhenPriceIsInvalid_ShouldHaveError(double invalidPrice)
    {
        var command = new EditServiceCommand(Guid.NewGuid(), "Name", Status.Active, (decimal)invalidPrice, Category.Analyses);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Price).WithErrorMessage("You've entered an invalid price");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(999)]
    public void WhenStatusIsInvalid_ShouldHaveError(int invalidStatus)
    {
        var command = new EditServiceCommand(Guid.NewGuid(), "Name", (Status)invalidStatus, 100, Category.Analyses);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Status).WithErrorMessage("Invalid status");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(999)]
    public void WhenCategoryIsInvalid_ShouldHaveError(int invalidCategory)
    {
        var command = new EditServiceCommand(Guid.NewGuid(), "Name", Status.Active, 100, (Category)invalidCategory);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Category).WithErrorMessage("Please, choose the service category");
    }
}