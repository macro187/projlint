using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MacroIO;
using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class No_Utf8_Boms : RepositoryAspect
    {

        static readonly string[] FilesThatShouldHaveNoBom =
            new []
            {
                ".sln",
                ".csproj",
                ".cs",
            };


        public No_Utf8_Boms(RepositoryContext context)
            : base(context)
        {
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

                Trace.TraceWarning(file);
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
            return
                FilesThatShouldHaveNoBom
                .Any(ext => path.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
        }


        void RemoveBom(string path)
        {
            Trace.TraceInformation($"Removing UTF-8 BOM from {path}");
            while (FileExtensions.DetectUtf8Bom(path) == true)
            {
                FileExtensions.RemoveFirst(path, FileExtensions.Utf8Bom.Count);
            }
        }

    }
}
