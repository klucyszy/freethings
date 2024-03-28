using System.Reflection;
using FluentAssertions;
using FluentAssertions.Execution;
using NetArchTest.Rules;

namespace Freethings.ArchTests;

public sealed class AuctionModuleTests
{
    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOnAboveLayers()
    {
        // Act
        var result = GetResultOnDomainModuleLayerDoesNotHaveDependencyOnAboveLayers(
            Assembly.Load("Freethings"),
            "Freethings.Auctions");
        
        // Assert
        using (new AssertionScope())
        {
            result.IsSuccessful.Should().BeTrue();
            result.FailingTypes.Should().BeNullOrEmpty();
        }
    }
    
    private TestResult GetResultOnDomainModuleLayerDoesNotHaveDependencyOnAboveLayers(Assembly assembly, string moduleNamespace)
    {
        return Types.InAssembly(assembly)
            .That()
            .ResideInNamespace($"{moduleNamespace}.Domain")
            .Should()
            .NotHaveDependencyOn($"{moduleNamespace}.Application")
            .And().NotHaveDependencyOn($"{moduleNamespace}.Infrastructure")
            .And().NotHaveDependencyOn($"{moduleNamespace}.Presentation")
            .GetResult();
    }
}

