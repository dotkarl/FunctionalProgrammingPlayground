# On the 'ForEach' function


## List.ForEach


List already defines a `ForEach`-method. (Note that I call this a "method", not a "function!") `ForEach` is a method used to perform a side effect on the contents of a `List`.


## `ForEach` and `Map`


`ForEach` is similar to `Map` (or `Select`) in that it applies its parameter function (an `Action` in the case of the former, a `Func` in the case on the latter) to each inner value.


The difference between the two lies in the fact that `Map` transforms the inner value. `ForEach` applies a side effect on the inner value, but does not return anything. Because of this, `ForEach` should always be called last when chaining the core functions, or rather: it has to be called last.


## Separate side effects from pure logic


In functional programming, it is a good practice to separate pure logic from side effects. So don't write: `opt.ForEach(name => Console.WriteLine($"Hello, {name}"));`. This line performs two operations: (1) transform the inner value (from *name* to "Hello, {*name*}", and (2) write it to the console.


Instead write: `opt.Map(name => "$Hello, {name}").ForEach(Console.WriteLine);`. This clearly separates out the two responsiblities. It also makes it a lot easier to plug in some other operation, e.g.:

```
opt.Map(String.ToUpper)
    .Map(name => "Hello, {name}")
    .ForEach(Console.WriteLine);
```
