using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class Sln_Windows_EndOfLine_EditorConfig_Declaration
        : WindowsEndOfLineEditorConfigDeclarationAspect<Sln_EditorConfig_Section>
    {

        public Sln_Windows_EndOfLine_EditorConfig_Declaration(RepositoryContext context)
            : base(context, "*.sln")
        {
        }

    }
}
