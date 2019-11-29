using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class CS_Utf8_Charset_EditorConfig_Declaration
        : Utf8CharsetEditorConfigDeclarationAspect<CS_EditorConfig_Section>
    {

        public CS_Utf8_Charset_EditorConfig_Declaration(RepositoryContext context)
            : base(context, "*.cs")
        {
        }

    }
}
