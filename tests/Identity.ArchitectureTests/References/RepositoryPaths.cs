namespace Identity.ArchitectureTests.References;

internal static class RepositoryPaths
{
    private const string SolutionFileName = "Identity.slnx";

    public static string Root { get; } = FindRepositoryRoot();

    public static string GetPath(string relativePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(relativePath);

        string normalizedPath = relativePath.Replace('/', Path.DirectorySeparatorChar);

        return Path.Combine(Root, normalizedPath);
    }

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (directory is not null)
        {
            string solutionPath = Path.Combine(directory.FullName, SolutionFileName);

            if (File.Exists(solutionPath))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        throw new DirectoryNotFoundException($"Could not locate the repository root containing {SolutionFileName}.");
    }
}