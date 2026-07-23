using System.Xml.Linq;

namespace Identity.ArchitectureTests.References;

internal static class ProjectReferenceReader
{
    public static IReadOnlySet<string> ReadReferencedProjectNames(string projectRelativePath)
    {
        string projectPath = RepositoryPaths.GetPath(projectRelativePath);

        if (!File.Exists(projectPath))
        {
            throw new FileNotFoundException("The project file could not be found.", projectPath);
        }

        string projectDirectory =
            Path.GetDirectoryName(projectPath)
            ?? throw new InvalidOperationException($"Could not determine the directory for {projectPath}.");

        XDocument projectDocument = XDocument.Load(projectPath);

        return projectDocument
            .Descendants("ProjectReference")
            .Select(element => element.Attribute("Include")?.Value)
            .Where(include => !string.IsNullOrWhiteSpace(include))
            .Select(include => Path.GetFullPath(
                Path.Combine(projectDirectory, include!)))
            .Select(fullPath => Path.GetFileNameWithoutExtension(fullPath)!)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);
    }
}