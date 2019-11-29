using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class Sln_Utf8_Charset_EditorConfig_Declaration
        : Utf8CharsetEditorConfigDeclarationAspect<Sln_EditorConfig_Section>
    {

        public Sln_Utf8_Charset_EditorConfig_Declaration(RepositoryContext context)
            : base(context, "*.sln")
        {
        }

    }
}
