using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class CSProj_Utf8_Charset_EditorConfig_Declaration
        : Utf8CharsetEditorConfigDeclarationAspect<CSProj_EditorConfig_Section>
    {

        public CSProj_Utf8_Charset_EditorConfig_Declaration(RepositoryContext context)
            : base(context, "*.csproj")
        {
        }

    }
}
