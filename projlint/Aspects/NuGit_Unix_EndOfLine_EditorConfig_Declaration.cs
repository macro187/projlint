using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class NuGit_Unix_EndOfLine_EditorConfig_Declaration
        : UnixEndOfLineEditorConfigDeclarationAspect<NuGit_EditorConfig_Section>
    {

        public NuGit_Unix_EndOfLine_EditorConfig_Declaration(RepositoryContext context)
            : base(context, ".nugit")
        {
        }

    }
}
