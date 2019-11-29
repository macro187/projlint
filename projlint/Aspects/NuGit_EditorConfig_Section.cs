using ProjLint.Contexts;

namespace ProjLint.Aspects
{

    public class NuGit_EditorConfig_Section : EditorConfigSectionAspect
    {

        public NuGit_EditorConfig_Section(RepositoryContext context)
            : base(context, ".nugit")
        {
        }

    }

}
