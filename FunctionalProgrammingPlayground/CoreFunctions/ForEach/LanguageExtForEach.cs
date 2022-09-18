using LanguageExt;
using LaYumba.Functional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace FunctionalProgrammingPlayground.CoreFunctions.ForEach;
internal class LanguageExtForEach
{
    public static void Run()
    {
        // This functionality is identical to the LaYumba implementation
        var opt = Some("John");
        opt.ForEach(name => Console.WriteLine($"Hello, {name}"));

        opt.Map(name => $"Hello, {name}")
            .ForEach(Console.WriteLine);
    }
}
