using System.Collections.Generic;
using System.IO;
using IOPath = System.IO.Path;
using System.Linq;
using MacroGit;
using MacroGuards;

namespace ProjLint.Contexts
{
    public class RepositoryContext
    {

        public RepositoryContext(GitRepository repository)
        {
            Guard.NotNull(repository, nameof(repository));
            Repository = repository;
            Name = repository.Name;
            Path = repository.Path;
        }


        /// <summary>
        /// The Git repository
        /// </summary>
        ///
        public GitRepository Repository { get; }


        /// <summary>
        /// The name of the repository directory
        /// </summary>
        ///
        public string Name { get; }


        /// <summary>
        /// The absolute path to the repository directory
        /// </summary>
        ///
        public string Path { get; }


        /// <summary>
        /// Find subdirectories that look like projects
        /// </summary>
        ///
        public IEnumerable<ProjectContext> FindProjects() =>
            Directory.EnumerateDirectories(Path, "*", SearchOption.TopDirectoryOnly)
                .Where(path => LooksLikeProject(path))
                .Select(path => IOPath.GetFileName(path))
                .Select(name => new ProjectContext(Repository, name));


        bool LooksLikeProject(string path)
        {
            return Directory.EnumerateFiles(path, "*.csproj", SearchOption.TopDirectoryOnly).Any();
        }

    }
}
