using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using MacroDiagnostics;
using MacroExceptions;
using MacroSln;
using projlint.Contexts;

namespace
projlint.Aspects
{

public class
CSProjFileAspect : ProjectAspect
{


public
CSProjFileAspect(ProjectContext context)
    : base(context)
{
    correctCsproj = $"{context.Subdirectory}.csproj";
    correctCsprojPath = Path.Combine(context.Path, correctCsproj);
}


readonly string correctCsproj;
readonly string correctCsprojPath;
ICollection<string> existingCsprojs;


protected override bool
OnAnalyse()
{
    existingCsprojs =
        Directory.EnumerateFiles(Context.Path, "*.csproj", SearchOption.TopDirectoryOnly)
            .Select(path => Path.GetFileName(path))
            .ToList();

    if (existingCsprojs.Count == 0)
    {
        Trace.TraceWarning("No .csproj file found");
        return false;
    }

    if (existingCsprojs.Count > 1)
    {
        Trace.TraceWarning("Multiple .csproj files found:");
        foreach (var csproj in existingCsprojs)
        {
            Trace.TraceWarning(csproj);
        }

        return false;
    }

    var existingCsproj = existingCsprojs.Single();
    if (existingCsproj != correctCsproj)
    {
        Trace.TraceWarning($"Existing {existingCsproj} should be named {correctCsproj}");
        return false;
    }

    return true;
}


protected override void
OnApply()
{
    if (existingCsprojs.Count > 1)
    {
        var incorrectCsprojs =
            existingCsprojs
                .Where(csproj => csproj != correctCsproj)
                .ToList();

        foreach (var csproj in incorrectCsprojs)
        {
            using (LogicalOperation.Start($"Deleting {csproj}"))
            {
                var path = Path.Combine(Context.Path, csproj);
                File.Delete(path);
                existingCsprojs.Remove(csproj);
            }
        }
    }

    if (existingCsprojs.Count == 1)
    {
        var existingCsproj = existingCsprojs.Single();
        if (existingCsproj != correctCsproj)
        {
            using (LogicalOperation.Start($"Renaming {existingCsproj} to {correctCsproj}"))
            {
                var existingCsprojPath = Path.Combine(Context.Path, existingCsproj);
                File.Move(existingCsprojPath, correctCsprojPath);
                existingCsprojs.Remove(existingCsproj);
                existingCsprojs.Add(correctCsproj);
            }
        }
    }

    if (existingCsprojs.Count == 0)
    {
        using (LogicalOperation.Start($"Creating {correctCsproj}"))
        {
            VisualStudioProject.Create(correctCsprojPath);
        }
    }
}


}
}
