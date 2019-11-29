using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class EditorConfig_Unix_EndOfLine_EditorConfig_Declaration
        : UnixEndOfLineEditorConfigDeclarationAspect<EditorConfig_EditorConfig_Section>
    {

        public EditorConfig_Unix_EndOfLine_EditorConfig_Declaration(RepositoryContext context)
            : base(context, ".editorconfig")
        {
        }

    }
}
