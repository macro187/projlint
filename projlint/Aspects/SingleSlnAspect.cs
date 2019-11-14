using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using MacroDiagnostics;
using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class SingleSlnAspect : RepositoryAspect
    {

        public SingleSlnAspect(RepositoryContext context)
            : base(context)
        {
            correctSlnName = SlnNameAspect.GetCorrectSlnName(context);
        }


        readonly string correctSlnName;
        ICollection<string> slnNames;


        protected override bool OnAnalyse()
        {
            slnNames =
                Context.EnumerateFiles(Context.Path, "*.sln")
                    .Select(path => Path.GetFileName(path))
                    .ToList();

            if (slnNames.Count == 0)
            {
                Trace.TraceWarning("No .sln file(s) found");
                return false;
            }

            if (slnNames.Count > 1)
            {
                Trace.TraceWarning("Multiple .sln files found:");
                foreach (var sln in slnNames)
                {
                    Trace.TraceWarning(sln);
                }

                return false;
            }

            return true;
        }


        protected override bool OnApply()
        {
            if (slnNames.Count > 1)
            {
                var incorrectSlns =
                    slnNames
                        .Where(sln => sln != correctSlnName)
                        .ToList();

                foreach (var sln in incorrectSlns)
                {
                    using (LogicalOperation.Start($"Deleting {sln}"))
                    {
                        var path = Path.Combine(Context.Path, sln);
                        File.Delete(path);
                        slnNames.Remove(sln);
                    }
                }
            }

            if (slnNames.Count == 0)
            {
                using (LogicalOperation.Start($"Creating {correctSlnName}"))
                {
                    var exitCode =
                        ProcessExtensions.ExecuteAny(
                            true, true,
                            Context.Path,
                            "dotnet", "new", "sln", "--name", correctSlnName);

                    if (exitCode != 0)
                    {
                        Trace.TraceError("dotnet new sln failed");
                        return false;
                    }
                }
            }

            return true;
        }

    }
}
