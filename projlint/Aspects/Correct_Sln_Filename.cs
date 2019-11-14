using System.Diagnostics;
using System.IO;
using System.Linq;
using MacroDiagnostics;
using MacroGuards;
using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class Correct_Sln_Filename : RepositoryAspect
    {

        public static string GetCorrectSlnName(RepositoryContext context)
        {
            Guard.NotNull(context, nameof(context));
            return $"{context.Repository.Name}.sln";
        }


        public Correct_Sln_Filename(RepositoryContext context)
            : base(context)
        {
            Require<Single_Sln_File>();
            correctSlnName = GetCorrectSlnName(context);
        }


        readonly string correctSlnName;
        string slnName;


        protected override bool OnAnalyse()
        {
            slnName =
                Context.EnumerateFiles(Context.Path, "*.sln")
                    .Select(path => Path.GetFileName(path))
                    .Single();

            if (slnName != correctSlnName)
            {
                Trace.TraceWarning($"Existing {slnName} should be named {correctSlnName}");
                return false;
            }

            return true;
        }


        protected override bool OnApply()
        {
            using (LogicalOperation.Start($"Renaming {slnName} to {correctSlnName}"))
            {
                var slnPath = Path.Combine(Context.Path, slnName);
                var correctSlnPath = Path.Combine(Context.Path, correctSlnName);
                File.Move(slnPath, correctSlnPath);
            }

            return true;
        }

    }
}
