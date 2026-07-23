using ArchUnitNET.Domain;
using ArchUnitNET.Loader;

namespace Identity.ArchitectureTests.Layers;

internal static class ArchitectureDefinition 
{
    public static readonly Architecture Architecture = new ArchLoader()
        .LoadAssemblies(
            typeof(Domain.AssemblyReference).Assembly,
            typeof(Application.AssemblyReference).Assembly,
            typeof(Infrastructure.AssemblyReference).Assembly,
            typeof(Api.AssemblyReference).Assembly
        ).Build();
}