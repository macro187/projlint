using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class Obj_GitIgnore : GitIgnoreAspect
    {

        public Obj_GitIgnore(RepositoryContext context)
            : base(context, "/*/obj")
        {
        }

    }
}
