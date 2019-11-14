using System.IO;
using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class A_GitIgnore_File : RepositoryAspect
    {

        public A_GitIgnore_File(RepositoryContext context)
            : base(context)
        {
            gitIgnorePath = Path.Combine(Context.Path, ".gitignore");
        }


        string gitIgnorePath;
        bool isGitIgnorePresent;


        protected override bool OnAnalyse()
        {
            return isGitIgnorePresent = File.Exists(gitIgnorePath);
        }


        protected override bool OnApply()
        {
            if (!isGitIgnorePresent)
            {
                using (File.Create(gitIgnorePath)) {}
            }

            return true;
        }

    }
}
