using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public abstract class UnixEndOfLineEditorConfigDeclarationAspect<TSectionAspect>
        : EditorConfigDeclarationAspect<TSectionAspect>
        where TSectionAspect : EditorConfigSectionAspect
    {

        public UnixEndOfLineEditorConfigDeclarationAspect(RepositoryContext context, string sectionName)
            : base(context, sectionName, "end_of_line", "lf")
        {
        }

    }
}
