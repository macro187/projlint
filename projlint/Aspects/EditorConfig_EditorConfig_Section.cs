using ProjLint.Contexts;

namespace ProjLint.Aspects
{

    public class EditorConfig_EditorConfig_Section : EditorConfigSectionAspect
    {

        public EditorConfig_EditorConfig_Section(RepositoryContext context)
            : base(context, ".editorconfig")
        {
        }

    }

}
