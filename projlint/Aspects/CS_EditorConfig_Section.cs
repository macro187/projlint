using ProjLint.Contexts;

namespace ProjLint.Aspects
{

    public class CS_EditorConfig_Section : EditorConfigSectionAspect
    {

        public CS_EditorConfig_Section(RepositoryContext context)
            : base(context, "*.cs")
        {
        }

    }

}
