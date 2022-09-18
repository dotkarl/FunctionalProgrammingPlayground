using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaYumba.Functional;
using static LaYumba.Functional.F;
using String = LaYumba.Functional.String;

namespace FunctionalProgrammingPlayground.CoreFunctions.ForEach;
public class LaYumbaForEach
{
    public static void Run()
    {
        // IEnumerable example
        Enumerable.Range(1, 5).ForEach(Console.Write);

        // -- A note on notation
        // You could write the above method as the following equivalent:
        Enumerable.Range(1, 5).ForEach(i => Console.Write(i));
        // Which one is preferable? To me, this notation stands out, at the moment, as more readable:
        // it makes very explicit the fact that you are passing a function (in this case: an Action)
        // as a parameter. However, my preference could simply be a result of not having to use
        // the "function syntax" (i.e. a method without parenthesis) that often. I actually quite
        // like this syntax a bit, since invoking the function is a matter of adding the parenthesis:
        // var w = Console.Write;
        // w("This will be written");
        // It has some intuitive quality, surely.

        // Option example
        var opt = Some("John");
        opt.ForEach(name => Console.WriteLine($"Hello, {name}"));

        // It is good practice to separate pure logic from its side effects.
        // Therefore, the above method should rather be written as follows:
        opt.Map(name => $"Hello, {name}")
            .ForEach(Console.WriteLine);

        // Transform the value inside a Map function;
        // perform a side effect in the ForEach:
        var name = Some("Enrico");
        name.Map(String.ToUpper)
            .ForEach(Console.WriteLine);
        var names = new[] { "Constance", "Albert" };
        names.Map(String.ToUpper)
            .ForEach(Console.WriteLine);
         
    }
}
