using LaYumba.Functional;
using static LaYumba.Functional.F;

namespace FunctionalProgrammingPlayground.CoreFunctions.Map;
public class LaYumbaMap
{
    record Apples();
    record ApplePie(Apples Apples);

    public static void Run()
    {
        // IEnmerable example
        var triple = (int x) => x * 3;
        var result = Enumerable.Range(1, 3).Map(triple);
        foreach (var item in result)
        {
            Console.WriteLine(item);
        }

        // Option example: greet
        var greet = (string name) => $"hello, {name}";

        Option<string> empty = None;
        Option<string> optJohn = Some("John");

        var emptyGreet = empty.Map(greet); // => None
        var johnGreet = optJohn.Map(greet); // => Some("hello, John")

        emptyGreet.Match(
            () => Console.WriteLine($"{nameof(emptyGreet)}: None"),
            (greet) => Console.WriteLine($"{nameof(emptyGreet)}: {greet}"));
        johnGreet.Match(
            () => Console.WriteLine($"{nameof(johnGreet)}: None"),
            (greet) => Console.WriteLine($"{nameof(johnGreet)}: {greet}"));

        // Option example: makePie
        var makePie = (Apples apples) => new ApplePie(apples);

        Option<Apples> fullBasket = Some(new Apples());
        Option<Apples> emptyBasket = None;

        var applePie = fullBasket.Map(makePie); // => Some(ApplePie);
        var noApplePie = emptyBasket.Map(makePie); // => None

        applePie.Match(
            () => Console.WriteLine($"{nameof(applePie)}: None"),
            (applePie) => Console.WriteLine($"{nameof(applePie)}: ApplePie"));
        noApplePie.Match(
            () => Console.WriteLine($"{nameof(noApplePie)}: None"),
            (applePie) => Console.WriteLine($"{nameof(noApplePie)}: ApplePie"));
    }

}
