using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class NuGit_Utf8_Charset_EditorConfig_Declaration
        : Utf8CharsetEditorConfigDeclarationAspect<NuGit_EditorConfig_Section>
    {

        public NuGit_Utf8_Charset_EditorConfig_Declaration(RepositoryContext context)
            : base(context, ".nugit")
        {
        }

    }
}
