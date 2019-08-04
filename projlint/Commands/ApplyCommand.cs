using System.Collections.Generic;
using projlint.Aspects;
using projlint.Contexts;

namespace
projlint.Commands
{

public static class
ApplyCommand
{


public static int
Apply(RepositoryContext repository, Queue<string> args)
{
    ApplyRepositoryAspects(repository);
    ApplyProjectAspects(repository);
    return 0;
}


static void
ApplyRepositoryAspects(RepositoryContext repository)
{
    foreach (var type in Aspect.AllRepositoryAspects)
    {
        var aspect = RepositoryAspect.Create(type, repository);
        aspect.Apply();
    }
}


static void
ApplyProjectAspects(RepositoryContext repository)
{
    foreach (var project in repository.FindProjects())
    {
        foreach (var type in Aspect.AllProjectAspects)
        {
            var aspect = ProjectAspect.Create(type, project);
            aspect.Apply();
        }
    }
}


}
}
