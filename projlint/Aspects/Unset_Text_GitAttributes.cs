using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class Unset_Text_GitAttributes : GitAttributesAspect
    {

        public Unset_Text_GitAttributes(RepositoryContext context)
            : base(context, "* -text")
        {
        }

    }
}
