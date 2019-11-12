using System;
using System.IO;
using MacroGit;
using MacroGuards;
using IOPath = System.IO.Path;

namespace ProjLint.Contexts
{
    public class ProjectContext
    {

        public ProjectContext(GitRepository repository, string subdirectory)
        {
            Guard.NotNull(repository, nameof(repository));
            Guard.NotNull(subdirectory, nameof(subdirectory));
            Guard.NotWhiteSpaceOnly(subdirectory, nameof(subdirectory));

            Repository = repository;
            Subdirectory = subdirectory;
            Path = IOPath.Combine(Repository.Path, subdirectory);

            if (!Directory.Exists(Path))
            {
                throw new ArgumentException($"Subdirectory {subdirectory} does not exist", nameof(subdirectory));
            }
        }


        /// <summary>
        /// The Git repository
        /// </summary>
        ///
        public GitRepository Repository { get; }


        /// <summary>
        /// The name of the project subdirectory
        /// </summary>
        ///
        public string Subdirectory { get; }


        /// <summary>
        /// The absolute normalized path to the project subdirectory
        /// </summary>
        ///
        public string Path { get; }

    }
}
