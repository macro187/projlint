using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class GitIgnore_Unix_EndOfLine_EditorConfig_Declaration
        : UnixEndOfLineEditorConfigDeclarationAspect<GitIgnore_EditorConfig_Section>
    {

        public GitIgnore_Unix_EndOfLine_EditorConfig_Declaration(RepositoryContext context)
            : base(context, ".gitignore")
        {
        }

    }
}
