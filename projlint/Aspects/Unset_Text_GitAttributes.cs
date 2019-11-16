using System;
using System.Diagnostics;
using MacroExceptions;
using ProjLint.Contexts;

namespace ProjLint.Aspects
{
    public class Unset_Text_GitAttributes : GitAttributesAspect
    {

        public Unset_Text_GitAttributes(RepositoryContext context)
            : base(context, "* -text")
        {
            Require<Incorrect_Text_False_GitAttributes>();
            SetPriority(100);
        }


        protected override void OnApplied()
        {
            Trace.TraceWarning(
                string.Join(
                    Environment.NewLine,
                    $"'* -text' has been added to .gitattributes.",
                    $"Commit this change now.",
                    $"Then you may need to run 'git add --renormalize' to refresh working directory files.",
                    $"Then you can then continue to 'projlint apply' remaining aspects."));

            throw new UserException("Finish applying text .gitattribute changes before proceeding");
        }

    }
}
