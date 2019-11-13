﻿using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using MacroDiagnostics;
using MacroSln;
using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class SingleCSProjAspect : ProjectAspect
    {

        public SingleCSProjAspect(ProjectContext context)
            : base(context)
        {
            correctCsprojName = CSProjNameAspect.GetCorrectCSProjName(context);
            correctCsprojPath = Path.Combine(context.Path, correctCsprojName);
        }


        ICollection<string> csprojs;
        readonly string correctCsprojName;
        readonly string correctCsprojPath;


        protected override bool OnAnalyse()
        {
            csprojs =
                Directory.EnumerateFiles(Context.Path, "*.csproj", SearchOption.TopDirectoryOnly)
                    .Select(path => Path.GetFileName(path))
                    .ToList();

            if (csprojs.Count == 0)
            {
                Trace.TraceWarning("No .csproj file(s) found");
                return false;
            }

            if (csprojs.Count > 1)
            {
                Trace.TraceWarning("Multiple .csproj files found:");
                foreach (var csproj in csprojs)
                {
                    Trace.TraceWarning(csproj);
                }

                return false;
            }

            return true;
        }


        protected override bool OnApply()
        {
            if (csprojs.Count > 1)
            {
                var incorrectCsprojs =
                    csprojs
                        .Where(csproj => csproj != correctCsprojName)
                        .ToList();

                foreach (var csproj in incorrectCsprojs)
                {
                    using (LogicalOperation.Start($"Deleting {csproj}"))
                    {
                        var path = Path.Combine(Context.Path, csproj);
                        File.Delete(path);
                        csprojs.Remove(csproj);
                    }
                }
            }

            if (csprojs.Count == 0)
            {
                using (LogicalOperation.Start($"Creating {correctCsprojName}"))
                {
                    VisualStudioProject.Create(correctCsprojPath);
                }
            }

            return true;
        }

    }
}
