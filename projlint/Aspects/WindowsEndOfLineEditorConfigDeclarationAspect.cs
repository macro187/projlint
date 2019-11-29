using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public abstract class WindowsEndOfLineEditorConfigDeclarationAspect<TSectionAspect>
        : EditorConfigDeclarationAspect<TSectionAspect>
        where TSectionAspect : EditorConfigSectionAspect
    {

        public WindowsEndOfLineEditorConfigDeclarationAspect(RepositoryContext context, string sectionName)
            : base(context, sectionName, "end_of_line", "crlf")
        {
        }

    }
}
