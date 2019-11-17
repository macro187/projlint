using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ProjLint.Aspects;
using ProjLint.Contexts;

namespace ProjLint.Commands
{
    public static class AnalyseCommand
    {

        public static int Analyse(RepositoryContext repository, Queue<string> _)
        {
            return Analyse(repository, false);
        }


        public static int Analyse(RepositoryContext repository, bool tryApplying)
        {
            bool success = true;

            success &= AnalyseRepositoryAspects(repository, tryApplying);
            success &= AnalyseProjectAspects(repository, tryApplying);

            return success ? 0 : 1;
        }


        static bool AnalyseRepositoryAspects(RepositoryContext repository, bool tryApplying)
        {
            return Analyse(repository, Aspect.AllRepositoryAspects, tryApplying);
        }


        static bool AnalyseProjectAspects(RepositoryContext repository, bool tryApplying)
        {
            bool result = true;

            foreach (var project in repository.FindProjects())
            {
                result &= Analyse(project, Aspect.AllProjectAspects, tryApplying);
            }

            return result;
        }


        static bool Analyse<TContext>(TContext context, IEnumerable<Type> allAspects, bool tryApplying)
        {
            var instances = BuildAspectInstances(allAspects, context);
            var results = new Dictionary<Type, bool?>();
            var result = true;

            var prioritisedAspects = allAspects.OrderByDescending(a => instances[a].Priority);

            foreach (var aspect in prioritisedAspects)
            {
                result &= Analyse(context, aspect, instances, results, tryApplying);
            }

            return result;
        }


        static bool Analyse<TContext>(
            TContext context,
            Type aspect,
            IDictionary<Type, Aspect<TContext>> instances,
            IDictionary<Type, bool?> results,
            bool tryApplying
        )
        {
            if (results.ContainsKey(aspect))
            {
                if (!results[aspect].HasValue)
                {
                    Trace.TraceWarning($"Breaking aspect dependency graph cycle at {aspect.Name}");
                    return true;
                }

                return results[aspect].Value;
            }

            results[aspect] = null;

            var instance = instances[aspect];
            var result = true;

            foreach (var requiredAspect in instance.RequiredAspects)
            {
                result &= Analyse(context, requiredAspect, instances, results, tryApplying);
            }

            if (result)
            {
                result &= instance.Analyse();

                if (!result && tryApplying)
                {
                    result = instance.Apply();
                }
            }

            results[aspect] = result;

            return result;
        }


        static IDictionary<Type, Aspect<TContext>> BuildAspectInstances<TContext>(
            IEnumerable<Type> aspects,
            TContext context
        )
        {
            var dictionary = new Dictionary<Type, Aspect<TContext>>();

            foreach (var aspect in aspects)
            {
                dictionary.Add(aspect, Aspect<TContext>.Create(aspect, context));
            }

            return dictionary;
        }

    }
}
