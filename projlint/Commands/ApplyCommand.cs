using System.Collections.Generic;
using ProjLint.Contexts;

namespace ProjLint.Commands
{
    public static class ApplyCommand
    {

        public static int Apply(RepositoryContext repository, Queue<string> _)
        {
            return AnalyseCommand.Analyse(repository, true);
        }

    }
}
