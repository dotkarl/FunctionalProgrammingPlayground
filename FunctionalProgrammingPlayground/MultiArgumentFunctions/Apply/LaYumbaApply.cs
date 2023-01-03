using FsCheck;
using LaYumba.Functional;
using static LaYumba.Functional.F;

namespace FunctionalProgrammingPlayground.MultiArgumentFunctions.Apply;
public class LaYumbaApply
{
    public static void Run()
    {
        // Apply unary function `doubl` to optional int:
        var doubl = (int i) => i * 2;
        Some(3).Map(doubl); // => Some(6)

        // Now, how would you go about applying a binary funciton,
        // e.g. multiplication?

        // The answer: currying allows you to turn an n-ary function
        // into a unary function that, when given its argument,
        // returns a (n-1)-ary function.
        // You can use `Map` with any function as long as it's curried.
        var multiply = (int x) => (int y) => x * y;
        var multiplyBy3 = Some(3).Map(multiply);

        // Here is the signature of `Map` for functor F:
        // Map: F<T> -> (T -> R) -> F<R>

        // If R happens to be a function T1 -> T2, we get:
        // F<T> -> (T -> T1 -> T2) -> F<T1 -> T2>
        // This is a function wrapped in an elevated type (!)

        // ("There is nothing special about an elevated function.
        // Functions are values, so it's simply another value
        // wrapped in on of the usual containers."
        // Maybe I should look at it this way: 
        // a function is nothing more than a value
        // that hasn't been computed yet,
        // because some of the necessary input is missing.)

        // You have a function wrapped in an elevated type.
        // How do you apply its argument(s)?
        // Should you unwrap it first, apply the argument,
        // and then wrap it again? No!
        // This is where the `Apply` function comes in.
        // This function does that for you.
        // (See below for its implementation in LaYumba.Functional.)
        var twelve = Some(3).Map(multiply).Apply(Some(4)); // => Some(12)
        var none1 = Some(3).Map(multiply).Apply(None); // => None

        // In order to step through the implementation, set a break point
        // at the following line:
        var sixteen = Some(4).Map(multiply).Apply1(Some(4));

        // You don't need to first elevate the value of the first argument
        // and then apply the function to it. You could also
        // elevate the function itself first, and then
        // apply the arguments to it:
        var fifteen = Some(multiply)
            .Apply(Some(5))
            .Apply(Some(3));
        var none2 = Some(multiply)
            .Apply(None)
            .Apply(Some(4));


    }
}

public static class LaYumbaApplyExample
{
    // Given an Option optF and an Option optT
    // if optF has a value f (which is a function!),
    // try to apply optT to it, i.e.:
    // if optT has a value t (which is a value!),
    // apply f to t and wrap the result in a new Option.
    public static Option<R> Apply1<T, R>(this Option<Func<T, R>> optF, Option<T> optT) => 
        optF.Match(
            () => None,
            (f) => optT.Match(
                () => None,
                (t) => Some(f(t))));

    public static Option<Func<T2, R>> Apply2<T1, T2, R>(this Option<Func<T1, T2, R>> optF, Option<T1> optT) => 
        Apply1(optF.Map(F.Curry), optT);
}
