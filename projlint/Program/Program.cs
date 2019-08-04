using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Reflection;
using MacroExceptions;
using MacroConsole;
using projlint.Commands;
using System.IO;
using MacroGit;
using MacroIO;
using projlint.Contexts;

namespace
projlint.Program
{

static class
Program
{
    

static int
Main(string[] args)
{
    var traceListener = new ConsoleApplicationTraceListener();
    Trace.Listeners.Add(traceListener);

    try
    {
        return Main2(new Queue<string>(args));
    }

    //
    // An expected user-facing error occurred
    //
    catch (UserException ue)
    {
        Trace.WriteLine("");
        foreach (var ex in ue.UserFacingExceptionChain) Trace.TraceError(ex.Message);
        return 1;
    }

    //
    // An unexpected internal error occurred
    //
    catch (Exception e)
    {
        Trace.WriteLine("");
        Trace.TraceError("An internal error occurred in nugit:");
        Trace.TraceError(ExceptionExtensions.Format(e));
        return 1;
    }
}


static int
Main2(Queue<string> args)
{
    PrintBanner();

    var context = FindContext();

    string command = "";
    if (args.Any())
    {
        command = args.Dequeue();
    }

    switch (command.ToUpperInvariant())
    {
        case "":
        case "ANALYSE":
        case "ANALYZE":
            return AnalyseCommand.Analyse(context, args);
        case "APPLY":
            return ApplyCommand.Apply(context, args);
        case "HELP":
            return HelpCommand.Help(context, args);
        default:
            throw new UserException($"Unrecognised <command>, try `projlint help`");
    }
}


static RepositoryContext
FindContext()
{
    var path = Path.GetFullPath(Environment.CurrentDirectory);

    var repository = GitRepository.FindContainingRepository(path);
    if (repository == null)
    {
        throw new UserException("Current directory is not in a Git repository");
    }

    return new RepositoryContext(repository);
}


static void
PrintBanner()
{
    var name = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductName;
    var version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
    Trace.TraceInformation($"");
    Trace.TraceInformation($"=====================");
    Trace.TraceInformation($"{name} {version}");
    Trace.TraceInformation($"=====================");
    Trace.TraceInformation($"");
}


}
}
