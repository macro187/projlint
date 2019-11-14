using System.IO;
using System.Linq;
using MacroIO;
using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public abstract class GitIgnoreAspect : RepositoryAspect
    {

        public GitIgnoreAspect(RepositoryContext context, string gitIgnoreLine)
            : base(context)
        {
            Require<GitIgnore_File>();
            this.gitIgnoreLine = gitIgnoreLine;
            gitIgnorePath = GitIgnore_File.GetGitIgnorePath(Context);
        }


        readonly string gitIgnoreLine;
        readonly string gitIgnorePath;
        bool isGitIgnored;


        protected override bool OnAnalyse()
        {
            return isGitIgnored = File.ReadLines(gitIgnorePath).Any(line => line.Trim() == gitIgnoreLine);
        }


        protected override bool OnApply()
        {
            if (!isGitIgnored)
            {
                FileExtensions.AppendLines(gitIgnorePath, gitIgnoreLine);
            }

            return true;
        }

    }
}
