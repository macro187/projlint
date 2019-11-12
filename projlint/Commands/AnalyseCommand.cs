using System.Collections.Generic;
using ProjLint.Aspects;
using ProjLint.Contexts;

namespace ProjLint.Commands
{
    public static class AnalyseCommand
    {

        public static int Analyse(RepositoryContext repository, Queue<string> args)
        {
            bool success = true;

            if (!AnalyseRepositoryAspects(repository)) success = false;
            if (!AnalyseProjectAspects(repository)) success = false;

            return success ? 0 : 1;
        }


        static bool AnalyseRepositoryAspects(RepositoryContext repository)
        {
            bool success = true;

            foreach (var type in Aspect.AllRepositoryAspects)
            {
                var aspect = RepositoryAspect.Create(type, repository);
                if (!aspect.Analyse()) success = false;
            }

            return success;
        }


        static bool AnalyseProjectAspects(RepositoryContext repository)
        {
            bool success = true;

            foreach (var project in repository.FindProjects())
            {
                foreach (var type in Aspect.AllProjectAspects)
                {
                    var aspect = ProjectAspect.Create(type, project);
                    if (!aspect.Analyse()) success = false;
                }
            }

            return success;
        }

    }
}
