using System;
using MacroDiagnostics;
using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public abstract class ProjectAspect : Aspect<ProjectContext>
    {

        public new static ProjectAspect Create(Type type, ProjectContext context)
        {
            return Create<ProjectAspect>(type, context);
        }


        protected ProjectAspect(ProjectContext context)
            : base(context)
        {
        }


        protected override IDisposable OnAnalysing()
        {
            return LogicalOperation.Start($"Checking for {Description} in {Context.Name}/");
        }


        protected override IDisposable OnApplying()
        {
            return LogicalOperation.Start($"Applying {Description} in {Context.Name}/");
        }

    }
}
