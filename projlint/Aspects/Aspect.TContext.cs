using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MacroGuards;

namespace ProjLint.Aspects
{
    public abstract class Aspect<TContext> : Aspect
    {

        public static new IReadOnlyCollection<Type> AllAspects { get; } =
            Aspect.AllAspects
                .Where(t => typeof(Aspect<TContext>).IsAssignableFrom(t))
                .ToArray();


        public static TAspect Create<TAspect>(Type type, TContext context)
            where TAspect : Aspect<TContext>
        {
            var aspect = Create(type, context) as TAspect;

            if (aspect == null)
            {
                throw new ArgumentException($"{type.Name} is not a {typeof(TAspect).Name}", nameof(type));
            }

            return aspect;
        }


        public static Aspect<TContext> Create(Type type, TContext context)
        {
            Guard.NotNull(type, nameof(type));
            Guard.NotNull(context, nameof(context));

            if (!typeof(Aspect<TContext>).IsAssignableFrom(type))
            {
                throw new ArgumentException($"{type.Name} is not an Aspect<{typeof(TContext).Name}>", nameof(type));
            }

            return (Aspect<TContext>)Activator.CreateInstance(type, context);
        }


        protected Aspect(TContext context)
        {
            Guard.NotNull(context, nameof(context));
            Context = context;
            requiredAspects = new List<Type>();
            RequiredAspects = new ReadOnlyCollection<Type>(requiredAspects);
        }


        public TContext Context { get; }


        public ICollection<Type> RequiredAspects { get; }
        readonly List<Type> requiredAspects;


        protected void Require<TAspect>()
            where TAspect : Aspect<TContext>
        {
            var type = typeof(TAspect);
            requiredAspects.Add(type);
        }

    }
}
