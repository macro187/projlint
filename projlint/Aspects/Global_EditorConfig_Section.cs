using System.IO;
using System.Linq;
using MacroEditorConfig;
using MacroCollections;
using MacroIO;
using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class Global_EditorConfig_Section : RepositoryAspect
    {

        public Global_EditorConfig_Section(RepositoryContext context)
            : base(context)
        {
            Require<EditorConfig_File>();
            editorConfigPath = EditorConfig_File.GetEditorConfigPath(Context);
        }


        readonly string editorConfigPath;
        EditorConfigFile editorConfigFile;


        protected override bool OnAnalyse()
        {
            editorConfigFile = new EditorConfigFile(File.ReadLines(editorConfigPath));
            return editorConfigFile.Sections.Any(s => s.Header?.Name == "*");
        }


        protected override bool OnApply()
        {
            var firstSectionHeader =
                editorConfigFile.Sections
                    .Where(s => !s.IsPreamble)
                    .Select(s => s.Header)
                    .FirstOrDefault();

            var lineToInsertAt =
                firstSectionHeader?.LineNumber ??
                editorConfigFile.Lines.Count + 1;

            editorConfigFile.Edit(lines => {
                lines.Insert(
                    lineToInsertAt - 1,
                    "[*]",
                    "");
            });

            FileExtensions.RewriteAllLines(editorConfigPath, editorConfigFile.Lines);

            return true;
        }

    }
}
