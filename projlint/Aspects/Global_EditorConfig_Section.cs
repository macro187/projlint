using ProjLint.Contexts;

namespace ProjLint.Aspects
{

    public class Global_EditorConfig_Section : EditorConfigSectionAspect
    {

        public Global_EditorConfig_Section(RepositoryContext context)
            : base(context, "*", true)
        {
        }

    }

}
