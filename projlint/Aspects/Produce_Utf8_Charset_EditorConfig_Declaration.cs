using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class Produce_Utf8_Charset_EditorConfig_Declaration
        : Utf8CharsetEditorConfigDeclarationAspect<Produce_EditorConfig_Section>
    {

        public Produce_Utf8_Charset_EditorConfig_Declaration(RepositoryContext context)
            : base(context, ".produce")
        {
        }

    }
}
