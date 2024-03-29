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
        Assembly assembly = Assembly.Load("Freethings");
        string moduleNamespace = "Freethings.Auctions";
        TestResult result = GetResultOnDomainModuleLayerDoesNotHaveDependencyOnAboveLayers(
            assembly,
            moduleNamespace);
        
        // Assert
        using (new AssertionScope())
        {
            result.IsSuccessful.Should().BeTrue();
            result.FailingTypes.Should().BeNullOrEmpty();
        }
    }
    
    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOnAboveLayersOrOtherModules()
    {
        // Act
        Assembly assembly = Assembly.Load("Freethings");
        string moduleNamespace = "Freethings.Auctions";
        TestResult result = GetResultOnApplicationModuleLayerDoesNotHaveDependencyOnAboveLayers(
            assembly,
            moduleNamespace);
        
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
    
    private TestResult GetResultOnApplicationModuleLayerDoesNotHaveDependencyOnAboveLayers(Assembly assembly, string moduleNamespace)
    {
        return Types.InAssembly(assembly)
            .That()
            .ResideInNamespace($"{moduleNamespace}.Application")
            .Should()
            .NotHaveDependencyOn($"{moduleNamespace}.Infrastructure")
            .And().NotHaveDependencyOn($"{moduleNamespace}.Presentation")
            .GetResult();
    }
}

