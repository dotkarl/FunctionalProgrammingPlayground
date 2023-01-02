using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace FunctionalProgrammingPlayground.MultiArgumentFunctions.LINQ;
public static class LanguageExtLinq
{
    public static void Run()
    {
        // LanguageExt also has overloads for the Select and Where methods.
        // This allows you to write LINQ queries using Options. Nice!
        var minusOne = Some(123).Select(x => x - 1);
        var positive = Some(-1).Where(x => x > 0);
    }
}
