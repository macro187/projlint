using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public abstract class Utf8CharsetEditorConfigDeclarationAspect<TSectionAspect>
        : EditorConfigDeclarationAspect<TSectionAspect>
        where TSectionAspect : EditorConfigSectionAspect
    {

        public Utf8CharsetEditorConfigDeclarationAspect(RepositoryContext context, string sectionName)
            : base(context, sectionName, "charset", "utf-8")
        {
        }

    }
}
