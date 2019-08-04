using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using MacroConsole;
using MacroExceptions;
using MacroIO;
using projlint.Contexts;

namespace
projlint.Commands
{

public static class
HelpCommand
{


public static int
Help(RepositoryContext context, Queue<string> args)
{
    Trace.TraceInformation("");

    using (var reader = new StreamReader(
        Assembly.GetCallingAssembly().GetManifestResourceStream("projlint.readme.md")))
    {
        foreach (
            var line
            in ReadmeFilter.SelectSections(
                reader.ReadAllLines(),
                "Synopsis",
                "Commands"))
        {
            Trace.TraceInformation(line);
        }
    }

    if (args.Any()) throw new UserException("Too many arguments");

    return 0;
}


}
}
