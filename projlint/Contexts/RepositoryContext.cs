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
            EnumerateDirectories(Path, "*")
                .Where(p => LooksLikeProject(p))
                .Select(p => IOPath.GetFileName(p))
                .Select(p => new ProjectContext(this, p));


        /// <summary>
        /// Returns an enumerable collection of file names that match a search pattern in a specified path
        /// </summary>
        ///
        /// <remarks>
        /// Ignores .git directories and .gitignore'd files and directories.
        /// </remarks>
        ///
        public IEnumerable<string> EnumerateFiles(string path, string searchPattern) =>
            Directory.EnumerateFiles(path, searchPattern)
                .Where(p => !Repository.IsIgnored(p));


        /// <summary>
        /// Returns an enumerable collection of directory names that match a search pattern in a specified path
        /// </summary>
        ///
        /// <remarks>
        /// Ignores .git and .gitignore'd directories.
        /// </remarks>
        ///
        public IEnumerable<string> EnumerateDirectories(string path, string searchPattern) =>
            Directory.EnumerateDirectories(path, searchPattern)
                .Where(p => !IsDotGit(p))
                .Where(p => !Repository.IsIgnored(p));


        bool LooksLikeProject(string path) =>
            EnumerateFiles(path, "*.csproj").Any();


        static bool IsDotGit(string path) =>
            IOPath.GetFileName(path).ToLowerInvariant() == ".git";

    }
}
