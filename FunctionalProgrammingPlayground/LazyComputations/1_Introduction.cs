﻿using LaYumba.Functional;
using static LaYumba.Functional.F;

namespace FunctionalProgrammingPlayground.LazyComputations;
public static class Introduction
{
    public static void Run()
    {
        // # Lazy computations: an introduction

        var rand = new Random();
        T PickWithValues<T>(T l, T r) => 
            rand.NextDouble() < 0.5 ? l : r;
        PickWithValues(1 + 2, 3 + 4); // => either 3 or 7

        // C# evaluates both the first (1 + 2)
        // and the second computation (3 + 4) above,
        // even though only one is necessary
        // to get the result from Pick.

        // This is called eager evaluation.
        // The opposite is called lazy evaluation.

        // In this case, that's not really a big problem,
        // because the computation is cheap enough.
        // Once it gets more expensive,
        // it becomes imperative to avoid it.

        // You could do this be replacing the values
        // with functions that procude said values:
        T PickWithFuncs<T>(Func<T> l, Func<T> r) => 
            (rand.NextDouble() < 0.5 ? l : r)();
        PickWithFuncs(() => 1 + 2, () => 3 + 4);
        // This function first executes the ternary operator,
        // and then executes the Func associated with the choice.

        // If you're not sure whether a value will be required
        // and it may be expensive to compute it,
        // pass the value lazily by wrapping it in a function
        // that computes the value.

        // ## Lazy APIs with Options

        // OrElse provides a fallback
        // in case the Option is None.
        // It then returns a different Option.
        var orElse = Some(123);
        orElse.OrElse(Some(456));
        // This example is a bit nonsensical, of course.

        // But imagine this scenario:
        // First, look in the cache to see if the value exists.
        // If so, return the value.
        // If not, go to the database
        // and try to retrieve the value over there.

        // In that scenario, you'd want the second Option
        // to be computed lazily. After all,
        // you don't want to go to the database
        // if the value is already in the cache!

        // Compute the second Option lazily
        // by wrapping it into a Func:
        var lazyOrElse = Some(123);
        lazyOrElse.OrElse(() => Some(456));

        // As a guideluine,
        // when a function might not use
        // some of its arguments,
        // those arguments should be specified
        // as lazy computations.

        // ## Funcs as functors

        // A function can be composed as a functor.
        
        // As you recall, a functor is a wrapper
        // that implements a Map function.
        // The general signature of that function is
        // Map: (C<T>, (T -> R)) -> C<R>.
        
        // Applied to a Func, this turns into:
        // Map: (Func<T>, (T -> R)) -> Func<R>.

        // An example:
        var lazyGrandma = () => "grandma";
        var turnBlue = (string s) => $"blue {s}";
        var lazyGrandmaBlue = lazyGrandma.Map(turnBlue);
        var grandmaBlue = lazyGrandmaBlue(); // => "blue grandma"
        // The first three statements are not evaluated immediately,
        // hence their characterisation as "lazy".
        // They are only evaluated when the function is actually executed.

        // This means that you can build up complex logic
        // without executing anything until you decide to fire things off.

        // ## Funcs as monads

        // A function can also be composed as a monad.

        // A monad is a wrapper that implements a Bind function:
        // Bind: (C<T>, (T -> C<R>)) -> C<R>.

        // Applied to a Func, this turns into:
        // Bind: (Func<T>, T -> Func<R>)) -> Func<R>

        // An example:
        var turnGreen = (string s) => () => $"green {s}";
        var lazyGrandmaGreen = lazyGrandma.Bind(turnGreen);
        var grandmaGreen = lazyGrandmaGreen();

        // This means that you can chain Funcs
        // the same way you can chain any monad.
    }
}