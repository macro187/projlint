using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class GitIgnore_Utf8_Charset_EditorConfig_Declaration
        : Utf8CharsetEditorConfigDeclarationAspect<GitIgnore_EditorConfig_Section>
    {

        public GitIgnore_Utf8_Charset_EditorConfig_Declaration(RepositoryContext context)
            : base(context, ".gitignore")
        {
        }

    }
}
