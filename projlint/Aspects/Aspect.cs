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
            Name = GetType().Name;
            Description = Name
                .Replace(nameof(Aspect), "")
                .Replace("_", " ")
                .ToLower()
                .Trim();
        }


        public string Name { get; }


        public string Description { get; }


        /// <summary>
        /// Analyse this aspect of the context
        /// </summary>
        ///
        /// <returns>
        /// Whether this aspect of the context is correct
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
        /// Apply this aspect to the context
        /// </summary>
        ///
        /// <returns>
        /// Whether the aspect was successfully applied
        /// </returns>
        ///
        public bool Apply()
        {
            using (OnApplying())
            {
                return OnApply();
            }
        }


        protected virtual IDisposable OnAnalysing()
        {
            return LogicalOperation.Start($"Checking for {Description}");
        }


        protected virtual IDisposable OnApplying()
        {
            return LogicalOperation.Start($"Applying {Description}");
        }


        protected abstract bool OnAnalyse();


        protected virtual bool OnApply()
        {
            return false;
        }

    }
}
