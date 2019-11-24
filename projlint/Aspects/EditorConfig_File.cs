using System.IO;
using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class EditorConfig_File : RepositoryAspect
    {

        public static string GetEditorConfigPath(RepositoryContext context) =>
            Path.Combine(context.Path, ".editorconfig");


        public EditorConfig_File(RepositoryContext context)
            : base(context)
        {
            editorConfigFile = GetEditorConfigPath(Context);
        }


        readonly string editorConfigFile;
        bool isEditorConfigPresent;


        protected override bool OnAnalyse()
        {
            return isEditorConfigPresent = File.Exists(editorConfigFile);
        }


        protected override bool OnApply()
        {
            if (!isEditorConfigPresent)
            {
                using (File.Create(editorConfigFile)) { }
            }

            return true;
        }

    }
}
