using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class Dot_VS_GitIgnore : GitIgnoreAspect
    {

        public Dot_VS_GitIgnore(RepositoryContext context)
            : base(context, "/.vs")
        {
        }

    }
}
