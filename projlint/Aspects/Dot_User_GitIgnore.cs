using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class Dot_User_GitIgnore : GitIgnoreAspect
    {

        public Dot_User_GitIgnore(RepositoryContext context)
            : base(context, "*.user")
        {
        }

    }
}
