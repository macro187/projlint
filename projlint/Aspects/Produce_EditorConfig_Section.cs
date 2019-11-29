using ProjLint.Contexts;

namespace ProjLint.Aspects
{

    public class Produce_EditorConfig_Section : EditorConfigSectionAspect
    {

        public Produce_EditorConfig_Section(RepositoryContext context)
            : base(context, ".produce")
        {
        }

    }

}
