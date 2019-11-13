using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MacroDiagnostics;

namespace ProjLint.Aspects
{
    public abstract class Aspect
    {

        public static IReadOnlyCollection<Type> AllAspects { get; } =
            Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(Aspect).IsAssignableFrom(t))
                .Where(t => !t.IsAbstract)
                .ToArray();


        public static IReadOnlyCollection<Type> AllRepositoryAspects { get; } =
            AllAspects
                .Where(t => typeof(RepositoryAspect).IsAssignableFrom(t))
                .ToArray();


        public static IReadOnlyCollection<Type> AllProjectAspects { get; } =
            AllAspects
                .Where(t => typeof(ProjectAspect).IsAssignableFrom(t))
                .ToArray();


        protected Aspect()
        {
            Name =
#if (NET461)
                    GetType().Name
                        .Replace(GetType().BaseType.Name, "")
                        .Replace(nameof(Aspect), "");
#else
                    GetType().Name
                        .Replace(GetType().BaseType.Name, "", StringComparison.Ordinal)
                        .Replace(nameof(Aspect), "", StringComparison.Ordinal);
#endif
        }


        public string Name { get; }


        /// <summary>
        /// Analyse this aspect of the repository or project
        /// </summary>
        ///
        /// <returns>
        /// Whether this aspect is already correctly applied
        /// </returns>
        ///
        public bool Analyse()
        {
            using (OnAnalysing())
            {
                return OnAnalyse();
            }
        }


        /// <summary>
        /// Based on analysis, apply this aspect where necessary
        /// </summary>
        ///
        public void Apply()
        {
            Analyse();
            using (OnApplying())
            {
                OnApply();
            }
        }


        protected virtual IDisposable OnAnalysing()
        {
            return LogicalOperation.Start($"Analysing {Name}");
        }


        protected virtual IDisposable OnApplying()
        {
            return LogicalOperation.Start($"Applying {Name}");
        }


        protected abstract bool OnAnalyse();


        protected abstract void OnApply();

    }
}
