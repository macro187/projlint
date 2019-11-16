using System.IO;
using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class GitAttributes_File : RepositoryAspect
    {

        public static string GetGitAttributesPath(RepositoryContext context) =>
            Path.Combine(context.Path, ".gitattributes");


        public GitAttributes_File(RepositoryContext context)
            : base(context)
        {
            gitAttributesPath = GetGitAttributesPath(Context);
        }


        readonly string gitAttributesPath;
        bool isGitAttributesPresent;


        protected override bool OnAnalyse()
        {
            return isGitAttributesPresent = File.Exists(gitAttributesPath);
        }


        protected override bool OnApply()
        {
            if (!isGitAttributesPresent)
            {
                using (File.Create(gitAttributesPath)) { }
            }

            return true;
        }

    }
}
