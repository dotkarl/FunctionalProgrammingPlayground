using LanguageExt;
using static LanguageExt.Prelude;
using static System.Math;

namespace FunctionalProgrammingPlayground.Either;
public class LanguageExtEither
{
    public static void Run()
    {
        var right = Right(12);
        var left = Left("oops");

        var result = Render(right); // The result is: 12
        var invalid = Render(left); // Invalid value: oops

        string Render(Either<string, int> val) =>
            val.Match(
                l => $"Invalid value: {l}",
                r => $"The result is: {r}");

        Console.WriteLine(right); // Note that these will print "Right(12)"
        Console.WriteLine(left);  // and "Left("oops")" to the Console
        Console.WriteLine(result);
        Console.WriteLine(invalid);

        var yIs0 = Calc(3, 0);
        var ratioNegative = Calc(-3, 3);
        var calc = Calc(-3, -3);

        Either<string, double> Calc(double x, double y)
        {
            if (y == 0)
            {
                return "y cannot be 0";
            }

            if (x != 0 && Sign(x) != Sign(y))
            {
                return "x / y cannot be negative";
            }

            return Sqrt(x / y);
        }

        Console.WriteLine(yIs0);
        Console.WriteLine(ratioNegative);
        Console.WriteLine(calc);

        var map = calc.Map(x => x * 2);
        Console.WriteLine(map);
        // You have to explicitly state for which side of the either
        // you want the Action performed. It does not assume that
        // Right is the "right" one, and left the alternative path,
        // i.e. this implementation is unbiased.
        var forEach = calc.Right(x => Console.WriteLine(x));
        Console.WriteLine(forEach);

        Func<double, Either<string, double>> bindFunc = x => x * 3;
        var bind2 = calc.Bind(bindFunc);
        // You only need to provide the return type as the generic parameter,
        // very nice! An improvement from LaYumba.Functional.
        var bind3 = calc.Bind<double>(x => x * 3);
        Console.WriteLine(bind3);
    }
}
