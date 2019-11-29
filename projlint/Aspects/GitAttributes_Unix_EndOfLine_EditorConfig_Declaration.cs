using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class GitAttributes_Unix_EndOfLine_EditorConfig_Declaration
        : UnixEndOfLineEditorConfigDeclarationAspect<GitAttributes_EditorConfig_Section>
    {

        public GitAttributes_Unix_EndOfLine_EditorConfig_Declaration(RepositoryContext context)
            : base(context, ".gitattributes")
        {
        }

    }
}
