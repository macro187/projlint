using System.Text.RegularExpressions;
using MacroSystem;
using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class Unix_Line_Endings : LineEndingAspect
    {

        public Unix_Line_Endings(RepositoryContext context)
            : base(
                context,
                LineEnding.LF,
                "Unix",
                new[]
                {
                    new Regex(@"\.csproj$"),
                    new Regex(@"\.cs$"),
                    new Regex(@"\.md$"),
                    new Regex(@"^\.gitignore$"),
                    new Regex(@"^\.gitattributes$"),
                    new Regex(@"^\.editorconfig$"),
                    new Regex(@"^\.nugit$"),
                    new Regex(@"^\.produce$"),
                })
        {
        }

    }
}
