using Name = System.String;
using Greeting = System.String;
using LaYumba.Functional;

namespace FunctionalProgrammingPlayground.PartialApplication;
internal class Currying
{
    public void Run()
    {
        // Currying is the process of transforming
        // an n-ary function f that takes the arguments t1, t2,..., tn
        // into a unary function that takes t1 
        // and yields a new fuction that takes t2, and so on.

        // Currying is transforming this: (T1, T2, ..., Tn) -> R
        // into this: T1 -> T2 -> ... -> Tn -> R

        // We've already seen an example of this:
        // (Greeting, Name) -> PersonalizedGreeting
        var greet = (Greeting gr, Name name) => $"{gr}, {name}";
        // Greeting -> Name -> PersonalizedGreeting
        var greetWith = (Greeting gr) => (Name name) => $"{gr}, {name}";

        // You could call it like this:
        greetWith("hello")("world"); // => "hello, world"

        // Let's break this up:
        var curried = greetWith("hello"); // => Func<Name, PersonalizedGreeting>()
        var result = curried("world");    // => PersonalizedGreeting

        // This is useful when you're doing partial application, e.g.
        // first, provide the greeting parameter...
        var greetFormally = greetWith("Good evening");
        // ...then retrieve all the names...
        Name[] names = { "Tristan", "Ivan" };
        // ...finally, provide the name parameter for each retrieved name
        names.Map(greetFormally).ForEach(Console.WriteLine);

        // This is manual currying.
        // It's possible to define generic functions that curry an n-ary function.
        // For examples of definitions, see CurryExtensions below.
        
        // Application:
        var greetWith2 = greet.Curry();
        var greetNostalgically = greetWith2("Arrivederci");
        names.Map(greetNostalgically).ForEach(Console.WriteLine);

        // Partial application and currying are related yet distinct.
        // - Partical application is giving a function fewer arguments than it expects,
        //   obtaning a function that's particularized with the values of the arguments given so far
        // - Currying is transforming an n-ary function into a unary function
        //   to which arguments can be sucessively given to eventually get the same result as the original function.
        //   Note: you don't give any arguments to the function just yet by currying it.
        // Currying optimizes a function for partial application.
    }
}

public static class CurryExtensions
{
    // Curry a binary function
    // Returns a Func that takes T1 as first paramater, and Func<T2, R> as second parameter
    // Input:  Func<T1, T2, R>
    // Output: Func<T1, Func<T2, R>>
    // Arrow notation (brackets added for readability):
    // ((t1, t2) -> r) -> (t1 -> (t2 -> r))
    public static Func<T1, Func<T2, R>> Curry<T1, T2, R>(this Func<T1, T2, R> f) =>
        t1 => t2 => f(t1, t2);

    // Curry a ternary function
    // (t1, t2, t3) -> t1 -> t2 -> t3 -> r
    public static Func<T1,Func<T2, Func<T3,R>>> Curry<T1, T2, T3, R>(this Func<T1, T2, T3, R> f) =>
        t1 => t2 => t3 => f(t1, t2, t3);

}

