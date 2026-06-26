using FluentValidation.TestHelper;
using Services.Application.Commands;
using Services.Application.Validators;
using Services.Domain.Models;
using Xunit;

namespace Services.Application.UnitTests.Validators;

public class EditServiceStatusCommandValidatorTest
{
    private readonly EditServiceStatusCommandValidator _validator = new();

    [Fact]
    public void WhenGuidIdIsInvalid_ShouldHaveError()
    {
        var command = new EditServiceStatusCommand(Guid.Empty, Status.Inactive);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Id).WithErrorMessage("Service ID is required");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(999)]
    public void WhenStatusIsInvalid_ShouldHaveError(int invalidStatus)
    {
        var command = new EditServiceStatusCommand(Guid.NewGuid(), (Status)invalidStatus);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Status).WithErrorMessage("Invalid status");
    }
}
