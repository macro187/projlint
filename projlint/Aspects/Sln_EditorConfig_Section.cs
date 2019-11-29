using ProjLint.Contexts;

namespace ProjLint.Aspects
{

    public class Sln_EditorConfig_Section : EditorConfigSectionAspect
    {

        public Sln_EditorConfig_Section(RepositoryContext context)
            : base(context, "*.sln")
        {
        }

    }

}
