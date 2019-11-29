using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class GitAttributes_Utf8_Charset_EditorConfig_Declaration
        : Utf8CharsetEditorConfigDeclarationAspect<GitAttributes_EditorConfig_Section>
    {

        public GitAttributes_Utf8_Charset_EditorConfig_Declaration(RepositoryContext context)
            : base(context, ".gitattributes")
        {
        }

    }
}
