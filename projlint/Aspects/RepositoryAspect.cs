using System;
using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public abstract class RepositoryAspect : Aspect<RepositoryContext>
    {

        public new static RepositoryAspect Create(Type type, RepositoryContext context)
        {
            return Create<RepositoryAspect>(type, context);
        }


        protected RepositoryAspect(RepositoryContext context)
            : base(context)
        {
        }

    }
}
