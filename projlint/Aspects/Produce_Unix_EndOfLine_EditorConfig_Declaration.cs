using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class Produce_Unix_EndOfLine_EditorConfig_Declaration
        : UnixEndOfLineEditorConfigDeclarationAspect<Produce_EditorConfig_Section>
    {

        public Produce_Unix_EndOfLine_EditorConfig_Declaration(RepositoryContext context)
            : base(context, ".produce")
        {
        }

    }
}
