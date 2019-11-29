using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class MD_Unix_EndOfLine_EditorConfig_Declaration
        : UnixEndOfLineEditorConfigDeclarationAspect<MD_EditorConfig_Section>
    {

        public MD_Unix_EndOfLine_EditorConfig_Declaration(RepositoryContext context)
            : base(context, "*.md")
        {
        }

    }
}
