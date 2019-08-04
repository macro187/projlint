using System;
using System.Collections.Generic;
using System.Linq;
using MacroGuards;

namespace
projlint.Aspects
{

public abstract class
Aspect<TContext> : Aspect
{


public static new IReadOnlyCollection<Type>
AllAspects { get; } =
    Aspect.AllAspects
        .Where(t => typeof(Aspect<TContext>).IsAssignableFrom(t))
        .ToArray();


protected static TAspect
Create<TAspect>(Type type, TContext context)
    where TAspect : Aspect<TContext>
{
    Guard.NotNull(type, nameof(type));
    Guard.NotNull(context, nameof(context));
    if (!typeof(TAspect).IsAssignableFrom(type))
    {
        throw new ArgumentException($"{type.Name} is not a {typeof(TAspect).Name}", nameof(type));
    }

    return (TAspect)Activator.CreateInstance(type, context);
}


protected
Aspect(TContext context)
{
    Guard.NotNull(context, nameof(context));
    Context = context;
}


public TContext
Context { get; }


}
}
