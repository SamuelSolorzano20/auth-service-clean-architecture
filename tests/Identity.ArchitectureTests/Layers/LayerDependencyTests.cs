using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;

using static ArchUnitNET.Fluent.ArchRuleDefinition;
using static Identity.ArchitectureTests.Layers.ArchitectureDefinition;
using static Identity.ArchitectureTests.Layers.ArchitectureLayers;

namespace Identity.ArchitectureTests.Layers;

public sealed class LayerDependencyTests
{
    [Fact]
    public void Domain_should_not_depend_on_Application()
    {
        IArchRule rule = Types()
            .That()
            .Are(DomainLayer)
            .Should()
            .NotDependOnAny(ApplicationLayer)
            .Because("The domain layer must contain framework-independent business rules")
            .WithoutRequiringPositiveResults();

        rule.Check(Architecture);
    }

    [Fact]
    public void Domain_should_not_depend_on_Infrastructure()
    {
        IArchRule rule = Types()
            .That()
            .Are(DomainLayer)
            .Should()
            .NotDependOnAny(InfrastructureLayer)
            .Because("The Domain layer must not know how persistence or external services are implemented")
            .WithoutRequiringPositiveResults();

        rule.Check(Architecture);
    }

    [Fact]
    public void Domain_should_not_depend_on_Api()
    {
        IArchRule rule = Types()
            .That()
            .Are(DomainLayer)
            .Should()
            .NotDependOnAny(ApiLayer)
            .Because("The Domain layer must not depend on HTTP or presentation concerns")
            .WithoutRequiringPositiveResults();

        rule.Check(Architecture);
    }

    [Fact]
    public void Application_should_not_depend_on_Infrastructure()
    {
        IArchRule rule = Types()
            .That()
            .Are(ApplicationLayer)
            .Should()
            .NotDependOnAny(InfrastructureLayer)
            .Because("Infrastructure must implement Application abstractions, not the reverse")
            .WithoutRequiringPositiveResults();

        rule.Check(Architecture);
    }

    [Fact]
    public void Application_should_not_depend_on_Api()
    {
        IArchRule rule = Types()
            .That()
            .Are(ApplicationLayer)
            .Should()
            .NotDependOnAny(ApiLayer)
            .Because("Application use cases must remain independent from HTTP transport concerns")
            .WithoutRequiringPositiveResults();

        rule.Check(Architecture);
    }

    [Fact]
    public void Infrastructure_should_not_depend_on_Api()
    {
        IArchRule rule = Types()
            .That()
            .Are(InfrastructureLayer)
            .Should()
            .NotDependOnAny(ApiLayer)
            .Because("Infrastructure must not depend on the application's composition root")
            .WithoutRequiringPositiveResults();

        rule.Check(Architecture);
    }
}