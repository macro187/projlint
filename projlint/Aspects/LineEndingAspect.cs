using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using MacroGuards;
using MacroIO;
using MacroSystem;
using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public abstract class LineEndingAspect : RepositoryAspect
    {

        protected LineEndingAspect(
            RepositoryContext context,
            LineEnding lineEnding,
            string lineEndingDescription,
            IEnumerable<string> applicableFilenameExtensions
        )
            : base(context)
        {
            Guard.NotNull(lineEnding, nameof(lineEnding));
            Guard.NotNull(applicableFilenameExtensions, nameof(applicableFilenameExtensions));
            SetPriority(-100);
            this.lineEnding = lineEnding;
            this.lineEndingDescription = lineEndingDescription;
            this.applicableFilenameExtensions = applicableFilenameExtensions;
        }


        readonly LineEnding lineEnding;
        readonly string lineEndingDescription;
        readonly IEnumerable<string> applicableFilenameExtensions;
        readonly List<string> filesWithIncorrectLineEndings = new List<string>();


        protected override bool OnAnalyse()
        {
            foreach (var file in Context.EnumerateAllFiles(Context.Path, "*"))
            {
                if (!IsApplicable(file))
                {
                    continue;
                }

                var endings = FileExtensions.DetectAllLineEndings(file);

                if (endings.Count == 0)
                {
                    continue;
                }

                if (endings.Count > 1)
                {
                    Trace.TraceWarning($"Multiple line endings in {file}");
                    filesWithIncorrectLineEndings.Add(file);
                    continue;
                }

                if (endings.Single() != lineEnding)
                {
                    Trace.TraceWarning($"Incorrect line endings in {file}");
                    filesWithIncorrectLineEndings.Add(file);
                    continue;
                }
            }

            return filesWithIncorrectLineEndings.Count == 0;
        }


        protected override bool OnApply()
        {
            foreach (var file in filesWithIncorrectLineEndings)
            {
                Trace.TraceInformation($"Applying {lineEndingDescription} line endings to {file}");
                var lines = File.ReadAllLines(file);
                FileExtensions.WriteAllLines(file, lines, lineEnding, false);
            }

            return true;
        }


        bool IsApplicable(string path)
        {
            return applicableFilenameExtensions.Any(ext => path.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
        }

    }
}
