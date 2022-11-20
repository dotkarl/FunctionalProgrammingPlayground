# On Either


Either is a monad that indicates: the return type could either be *this* or *that*. The code could go either of two ways: left or right.


This type (or a specialized version of it) is ideal for error handling.


Typical object oriented error handling using Exceptions disrupts the normal program flow, introducing side effects. Any method in C# that throws Exceptions is dishonest by default, since the Exception is never part of the method signature. This makes the code more difficult to maintain and reason about. The only way to analyze the implications of the Exception, is to follow all possible code paths into the function and then look for the first exception handler up the stack. (Imagine doing this for a method that is called hundreds of times!)


Functional error handling makes the error part of the return type. It signals: errors are part of the normal flow of execution, it clearly communicates intent. This is an honest way of telling the reader of your code what could happen. 
