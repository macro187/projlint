using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class CS_Unix_EndOfLine_EditorConfig_Declaration
        : UnixEndOfLineEditorConfigDeclarationAspect<CS_EditorConfig_Section>
    {

        public CS_Unix_EndOfLine_EditorConfig_Declaration(RepositoryContext context)
            : base(context, "*.cs")
        {
        }

    }
}
