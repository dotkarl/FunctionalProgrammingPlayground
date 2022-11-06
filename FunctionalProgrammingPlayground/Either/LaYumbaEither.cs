using LaYumba.Functional;
using static LaYumba.Functional.F;
using static System.Math;
using Unit = System.ValueType;

namespace FunctionalProgrammingPlayground.Either;
public class LaYumbaEither
{
    public static void Run()
    {
        // # Introduction

        // Either is a monad that represents two possible outcomes:
        // - A right one (operation successful, contains the result)
        // - A left one (operation failed, contains error information)

        // (Note that this is a biased implementation of Either.
        // There exist also unbiased implementations
        // in which both Left and Right represent valid code paths.)

        // Lifts the int to an Either.Right<int>
        var right = Right(12);
        // Lifts the string to an Either.Left<string>
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


        // # Calculation example

        // Suppose the following calculation:

        // f(x, y) -> sqrt(x/y)

        // This will fail if:
        // - y is zero
        // - the ratio x/y is negative

        var yIs0 = Calc(3, 0);
        var ratioNegative = Calc(-3, 3);
        var calc = Calc(-3, -3);

        Either<string, double> Calc(double x, double y)
        {
            if (y == 0)
            {
                // Returning a string in a method
                // that has Either<string, double> as its return type
                // will automatically lift that string
                // to an Either<string, double>, i.e. an Either.Left<string>
                return "y cannot be 0";
            }

            if (x != 0 && Sign(x) != Sign(y))
            {
                return "x / y cannot be negative";
            }

            // The same goes for the double at the right side
            return Sqrt(x / y);
        }

        Console.WriteLine(yIs0);
        Console.WriteLine(ratioNegative);
        Console.WriteLine(calc);


        // # The core functions

        // For Map, ForEach and Bind, the second parameter function is only applied to the Right side.
        // The Left signals an error, so there is no need to perform any function against that side.
        // (See also the note above on biased vs. unbiased implementations.)

        // There is no Where implementation for Either, because Where can only be defined for structures
        // where a zero value exists (e.g. empty list for IEnumerable, None for Option).

        var map = calc.Map(x => x * 2);
        Console.WriteLine(map);
        var forEach = calc.ForEach(x => Console.WriteLine(x));
        // Note: this prints "Right(())" to the Console,
        // I guess because the ForEach's Action does not return a value
        Console.WriteLine(forEach);

        // This does not work:
        // var bind1 = calc.Bind(x => x * 3);
        // Why not?
        // Let's try with a Func of the right (no pun intended) type:
        Func<double, Either<string, double>> bindFunc = x => x * 3;
        var bind2 = calc.Bind(bindFunc);
        // That works! The problem with the method above was that it couldn't infer the return type.
        // Supplying the generic arguments makes the error go away:
        var bind3 = calc.Bind<string, double, double>(x => x * 3);
        Console.WriteLine(bind3);
        // Note that it can't infer the return type because your Func may very well
        // transform the double above to an int, say.


        // # Comparing Option and Either

        // Imagine we're modeling a recruitment process.
        // This is an Option based approach:

        Func<Candidate, bool> IsEligible;
        Func<Candidate, Option<Candidate>> TechTestOpt;
        Func<Candidate, Option<Candidate>> InterviewOpt;

        Option<Candidate> RecruitOpt(Candidate c) =>
            Some(c)             // Lift Candidate to Option<Candidate>
            .Where(IsEligible)  // Where takes the bounded variable and returns a bool
                                // If the Candidate does not satisfy the condition, this will result in a None
            .Bind(TechTestOpt)     
            .Bind(InterviewOpt);

        // The drawback of this approach is that it does not give us any information on why either
        // the TechtTest or the Interview has failed; it simply returns null.

        // This is an Either based approach:

        Func<Candidate, Either<Rejection, Candidate>> TechTestEither;
        Func<Candidate, Either<Rejection, Candidate>> InterviewEither;

        // Transform predicate into Either-returning function 
        // because Where is not supported by Either
        Either<Rejection, Candidate> CheckEligibilityEither(Candidate c)
        {
            if (IsEligible(c)) return c;
            else return new Rejection("Not eligible");
        }

        Either<Rejection, Candidate> RecruitEither(Candidate c) =>
            Right(c)
                .Bind(CheckEligibilityEither)
                .Bind(TechTestEither)
                .Bind(InterviewEither);


        // # Chaining operations that may fail

        // Let's say you'd like to cook a meal for your spouse.
        // It may fail for a number of reasons:
        // - You wake up too late
        // - You're unable to go to the store
        // - You can't cook for sh**.

        Func<Either<Reason, Unit>> WakeUpEarly = () => new Either<Reason, Unit>();
        Func<Either<Reason, Ingredients>> ShopForIngredients = () => new Either<Reason, Ingredients>();
        Func<Ingredients, Either<Reason, Food>> CookRecipe = i => new Either<Reason, Food>();

        Action<Food> EnjoyTogether = f => Console.WriteLine($"Enjoying {f}");
        Action<Reason> ComplainAbout = r => Console.WriteLine($"{r} sucks!");
        Action OrderPizza = () => Console.WriteLine("Order pizza");

        WakeUpEarly()
            .Bind(_ => ShopForIngredients())
            .Bind(CookRecipe)
            .Match(
                Right: dish => EnjoyTogether(dish),
                Left: reason =>
                {
                    ComplainAbout(reason);
                    OrderPizza();
                });

        // You can chain Eithers, so that if all goes well, you end up with the correct result,
        // otherwise the failed state will be passed on. See the following diagram:

        //   o WakeUpEarly
        //  / \
        // |   R ShopForIngredients
        // |  / \
        // | |   R CookRecipe
        // | |  / \
        // | | |   R EnjoyTogether
        // | | |
        // ------> L OrderPizza

        // You could view a workflow containing several Either-returning functions
        // as a two-track system:

        // - There's a main track (happy path) that goes from R1 to Rn
        // - There's an alternative parallel track en the Left side

        // If everything goes right, you'll stay on the main track,
        // otherwise, you'll go to the alternative track,
        // and you'll stay on there untill the end of the chain.
        // Match is the end of the road, where the disjunction of the parallel tracks take place.
    }

    record Candidate();
    record Rejection(string Reason);

    record Reason();
    record Ingredients();
    record Food();
}
