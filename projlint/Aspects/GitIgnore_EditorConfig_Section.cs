using ProjLint.Contexts;

namespace ProjLint.Aspects
{

    public class GitIgnore_EditorConfig_Section : EditorConfigSectionAspect
    {

        public GitIgnore_EditorConfig_Section(RepositoryContext context)
            : base(context, ".gitignore")
        {
        }

    }

}
