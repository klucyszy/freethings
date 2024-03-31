using FluentAssertions;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;

namespace Freethings.UnitTests.Shared.Domain.BusinessOperations;

public sealed class BusinessErrorExtensionsTests
{
    [Fact]
    public void Format_OnPlainBusinessErrorShouldHaveNoEffect()
    {
        // Arrange
        BusinessError? error = BusinessError
            .Create("message", "test");

        // Act
        BusinessError? formatted = error.Format();

        // Assert
        formatted.Should().BeEquivalentTo(error);
    }
    
    [Fact]
    public void Format_WhenLessArgumentsThanPlacesToFillThenExceptionThrown()
    {
        // Arrange
        BusinessError? error = BusinessError
            .Create("message {Test1}", "test");

        // Act
        Action act = () => error.Format();

        // Assert
        act.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void Format_WhenMoreArgumentsThanPlacesToFillThenExceptionThrown()
    {
        // Arrange
        BusinessError? error = BusinessError
            .Create("message {Test1}", "test");

        // Act
        Action act = () => error.Format("value1", "value2");

        // Assert
        act.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void Format_FormattedApplied()
    {
        // Arrange
        BusinessError? error = BusinessError
            .Create("message {0}", "test");

        // Act
        BusinessError formatted = error.Format("value1");

        // Assert
        formatted.Message.Should().Be("message value1");
    }
}