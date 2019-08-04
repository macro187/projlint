using System;
using projlint.Contexts;

namespace
projlint.Aspects
{

public abstract class
RepositoryAspect : Aspect<RepositoryContext>
{


public static RepositoryAspect
Create(Type type, RepositoryContext context)
{
    return Create<RepositoryAspect>(type, context);
}


protected
RepositoryAspect(RepositoryContext context)
    : base(context)
{
}


}
}
