using ArchUnitNET.Domain;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Identity.ArchitectureTests;

internal static class ArchitectureLayers
{
    public static readonly IObjectProvider<IType> DomainLayer = 
        Types()
            .That()
            .ResideInAssembly("Identity.Domain")
            .As("Domain Layer");

    public static readonly IObjectProvider<IType> ApplicationLayer = 
        Types()
            .That()
            .ResideInAssembly("Identity.Application")
            .As("Application Layer");

    public static readonly IObjectProvider<IType> InfrastructureLayer =
        Types()
            .That()
            .ResideInAssembly("Identity.Infrastructure")
            .As("Infrastructure layer");

    public static readonly IObjectProvider<IType> ApiLayer =
        Types()
            .That()
            .ResideInAssembly("Identity.Api")
            .As("API layer");
}