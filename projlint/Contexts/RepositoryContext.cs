using System.Collections.Generic;
using System.IO;
using System.Linq;
using MacroGit;
using MacroGuards;
using IOPath = System.IO.Path;

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
        /// List files in a directory whose names match a wildcard pattern
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
        /// List files in a directory hierarchy whose names match a wildcard pattern
        /// </summary>
        ///
        /// <remarks>
        /// Ignores .git directories and .gitignore'd files and directories.
        /// </remarks>
        ///
        public IEnumerable<string> EnumerateAllFiles(string path, string searchPattern)
        {
            foreach (var file in EnumerateFiles(path, searchPattern))
            {
                yield return file;
            }

            foreach (var directory in EnumerateDirectories(path, "*"))
            {
                foreach (var file in EnumerateAllFiles(directory, searchPattern))
                {
                    yield return file;
                }
            }
        }


        /// <summary>
        /// List directories in a directory whose names match a wildcard pattern
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
