using System.Reflection;
using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using NetArchTest.Rules;

namespace Freethings.ArchTests;

public sealed class NamingConventionTests
{
    [Fact]
    public void HandlersShouldBeSuffixedWithHandler()
    {
        // Act
        var result = Types.InAssembly(Assembly.Load("Freethings"))
            .That()
            .ImplementInterface(typeof(IRequestHandler<,>))
            .Should()
            .BeSealed().And().HaveNameEndingWith("Handler").And().NotBePublic()
            .GetResult();
        
        // Assert
        using (new AssertionScope())
        {
            result.IsSuccessful.Should().BeTrue();
            result.FailingTypes.Should().BeNullOrEmpty();
        }
    }
}