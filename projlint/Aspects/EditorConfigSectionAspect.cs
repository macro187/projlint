using System.IO;
using System.Linq;
using MacroEditorConfig;
using MacroCollections;
using MacroIO;
using ProjLint.Contexts;
using System.Collections.Generic;

namespace ProjLint.Aspects
{
    public abstract class EditorConfigSectionAspect : RepositoryAspect
    {

        public EditorConfigSectionAspect(RepositoryContext context, string name)
            : this(context, name, false)
        {
        }


        public EditorConfigSectionAspect(RepositoryContext context, string name, bool isSectionFirst)
            : base(context)
        {
            Require<EditorConfig_File>();
            editorConfigPath = EditorConfig_File.GetEditorConfigPath(Context);
            this.name = name;
            this.isSectionFirst = isSectionFirst;
        }


        readonly string editorConfigPath;
        readonly string name;
        readonly bool isSectionFirst;
        EditorConfigFile editorConfigFile;


        protected override bool OnAnalyse()
        {
            editorConfigFile = new EditorConfigFile(File.ReadLines(editorConfigPath));
            return editorConfigFile.Sections.Any(s => s.Header?.Name == name);
        }


        protected override bool OnApply()
        {
            var sectionToInsertAfter =
                isSectionFirst ?
                    editorConfigFile.Sections.First() :
                    editorConfigFile.Sections.Last();

            var insertIndex =
                sectionToInsertAfter.Lines
                    .Select(l => l.Index + 1)
                    .LastOrDefault();

            var isBlankLineRequired =
                insertIndex > 0 &&
                !(editorConfigFile.EditorConfigLines[insertIndex - 1] is EditorConfigBlankLine);

            var linesToInsert = new List<string>();

            if (isBlankLineRequired)
            {
                linesToInsert.Add("");
            }

            linesToInsert.Add(
                $"[{name}]",
                $"");

            editorConfigFile.Edit(lines => {
                lines.InsertRange(insertIndex, linesToInsert);
            });

            FileExtensions.RewriteAllLines(editorConfigPath, editorConfigFile.Lines);

            return true;
        }

    }
}
