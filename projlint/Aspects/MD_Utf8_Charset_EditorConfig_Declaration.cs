using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class MD_Utf8_Charset_EditorConfig_Declaration
        : Utf8CharsetEditorConfigDeclarationAspect<MD_EditorConfig_Section>
    {

        public MD_Utf8_Charset_EditorConfig_Declaration(RepositoryContext context)
            : base(context, "*.md")
        {
        }

    }
}
