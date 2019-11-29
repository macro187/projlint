using ProjLint.Contexts;

namespace ProjLint.Aspects
{

    public class MD_EditorConfig_Section : EditorConfigSectionAspect
    {

        public MD_EditorConfig_Section(RepositoryContext context)
            : base(context, "*.md")
        {
        }

    }

}
