using System.IO;
using System.Linq;
using MacroEditorConfig;
using MacroIO;
using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class Global_120_Column_Guideline_EditorConfig_Declaration : RepositoryAspect
    {

        public Global_120_Column_Guideline_EditorConfig_Declaration(RepositoryContext context)
            : base(context)
        {
            Require<Global_EditorConfig_Section>();
            editorConfigPath = EditorConfig_File.GetEditorConfigPath(Context);
        }


        readonly string editorConfigPath;
        EditorConfigFile editorConfigFile;
        EditorConfigSection globalSection;


        protected override bool OnAnalyse()
        {
            editorConfigFile = new EditorConfigFile(File.ReadLines(editorConfigPath));
            globalSection = editorConfigFile.Sections .Single(s => s.Header?.Name == "*");
            return globalSection.Declarations.Any(d => d.Key == "guidelines" && d.Value == "120");
        }


        protected override bool OnApply()
        {
            var lastLine = globalSection.Lines.LastOrDefault(line => !(line is EditorConfigBlankLine));

            var lineToInsertAfter =
                lastLine?.LineNumber ??
                globalSection.Header.LineNumber;

            editorConfigFile.Edit(lines => {
                lines.Insert(lineToInsertAfter, "guidelines = 120");
            });

            FileExtensions.RewriteAllLines(editorConfigPath, editorConfigFile.Lines);

            return true;
        }

    }
}
