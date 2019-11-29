using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class Global_120_Guidelines_EditorConfig_Declaration
        : EditorConfigDeclarationAspect<Global_EditorConfig_Section>
    {

        public Global_120_Guidelines_EditorConfig_Declaration(RepositoryContext context)
            : base(context, "*", "guidelines", "120")
        {
        }

    }
}
