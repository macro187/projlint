using System.Text.RegularExpressions;
using MacroSystem;
using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class Windows_Line_Endings : LineEndingAspect
    {

        public Windows_Line_Endings(RepositoryContext context)
            : base(
                context,
                LineEnding.CRLF,
                "Windows",
                new[]
                {
                    new Regex(@"\.sln$"),
                })
        {
        }

    }
}
