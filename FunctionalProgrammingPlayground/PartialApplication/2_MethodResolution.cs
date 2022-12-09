using Name = System.String;
using Greeting = System.String;
using PersonalizedGreeting = System.String;
using LaYumba.Functional;

namespace FunctionalProgrammingPlayground.PartialApplication;
internal class MethodResolution
{
    // Some problems arise when you rewrite a Func to be a method.
    // Example:
    PersonalizedGreeting GreeterMethod(Greeting gr, Name name) =>
        $"{gr}, {name}";

    // This does not compile:

    // Func<Name, PersonalizedGreeting> GreetWith(Greeting greeting) =>
    //     GreeterMethod.Apply(greeting);

    // GreeterMethod is a MethodGroup; Apply expects a Func.

    // You could rewrite this to either of these two:
    Func<Name, PersonalizedGreeting> GreetWith_1(Greeting greeting) =>
        FuncExt.Apply<Greeting, Name, PersonalizedGreeting>(GreeterMethod, greeting);
    Func<Name, PersonalizedGreeting> GreetWith_2(Greeting greeting) =>
        new Func<Greeting, Name, PersonalizedGreeting>(GreeterMethod).Apply(greeting);
    // This is, to say the least, not great.

    // Perhaps, it is better to avoid using methods.
    // See the options in class TypeInference_Delegate below.

    // Important: if you want to use higher order functions
    // that take multi-argument functions as arguments
    // it's best to move away from using methods and write Funcs instead
    // (or methods that return Funcs).
}

public class TypeInference_Delegate
{
    // 1. Field
    readonly Func<Greeting, Name, PersonalizedGreeting> GreeterField =
        (gr, name) => $"{gr}, {name}";
    // Disadvantage: you can't refer to instance variables,
    //               needs to be marked as readonly

    // 2. Property
    Func<Greeting, Name, PersonalizedGreeting> GreeterProperty =>
        (gr, name) => $"{gr}, {name}";
    // Minor change from using a field; can refer to instance variables

    // 3. Factory method
    Func<Greeting, T, PersonalizedGreeting> GreeterFactory<T>() =>
        (gr, t) => $"{gr}, {t}";
    // This is the most powerful way of creating a Func:
    // define a method whose single job it is to return a Func.
    // This allows you to also use generic parameters.

    public void CallingCodeExamples()
    {
        GreeterField.Apply("Hi");
        GreeterProperty.Apply("Hi");
        GreeterFactory<Name>().Apply("Hi");
    }
}
