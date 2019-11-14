using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class Dot_VSCode_GitIgnore : GitIgnoreAspect
    {

        public Dot_VSCode_GitIgnore(RepositoryContext context)
            : base(context, "/.vscode")
        {
        }

    }
}
