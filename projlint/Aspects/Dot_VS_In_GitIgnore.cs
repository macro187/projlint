using System.IO;
using System.Linq;
using MacroIO;
using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class Dot_VS_In_GitIgnore : RepositoryAspect
    {

        const string GitIgnoreDotVs = "/.vs";


        public Dot_VS_In_GitIgnore(RepositoryContext context)
            : base(context)
        {
            Require<GitIgnore_File>();
            gitIgnorePath = GitIgnore_File.GetGitIgnorePath(Context);
        }


        readonly string gitIgnorePath;
        bool isDotVsInGitIgnore;


        protected override bool OnAnalyse()
        {
            return
                isDotVsInGitIgnore =
                    File.ReadLines(gitIgnorePath)
                        .Any(line => line.Trim() == GitIgnoreDotVs);
        }


        protected override bool OnApply()
        {
            if (!isDotVsInGitIgnore)
            {
                FileExtensions.AppendLines(gitIgnorePath, GitIgnoreDotVs);
            }

            return true;
        }

    }
}
