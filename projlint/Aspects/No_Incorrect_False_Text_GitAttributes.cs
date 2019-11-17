using System.IO;
using System.Linq;
using MacroIO;
using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class No_Incorrect_False_Text_GitAttributes : RepositoryAspect
    {

        /// <summary>
        /// What people sometimes incorrectly put in .gitattributes instead of "* -text"
        /// </summary>
        ///
        const string IncorrectLine = "* text=false";


        public No_Incorrect_False_Text_GitAttributes(RepositoryContext context)
            : base(context)
        {
            Require<GitAttributes_File>();
            gitAttributesPath = GitAttributes_File.GetGitAttributesPath(Context);
        }


        readonly string gitAttributesPath;
        bool isLinePresent;


        protected override bool OnAnalyse()
        {
            isLinePresent = File.ReadLines(gitAttributesPath).Any(line => line.Trim() == IncorrectLine);
            return !isLinePresent;
        }


        protected override bool OnApply()
        {
            if (isLinePresent)
            {
                var correctLines =
                    File.ReadLines(gitAttributesPath)
                        .Where(line => line != IncorrectLine)
                        .ToList();

                FileExtensions.RewriteAllLines(gitAttributesPath, correctLines);
            }

            return true;
        }

    }
}
