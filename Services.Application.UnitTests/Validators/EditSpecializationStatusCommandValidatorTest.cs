using FluentValidation.TestHelper;
using Services.Application.Commands;
using Services.Application.Validators;
using Services.Domain.Models;
using Xunit;

namespace Services.Application.UnitTests.Validators;

public class EditSpecializationStatusCommandValidatorTest
{
    private readonly EditSpecializationStatusCommandValidator _validator = new();

    [Fact]
    public void WhenGuidIdIsInvalid_ShouldHaveError()
    {
        var command = new EditSpecializationStatusCommand(Guid.Empty, Status.Inactive);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Id).WithErrorMessage("Specialization ID is required");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(999)]
    public void WhenStatusIsInvalid_ShouldHaveError(int invalidStatus)
    {
        var command = new EditSpecializationStatusCommand(Guid.NewGuid(), (Status)invalidStatus);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Status).WithErrorMessage("Invalid status");
    }
}