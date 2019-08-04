using System.Collections.Generic;
using System.IO;
using System.Linq;
using MacroGit;
using MacroGuards;

namespace
projlint.Contexts
{

public class
RepositoryContext
{


public
RepositoryContext(GitRepository repository)
{
    Guard.NotNull(repository, nameof(repository));
    Repository = repository;
}


/// <summary>
/// The Git repository
/// </summary>
///
public GitRepository
Repository
{
    get;
}


/// <summary>
/// Find subdirectories that look like projects
/// </summary>
///
public IEnumerable<ProjectContext>
FindProjects() =>
    Directory.EnumerateDirectories(Repository.Path, "*", SearchOption.TopDirectoryOnly)
        .Where(subdirectoryPath => LooksLikeProject(subdirectoryPath))
        .Select(subdirectoryPath => Path.GetFileName(subdirectoryPath))
        .Select(subdirectoryName => new ProjectContext(Repository, subdirectoryName));


bool
LooksLikeProject(string path)
{
    if (Directory.EnumerateFiles(path, "*.csproj", SearchOption.TopDirectoryOnly).Any())
    {
        return true;
    }

    if (Directory.EnumerateFiles(path, "*.cs", SearchOption.AllDirectories).Any())
    {
        return true;
    }

    return false;
}


}
}
