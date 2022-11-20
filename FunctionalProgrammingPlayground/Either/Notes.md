# On Either


Either is a monad that indicates: the return type could either be *this* or *that*. The code could go either of two ways: left or right.


This type (or a specialized version of it) is ideal for error handling. Specialized versions in [LanguageExt](https://github.com/louthy/language-ext) include: 


- [`Try`](https://louthy.github.io/language-ext/LanguageExt.Core/Monads/Alternative%20Value%20Monads/Try/Try/index.html): used to capture Exceptions;

- [`Validation`](https://louthy.github.io/language-ext/LanguageExt.Core/Monads/Alternative%20Value%20Monads/Validation/index.html): used to capture failed business validation. 


Typical object oriented error handling using Exceptions disrupts the normal program flow, introducing side effects. Any method in C# that throws Exceptions is dishonest by default, since the Exception is never part of the method signature. This makes the code more difficult to maintain and reason about. The only way to analyze the implications of the Exception, is to follow all possible code paths into the function and then look for the first exception handler up the stack. (Imagine doing this for a method that is called hundreds of times!)


Functional error handling makes the error part of the return type. It signals: errors are part of the normal flow of execution, it clearly communicates intent. This is an honest way of telling the reader of your code what could happen. 
