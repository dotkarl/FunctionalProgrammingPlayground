# On partial application


Partial application means: the arguments for a function are provided piecemeal.


You can achieve this in different ways:


- By writing functions in curried form, 
i.e. `T1 -> T2 -> ... -> Tn -> R`.

- By currying functions with `Curry` and then invoking the curried function with subsequent arguments, 
i.e. `((T1, T2) -> R) -> (T1 -> (T2 -> R))` (the arrow outside of the brackets represent the `Curry` function).

- By supplying arguments one by one with `Apply`,