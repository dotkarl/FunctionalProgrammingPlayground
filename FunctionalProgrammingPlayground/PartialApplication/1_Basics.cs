using Name = System.String;
using Greeting = System.String;
using PersonalizedGreeting = System.String;
using LaYumba.Functional;

namespace FunctionalProgrammingPlayground.PartialApplication;
public class Basics
{
    public static void Run()
    {
        // # Normal function application: an example

        // ## Case
        // A function that expects two arguments
        // is given two arguments at the time it is called

        // A function that takes two parameters.
        // (Greeting, Name) -> PersonalizedGreeting
        var greet = (Greeting gr, Name name) => $"{gr}, {name}";
        // A list of second parameters
        Name[] names = { "Tristan", "Ivan" };
        // The first parameter is given inline,
        // the second parameter comes from the list above
        names.Map(n => greet("Hello", n))
            .ForEach(Console.WriteLine);

        // ## Problem

        // Wouldn't it be better to fix the greeting to be "Hello" outside the scope of Map?
        // Deciding on "Hello" as the greeting to use for all names is a more general decision
        // and can be taken first.

        // ## Solution

        // ### 1. Manually enabling partial application

        // A function that returns a function
        // (Greeting) -> ((Name) -> PersonalizedGreeting)
        var greetWith = (Greeting gr) => (Name name) => $"{gr}, {name}";
        var greetFormally = greetWith("Good evening");
        names.Map(greetFormally)
            .ForEach(Console.WriteLine);

        // Compare the signatures of the normal and partial function application:

        // greet    : (Greeting, Name) → PersonalizedGreeting
        // greetWith: Greeting → (Name → PersonalizedGreeting)

        // Note: arrow notation is right-associative: everything to the right of an arrow is grouped.

        // greetWith is said to be in curried form:
        // all arguments are supplied one by one via function invocation.

        // ### 2. Generalizing partial application

        // You could define an adapter function that allows you to provide
        // just one argument to a multi-argument function,
        // producing a function that is waiting to receive the remaing arguments.
        // Seet the Apply function, defined in FuncExtensions below.
        var greetInformally = greet.Apply("Hey");
        names.Map(greetInformally)
            .ForEach(Console.WriteLine);

        // This is the underlying pattern:
        // You start with a general function,
        // then you provide arguments (i.e. partial application)
        // to create a specialized version of this function.

        // In order to make this work, it is best practice to
        // put the arguments that represent the data the operation will affect, last;
        // the arguments that represent how the function wil operate should come first.

        // In the example above: the name (= data) is the second argument,
        // the greeting (= operation) is the first one.
        // The operation is likely to be decided sooner than the data.
    }
}

public static class FuncExtensions
{
    // Signature:
    // Apply: ((T1, T2) -> R, T1) -> ((T2) -> R)
    public static Func<T2, R> Apply<T1, T2, R>(this Func<T1, T2, R> f, T1 t1) =>
        t2 => f(t1, t2);
    // What does this mean?
    // - You have a function that needs two arguments;
    // - You provide the first argument;
    // - It returns a function that only needs the second argument.

    // Of course, there is nothing stopping you from defining similar functions
    // for functions that need more than two arguments, e.g.:
    public static Func<T2, T3, R> Apply<T1, T2, T3, R>(this Func<T1, T2, T3, R> f, T1 t1) =>
        (t2, t3) => f(t1, t2, t3);

}
