using ProjLint.Contexts;

namespace ProjLint.Aspects
{

    public class CSProj_EditorConfig_Section : EditorConfigSectionAspect
    {

        public CSProj_EditorConfig_Section(RepositoryContext context)
            : base(context, "*.csproj")
        {
        }

    }

}
