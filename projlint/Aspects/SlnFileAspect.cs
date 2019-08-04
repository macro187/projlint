using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using MacroDiagnostics;
using MacroExceptions;
using projlint.Contexts;

namespace
projlint.Aspects
{

public class
SlnFileAspect : RepositoryAspect
{


public
SlnFileAspect(RepositoryContext context)
    : base(context)
{
    correctSln = $"{context.Repository.Name}.sln";
}


readonly string correctSln;
ICollection<string> existingSlns;


protected override bool
OnAnalyse()
{
    existingSlns =
        Directory.EnumerateFiles(Context.Repository.Path, "*.sln", SearchOption.TopDirectoryOnly)
            .Select(path => Path.GetFileName(path))
            .ToList();

    if (existingSlns.Count == 0)
    {
        Trace.TraceWarning("No .sln file found");
        return false;
    }

    if (existingSlns.Count > 1)
    {
        Trace.TraceWarning("Multiple .sln files found:");
        foreach (var sln in existingSlns)
        {
            Trace.TraceWarning(sln);
        }

        return false;
    }

    var existingSln = existingSlns.Single();
    if (existingSln != correctSln)
    {
        Trace.TraceWarning($"Existing {existingSln} should be named {correctSln}");
        return false;
    }

    return true;
}


protected override void
OnApply()
{
    if (existingSlns.Count > 1)
    {
        var incorrectSlns =
            existingSlns
                .Where(sln => sln != correctSln)
                .ToList();

        foreach (var sln in incorrectSlns)
        {
            using (LogicalOperation.Start($"Deleting {sln}"))
            {
                var path = Path.Combine(Context.Repository.Path, sln);
                File.Delete(path);
                existingSlns.Remove(sln);
            }
        }
    }

    if (existingSlns.Count == 1)
    {
        var existingSln = existingSlns.Single();
        if (existingSln != correctSln)
        {
            using (LogicalOperation.Start($"Renaming {existingSln} to {correctSln}"))
            {
                var existingSlnPath = Path.Combine(Context.Repository.Path, existingSln);
                var correctSlnPath = Path.Combine(Context.Repository.Path, correctSln);
                File.Move(existingSlnPath, correctSlnPath);
                existingSlns.Remove(existingSln);
                existingSlns.Add(correctSln);
            }
        }
    }

    if (existingSlns.Count == 0)
    {
        using (LogicalOperation.Start($"Creating {correctSln}"))
        {
            var exitCode =
                ProcessExtensions.ExecuteAny(
                    true, true,
                    Context.Repository.Path,
                    "dotnet", "new", "sln", "--name", correctSln);

            if (exitCode != 0)
            {
                throw new UserException("dotnet new sln failed");
            }
        }
    }
}


}
}
