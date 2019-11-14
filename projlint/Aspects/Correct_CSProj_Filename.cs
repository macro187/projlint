using System.Diagnostics;
using System.IO;
using System.Linq;
using MacroDiagnostics;
using MacroGuards;
using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class Correct_CSProj_Filename : ProjectAspect
    {

        public static string GetCorrectCSProjName(ProjectContext context)
        {
            Guard.NotNull(context, nameof(context));
            return $"{context.Name}.csproj";
        }


        public Correct_CSProj_Filename(ProjectContext context)
            : base(context)
        {
            Require<A_Single_CSProj_File>();
            correctCsprojName = GetCorrectCSProjName(context);
        }


        readonly string correctCsprojName;
        string csprojName;


        protected override bool OnAnalyse()
        {
            csprojName =
                Context.Repository.EnumerateFiles(Context.Path, "*.csproj")
                    .Select(path => Path.GetFileName(path))
                    .Single();

            if (csprojName != correctCsprojName)
            {
                Trace.TraceWarning($"Existing {csprojName} should be named {correctCsprojName}");
                return false;
            }

            return true;
        }


        protected override bool OnApply()
        {
            using (LogicalOperation.Start($"Renaming {csprojName} to {correctCsprojName}"))
            {
                var csprojPath = Path.Combine(Context.Path, csprojName);
                var correctCsprojPath = Path.Combine(Context.Path, correctCsprojName);
                File.Move(csprojPath, correctCsprojPath);
            }

            return true;
        }

    }
}
