using FluentValidation;
using FluentValidation.Results;
using FluentAssertions;
using MediatR;
using Moq;
using Services.Application.Behaviours;
using Services.Application.Results; 
using Xunit;

namespace Services.Application.UnitTests.Behaviours;

public class ValidationPipelineBehaviourTest
{
    public sealed record DummyCommand : IRequest<Result>;

    [Fact]
    public async Task Handle_ShouldCallNext_WhenNoValidatorsExist()
    {
        var validators = Enumerable.Empty<IValidator<DummyCommand>>();
        var behaviour = new ValidationPipelineBehaviour<DummyCommand, Result>(validators);

        var request = new DummyCommand();
        var nextDelegateMock = new Mock<RequestHandlerDelegate<Result>>();
        nextDelegateMock.Setup(n => n.Invoke()).ReturnsAsync(Result.Success());

        var result = await behaviour.Handle(request, nextDelegateMock.Object, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();

        nextDelegateMock.Verify(n => n.Invoke(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallNext_WhenValidationIsSuccessful()
    {
        var request = new DummyCommand();
        
        var validatorMock = new Mock<IValidator<DummyCommand>>();
        validatorMock.Setup(v => v.Validate(request))
                     .Returns(new FluentValidation.Results.ValidationResult());

        var behaviour = new ValidationPipelineBehaviour<DummyCommand, Result>(new[] { validatorMock.Object });

        var nextDelegateMock = new Mock<RequestHandlerDelegate<Result>>();
        nextDelegateMock.Setup(n => n.Invoke()).ReturnsAsync(Result.Success());

        var result = await behaviour.Handle(request, nextDelegateMock.Object, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        nextDelegateMock.Verify(n => n.Invoke(), Times.Once); 
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationResult_WhenValidationFails()
    {
        var request = new DummyCommand();

        var validationFailure = new ValidationFailure("Property", "Error message");
        var failedValidationResult = new FluentValidation.Results.ValidationResult(new[] { validationFailure });

        var validatorMock = new Mock<IValidator<DummyCommand>>();
        validatorMock.Setup(v => v.Validate(request)).Returns(failedValidationResult);

        var behaviour = new ValidationPipelineBehaviour<DummyCommand, Result>(new[] { validatorMock.Object });

        var nextDelegateMock = new Mock<RequestHandlerDelegate<Result>>();

        var result = await behaviour.Handle(request, nextDelegateMock.Object, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();

        result.Should().BeAssignableTo<IValidationResult>();

        var validationResult = (IValidationResult)result;
        validationResult.Errors.Should().HaveCount(1);
        validationResult.Errors[0].Code.Should().Be("Property");
        validationResult.Errors[0].Message.Should().Be("Error message");

        nextDelegateMock.Verify(n => n.Invoke(), Times.Never);
    }
}
