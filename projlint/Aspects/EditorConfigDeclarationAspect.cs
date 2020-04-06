using System.Collections.Generic;
using System.IO;
using System.Linq;
using MacroCollections;
using MacroEditorConfig;
using MacroIO;
using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public abstract class EditorConfigDeclarationAspect<TSectionAspect> : RepositoryAspect
        where TSectionAspect : EditorConfigSectionAspect
    {

        public EditorConfigDeclarationAspect(RepositoryContext context, string sectionName, string key, string value)
            : base(context)
        {
            Require<TSectionAspect>();
            this.sectionName = sectionName;
            this.key = key;
            this.value = value;
            editorConfigPath = EditorConfig_File.GetEditorConfigPath(Context);
        }


        readonly string sectionName;
        readonly string key;
        readonly string value;
        readonly string editorConfigPath;
        EditorConfigFile editorConfigFile;
        EditorConfigSection section;


        protected override bool OnAnalyse()
        {
            editorConfigFile = new EditorConfigFile(File.ReadLines(editorConfigPath));
            section = editorConfigFile.Sections.Single(s => s.Header?.Name == sectionName);
            return section.Declarations.Any(d => d.Key == key && d.Value == value);
        }


        protected override bool OnApply()
        {
            var insertIndex =
                section.Lines
                    .Where(line => !(line is EditorConfigBlankLine))
                    .Select(line => line.Index + 1)
                    .LastOrDefault();

            var isBlankLineRequired =
                insertIndex < editorConfigFile.Lines.Count - 1 &&
                !(editorConfigFile.Lines[insertIndex] is EditorConfigBlankLine);

            var linesToInsert = new List<string>();

            linesToInsert.Add($"{key} = {value}");

            if (isBlankLineRequired)
            {
                linesToInsert.Insert(0, "");
            }

            editorConfigFile.Edit(lines => {
                lines.InsertRange(insertIndex, linesToInsert);
            });

            FileExtensions.RewriteAllLines(editorConfigPath, editorConfigFile.LinesOfText);

            return true;
        }

    }
}
