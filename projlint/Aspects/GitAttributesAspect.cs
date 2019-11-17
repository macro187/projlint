using System.IO;
using System.Linq;
using MacroIO;
using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public abstract class GitAttributesAspect : RepositoryAspect
    {

        public GitAttributesAspect(RepositoryContext context, string gitAttributesLine)
            : base(context)
        {
            Require<GitAttributes_File>();
            this.gitAttributesLine = gitAttributesLine;
            gitAttributesPath = GitAttributes_File.GetGitAttributesPath(Context);
        }


        readonly string gitAttributesLine;
        readonly string gitAttributesPath;
        bool isLinePresent;


        protected override bool OnAnalyse()
        {
            return isLinePresent = File.ReadLines(gitAttributesPath).Any(line => line.Trim() == gitAttributesLine);
        }


        protected override bool OnApply()
        {
            if (!isLinePresent)
            {
                FileExtensions.AppendLines(gitAttributesPath, gitAttributesLine);
                OnApplied();
            }

            return true;
        }


        protected virtual void OnApplied() { }

    }
}
