using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MacroDiagnostics;
using MacroIO;
using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class No_Utf8_Boms : RepositoryAspect
    {

        static readonly Regex[] FilesThatShouldHaveNoBom =
            new []
            {
                new Regex(@"\.sln$"),
                new Regex(@"\.csproj$"),
                new Regex(@"\.cs$"),
                new Regex(@"\.md$"),
                new Regex(@"^\.gitignore$"),
                new Regex(@"^\.gitattributes$"),
                new Regex(@"^\.editorconfig$"),
                new Regex(@"^\.nugit$"),
                new Regex(@"^\.produce$"),
            };


        public No_Utf8_Boms(RepositoryContext context)
            : base(context)
        {
            SetPriority(-101);
        }


        readonly List<string> filesWithBoms = new List<string>();


        protected override bool OnAnalyse()
        {
            foreach (var file in Context.EnumerateAllFiles(Context.Path, "*"))
            {
                if (!ShouldHaveNoBom(file))
                {
                    continue;
                }

                if (FileExtensions.DetectUtf8Bom(file) != true)
                {
                    continue;
                }

                Trace.TraceError($"UTF-8 BOM(s) in {file}");
                filesWithBoms.Add(file);
            }

            return filesWithBoms.Count == 0;
        }


        protected override bool OnApply()
        {
            foreach (var file in filesWithBoms)
            {
                RemoveBom(file);
            }

            return true;
        }


        bool ShouldHaveNoBom(string path)
        {
            return FilesThatShouldHaveNoBom.Any(regex => regex.IsMatch(Path.GetFileName(path)));
        }


        void RemoveBom(string path)
        {
            using (LogicalOperation.Start($"Removing UTF-8 BOM(s) from {path}"))
            {
                while (FileExtensions.DetectUtf8Bom(path) == true)
                {
                    FileExtensions.RemoveFirst(path, FileExtensions.Utf8Bom.Count);
                }
            }
        }

    }
}
