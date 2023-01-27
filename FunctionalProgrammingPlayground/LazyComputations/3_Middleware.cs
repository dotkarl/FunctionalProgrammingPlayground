using Dapper;
using FunctionalProgrammingPlayground.PartialApplication;
using Microsoft.Extensions.Logging;
using System.Data;

namespace FunctionalProgrammingPlayground.LazyComputations;
public class Middleware
{
    public static void Run()
    {
        // Using higher order functions (HOFs)
        // can lead to deeply nested callbacks.
        // This is not pleasant to work with.
        // You could use monadic composition
        // to flatten your workflows instead.

        // Imagine the following method,
        // used to execute a stored procedure:
        ConnectionString connString = "retrieve connectionstring from config";

        void LogWithoutTracing(string message) =>
            ConnectionHelper.Connect(
                connString,
                c => c.Execute(
                    "sp_create_log", 
                    message, 
                    commandType: CommandType.StoredProcedure));

        // Say you'd want to trace this
        // with the Trace function defined below.
        // You could do it like this:
        void LogWithTracing(string message) =>
            Instrumentation.Trace(
                null, // use ILogger here;
                          // this object is somehow missing from the example in the book 
                "CreateLog",
                () => ConnectionHelper.Connect(
                    connString,
                    c => c.Execute(
                            "sp_create_log",
                            message,
                            commandType: CommandType.StoredProcedure)));
        // You see: this is becoming hard to read.
        // For every HOF you add, your callbacks
        // are nested one level deeper.

        // Instead, you want to compose a middleware pipeline.
        // This is a pipeline that is U-shaped.
        // On the way to, say, a database, you go through
        // some functions, and on the way out you do the same
        // in reverse order.
    }
}

public static class Instrumentation
{
    public static T Trace<T>(this ILogger log, string op, Func<T> f)
    {
        log.LogTrace($"Entering {op}");
        T t = f();
        log.LogTrace($"Leaving {op}");
        return t;
    }
}
