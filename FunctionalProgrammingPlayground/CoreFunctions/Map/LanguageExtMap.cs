using LanguageExt;
using static LanguageExt.Prelude;

namespace FunctionalProgrammingPlayground.CoreFunctions.Map;
public class LanguageExtMap
{
    record Apples();
    record ApplePie(Apples Apples);

    public static void Run()
    {
        // Option example: greet
        var greet = (string name) => $"hello, {name}";

        Option<string> empty = None;
        Option<string> optJohn = Some("John");

        var emptyGreet = empty.Map(greet);
        var johnGreet = optJohn.Map(greet);

        // Note that these Matches have two Actions as parameters
        emptyGreet.Match(
            (greet) => Console.WriteLine($"{nameof(emptyGreet)}: {greet}"),
            () => Console.WriteLine($"{nameof(emptyGreet)}: None"));
        johnGreet.Match(
            (greet) => Console.WriteLine($"{nameof(johnGreet)}: {greet}"),
            () => Console.WriteLine($"{nameof(johnGreet)}: None"));
        
        // An alternative would be as follows:
        Console.WriteLine(emptyGreet.Match(
            (greet) => $"{nameof(emptyGreet)}: {greet}",
            () => $"{nameof(emptyGreet)}: None"));
        Console.WriteLine(johnGreet.Match(
            (greet) => $"{nameof(johnGreet)}: {greet}",
            () => $"{nameof(johnGreet)}: None"));

        // In this case, that would definitely be the better option (no pun intended).
        // I still struggle in applying the Match function, though.
        // It requires you as a developer to think functionally, its signature forces the functional paradigm onto you, as it were:
        // "If Some, do this; if None, do that" (where "do" means "execute this Action/Func").
        // My gut reaction, however, is to treat it as null, i.e. extract it to a variable and then check if it is Some or None.
        // I guess this will fade once you work with Options more often.


        // Option example: makePie
        var makePie = (Apples apples) => new ApplePie(apples);

        Option<Apples> fullBasket = Some(new Apples());
        Option<Apples> emptyBasket = None;

        var applePie = fullBasket.Map(makePie);
        var noApplePie = emptyBasket.Map(makePie);

        Console.WriteLine(applePie.Match(
            (applePie) => $"{nameof(applePie)}: I made applePie",
            () => $"{nameof(applePie)}: None"));
        Console.WriteLine(noApplePie.Match(
            (applePie) => $"{nameof(noApplePie)}: I made applePie",
            () => $"{nameof(noApplePie)}: None"));

        // I'm familiar with the Select method of LINQ, where I usually declare the Func inline.
        // Therefore I find it much more intuitive to read that Func inline:
        _ = optJohn.Map(name => $"hello, {name}");
        _ = fullBasket.Map(apples => new ApplePie(apples));

        // The idea that you can declare a function in a variable still seems a bit foreign to me.
        // I have this mental image of variables representing THINGS, data, e.g. an instance of a class or some primitive.
        // But variables can also represent ACTIONS, transformations, e.g. an Action or a Func.
        // Even though I've used this technique several times while coding, it still feels awkward somehow.
    }
}
