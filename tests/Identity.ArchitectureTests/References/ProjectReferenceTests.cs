namespace Identity.ArchitectureTests.References;

public sealed class ProjectReferenceTests
{
    [Fact]
    public void Domain_should_have_no_project_references()
    {
        IReadOnlySet<string> references =
            ProjectReferenceReader.ReadReferencedProjectNames(
                "src/Identity.Domain/Identity.Domain.csproj");

        Assert.Empty(references);
    }

    [Fact]
    public void Application_should_reference_only_Domain()
    {
        IReadOnlySet<string> references =
            ProjectReferenceReader.ReadReferencedProjectNames(
                "src/Identity.Application/Identity.Application.csproj");

        AssertReferences(
            references,
            "Identity.Domain");
    }

    [Fact]
    public void Infrastructure_should_reference_Application_and_Domain()
    {
        IReadOnlySet<string> references =
            ProjectReferenceReader.ReadReferencedProjectNames(
                "src/Identity.Infrastructure/Identity.Infrastructure.csproj");

        AssertReferences(
            references,
            "Identity.Application",
            "Identity.Domain");
    }

    [Fact]
    public void Api_should_reference_Application_and_Infrastructure()
    {
        IReadOnlySet<string> references =
            ProjectReferenceReader.ReadReferencedProjectNames(
                "src/Identity.Api/Identity.Api.csproj");

        AssertReferences(
            references,
            "Identity.Application",
            "Identity.Infrastructure");
    }

    private static void AssertReferences(
        IReadOnlySet<string> actualReferences,
        params string[] expectedReferences)
    {
        string[] actual = actualReferences
            .Order(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        string[] expected = expectedReferences
            .Order(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        Assert.Equal(expected, actual);
    }
}