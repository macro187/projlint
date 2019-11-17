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
                    ".csproj",
                    ".cs",
                })
        {
        }

    }
}
