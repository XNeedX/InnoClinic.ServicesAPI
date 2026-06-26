using Xunit;
using FluentValidation.TestHelper;
using Services.Application.Commands;
using Services.Domain.Models;
using Services.Application.Validators;
using System;

namespace Services.Application.UnitTests.Validators;

public class CreateServiceCommandValidatorTest
{
    private readonly CreateServiceCommandValidator _validator = new();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    public void WhenNameIsInvalid_ShouldHaveError(string invalidName)
    {
        var command = new CreateServiceCommand(invalidName, 100, Status.Active, Category.Analyses, Guid.NewGuid());
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name).WithErrorMessage("Please, enter the name");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-100.50)]
    public void WhenPriceIsInvalid_ShouldHaveError(double invalidPrice)
    {
        var command = new CreateServiceCommand("Name", (decimal)invalidPrice, Status.Active, Category.Analyses, Guid.NewGuid());
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Price).WithErrorMessage("You've entered an invalid price");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(999)]
    public void WhenStatusIsInvalid_ShouldHaveError(int invalidStatus)
    {
        var command = new CreateServiceCommand("Name", 100, (Status)invalidStatus, Category.Analyses, Guid.NewGuid());
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Status).WithErrorMessage("Invalid status");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(999)]
    public void WhenCategoryIsInvalid_ShouldHaveError(int invalidCategory)
    {
        var command = new CreateServiceCommand("Name", 100, Status.Active, (Category)invalidCategory, Guid.NewGuid());
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Category).WithErrorMessage("Please, choose the service category");
    }

    [Fact]
    public void WhenSpecializationIdIsEmpty_ShouldHaveError()
    {
        var command = new CreateServiceCommand("Name", 100, Status.Active, Category.Analyses, Guid.Empty);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.SpecializationId).WithErrorMessage("Specialization ID is required");
    }
}