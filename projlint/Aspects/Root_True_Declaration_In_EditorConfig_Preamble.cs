using System.IO;
using System.Linq;
using MacroEditorConfig;
using MacroIO;
using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class Root_True_Declaration_In_EditorConfig_Preamble : RepositoryAspect
    {

        public Root_True_Declaration_In_EditorConfig_Preamble(RepositoryContext context)
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
            return editorConfigFile.Preamble.Declarations.Any(d => d.Key == "root" && d.Value == "true");
        }


        protected override bool OnApply()
        {
            var firstNonBlankNonCommentLine =
                editorConfigFile.Preamble.Lines
                    .Where(line => !(line is EditorConfigBlankLine))
                    .Where(line => !(line is EditorConfigCommentLine))
                    .FirstOrDefault();

            var lastNonBlankLine =
                editorConfigFile.Preamble.Lines
                    .Where(line => !(line is EditorConfigBlankLine))
                    .LastOrDefault();

            var lineToInsertAt =
                firstNonBlankNonCommentLine != null
                    ? firstNonBlankNonCommentLine.LineNumber
                : lastNonBlankLine != null
                    ? lastNonBlankLine.LineNumber + 1
                : 1;

            editorConfigFile.Edit(lines => {
                lines.Insert(lineToInsertAt - 1, "root = true");
            });

            FileExtensions.RewriteAllLines(editorConfigPath, editorConfigFile.Lines);

            return true;
        }

    }
}
