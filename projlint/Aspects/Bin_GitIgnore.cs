using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class Bin_GitIgnore : GitIgnoreAspect
    {

        public Bin_GitIgnore(RepositoryContext context)
            : base(context, "/*/bin")
        {
        }

    }
}
