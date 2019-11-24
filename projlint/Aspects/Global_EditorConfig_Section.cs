using System.IO;
using System.Linq;
using MacroEditorConfig;
using MacroCollections;
using MacroIO;
using ProjLint.Contexts;
using System.Collections.Generic;

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

            int lineToInsertAt;
            bool insertBlankLineBefore;
            if (firstSectionHeader != null)
            {
                lineToInsertAt = firstSectionHeader.LineNumber;
                insertBlankLineBefore = false;
            }
            else
            {
                lineToInsertAt = editorConfigFile.Lines.Count + 1;
                insertBlankLineBefore = true;
            }

            var linesToInsert =
                new List<string>()
                {
                    "[*]",
                    "",
                };

            if (insertBlankLineBefore)
            {
                linesToInsert.Insert(0, "");
            }

            editorConfigFile.Edit(lines => {
                lines.InsertRange(lineToInsertAt - 1, linesToInsert);
            });

            FileExtensions.RewriteAllLines(editorConfigPath, editorConfigFile.Lines);

            return true;
        }

    }
}
