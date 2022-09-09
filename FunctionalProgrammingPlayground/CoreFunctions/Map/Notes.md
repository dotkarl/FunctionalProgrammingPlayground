# On the `Map` function


## Similar idea: `Select` for `IEnumerable`


In the context of `IEnumerable`, `Map` maps a list of `T`'s to a list of `R`'s by applying a function `T -> R` to each element in the source list. In LINQ, this method is called `Select`.


## `Map` in the context of `Option`


In the context of `Option`, `Map` maps the Option of `T` to an Option of `R` by applying a function `T -> R` to the inner (or bound) value. Or, in functional notation: 


`Option<T>, (T -> R)) -> Option<R>`.


This is a possible implementation:


```cs
public static Option<R> Map<T, R>(this Option<T> optT, Func<T, R> f) =>
    optT.Match(
        () => None,
        (t) => Some(f(t))
    );
```


## `Map` in general


The signature of the `Map` function can be generalized even further. After all, this kind of behaviour is not exclusive to `IEnumerable` or `Option`. It can be applied to all kinds of container types that wrap some inner value. In functional notation, that would look like this: 


`Map: (C<T>, (T -> R)) -> C<R>`


`Map` can be defined as a function that takes a container `C<T>` and a function *f* of type `(T -> R)`. It returns a container `C<R>` that wraps the value(s) resulting from applying *f* to the container's inner value(s).


Types that implement a `Map` function are called *functors*.