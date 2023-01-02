using LaYumba.Functional;
using static LaYumba.Functional.F;
using Double = LaYumba.Functional.Double;

namespace FunctionalProgrammingPlayground.MultiArgumentFunctions.LINQ;
public static class LaYumbaLinq
{
    public static void Run()
    {
        // Note that an Option has a Select extension method.
        // Under the hood, this uses the Map function (which is equivalent,
        // but more common in functional programming namewise).
        var minusOne = Some(123)
            .Select(x => x - 1);
        // This allows you to write LINQ queries using Options.

        // # Simple examples: single from clause

        // Here are some simple examples:
        var doubled = from x in Some(12)
                      select x * 2;
        var none = from x in (Option<int>)None
                   select x * 2;
        var eq = (from x in Some(1) select x * 2) == Some(1).Map(x => x * 2);

        // # Complex example: multiple from clauses

        // Here is a more complex example:
        Console.WriteLine("Enter first addend:");
        var s1 = Console.ReadLine();
        Console.WriteLine("Enter second addend:");
        var s2 = Console.ReadLine();

        // 1. Using LINQ query
        var result = from a in Int.Parse(s1)
                     from b in Int.Parse(s2)
                     select a + b;

        Console.WriteLine(result.Match(
            () => "Please enter two valid integers",
            (r) => $"{s1} + {s2} = {r}"));

        // The query above can also be written as follows:
        // 2. Normal method invocation
        Int.Parse(s1)
            .Bind(a => Int.Parse(s2)
                .Map(b => a + b));

        // 3. Method invocation that the LINQ query will be converted to
        Int.Parse(s1)
            .SelectMany(a => Int.Parse(s2), (a, b) => a + b);

        // 4. Using Apply
        Some(new Func<int, int, int>((a, b) => a + b))
            .Apply(Int.Parse(s1))
            .Apply(Int.Parse(s2));

        // The LINQ query is the most readable alternative.
        // It might feel odd to use a SQL-like syntax for something that
        // has nothing to do with querying data, but actually
        // LINQ was modelled after equivalent constructs in functional languages
        // so this use is perfectly legitimate.

        // # Let. where

        // In order to use the `let` keyword,
        // which lets you store the intermediate result of a computation,
        // no extra work is needed.
        // This keyword relies on the Select function.
        // For the `where` keyword, a Where function is needed.
        // This is already defined for Options.
        var hypothenuse = from a in Double.Parse(s1)
                          where a >= 0
                          let aa = a * a
                          from b in Double.Parse(s2)
                          where b >= 0
                          let bb = b * b
                          select Math.Sqrt(aa + bb);
    }
}
