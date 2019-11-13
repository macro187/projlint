using System;
using System.IO;
using MacroGit;
using MacroGuards;
using IOPath = System.IO.Path;

namespace ProjLint.Contexts
{
    public class ProjectContext
    {

        public ProjectContext(GitRepository repository, string name)
        {
            Guard.NotNull(repository, nameof(repository));
            Guard.NotNull(name, nameof(name));
            Guard.NotWhiteSpaceOnly(name, nameof(name));

            Repository = repository;
            Name = name;
            Path = IOPath.Combine(Repository.Path, name);

            if (!Directory.Exists(Path))
            {
                throw new ArgumentException($"Subdirectory {name} does not exist", nameof(name));
            }
        }


        /// <summary>
        /// The Git repository containing this project
        /// </summary>
        ///
        public GitRepository Repository { get; }


        /// <summary>
        /// The name of the project directory
        /// </summary>
        ///
        public string Name { get; }


        /// <summary>
        /// The absolute path to the project directory
        /// </summary>
        ///
        public string Path { get; }

    }
}
