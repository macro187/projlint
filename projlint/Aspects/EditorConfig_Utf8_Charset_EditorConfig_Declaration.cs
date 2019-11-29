using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class EditorConfig_Utf8_Charset_EditorConfig_Declaration
        : Utf8CharsetEditorConfigDeclarationAspect<EditorConfig_EditorConfig_Section>
    {

        public EditorConfig_Utf8_Charset_EditorConfig_Declaration(RepositoryContext context)
            : base(context, ".editorconfig")
        {
        }

    }
}
