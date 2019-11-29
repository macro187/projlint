using ProjLint.Contexts;

namespace ProjLint.Aspects
{

    public class GitAttributes_EditorConfig_Section : EditorConfigSectionAspect
    {

        public GitAttributes_EditorConfig_Section(RepositoryContext context)
            : base(context, ".gitattributes")
        {
        }

    }

}
