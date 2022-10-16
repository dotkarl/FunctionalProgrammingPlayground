# On the 'Bind' function


Take a look at the following code:


```cs
Console.WriteLine("Please enter your age");
string input = Console.ReadLine();

Option<int> ageInt = Int.Parse(input);
Option<Option<int>> age = ageInt.Map(i => Age.Create(i));
```

As you can imagine, working with a type of Option<Option<int>> is unneccessarily complex. In such a case, instead of using Map, you would prefer a method named Bind. In the case of Options, it has the following signature:


`Option.Bind: (Option<T>, T -> Option<R>)) -> Option<R>`


Bind takes an Option and an Option-returning function and applies the function to the inner value of the Option. It is simmilar to the Map function, but instead of accepting a regular function as it second parameter, it takes an Option-returning function.


Essentially, Bind flattens an Option<Option<T>> to an Option<T>. It's comparable to IEnumerable.SelectMany. As a matter of fact, as applied to lists, Bind and SelectMany are identical.


## Monads


The signature of Bind is always in this form (where C stands for some kind of container):


`Bind: (C<T>, (T -> C<R>)) -> C<R>`


*Monads* are types for which a Bind function is defined, enabling you to effectively combine two (or more) monad-returning functions without ending up with a nested structure.


In addition to the Bind function, monads must also have a Return function that *lifts* or *wraps* a normal value T into a monadic value C<T>. Note, however, that this function can take on a different name than "Return". For Options, it is called "Some". The signature of Return is as follows where C stands for some kind of container):


`Return: T -> C<T>` 


In summary, a Monad is a type M<T> for which the following functions are defined:


`Return: T -> M<T>`
`Bind: (M<T>, (T -> M<R>)) -> M<R>`


## Functors and monads


Types *can* be functors and monads at the same time. Option and IEnumerable fall into this category.


Is every monad also a functor. Yes. Map can be implemented using Bind and Return.


Is every functor also a monad? No. Bind cannot be defined in terms of Map.
