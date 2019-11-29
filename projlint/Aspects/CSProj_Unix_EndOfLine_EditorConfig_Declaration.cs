using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class CSProj_Unix_EndOfLine_EditorConfig_Declaration
        : UnixEndOfLineEditorConfigDeclarationAspect<CSProj_EditorConfig_Section>
    {

        public CSProj_Unix_EndOfLine_EditorConfig_Declaration(RepositoryContext context)
            : base(context, "*.csproj")
        {
        }

    }
}
