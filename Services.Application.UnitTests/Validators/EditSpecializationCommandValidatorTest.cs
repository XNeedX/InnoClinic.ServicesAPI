using Xunit;
using FluentValidation.TestHelper;
using Services.Application.Commands;
using Services.Domain.Models;
using Services.Application.Validators;
using System;

namespace Services.Application.UnitTests.Validators;

public class EditSpecializationCommandValidatorTest
{
    private readonly EditSpecializationCommandValidator _validator = new();

    [Fact]
    public void WhenGuidIdIsInvalid_ShouldHaveError()
    {
        var command = new EditSpecializationCommand(Guid.Empty, "Name", 100, Status.Active, Category.Consultations);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Id).WithErrorMessage("Specialization ID is required");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void WhenNameIsInvalid_ShouldHaveError(string invalidName)
    {
        var command = new EditSpecializationCommand(Guid.NewGuid(), invalidName, 100, Status.Active, Category.Consultations);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name).WithErrorMessage("Please, enter the name");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-0.01)]
    public void WhenPriceIsInvalid_ShouldHaveError(double invalidPrice)
    {
        var command = new EditSpecializationCommand(Guid.NewGuid(), "Name", (decimal)invalidPrice, Status.Active, Category.Consultations);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Price).WithErrorMessage("You've entered an invalid price");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(500)]
    public void WhenStatusIsInvalid_ShouldHaveError(int invalidStatus)
    {
        var command = new EditSpecializationCommand(Guid.NewGuid(), "Name", 100, (Status)invalidStatus, Category.Consultations);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Status).WithErrorMessage("Invalid status");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(500)]
    public void WhenCategoryIsInvalid_ShouldHaveError(int invalidCategory)
    {
        var command = new EditSpecializationCommand(Guid.NewGuid(), "Name", 100, Status.Active, (Category)invalidCategory);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Category).WithErrorMessage("Please, choose the service category");
    }
}