using LanguageExt;
using static LanguageExt.Prelude;
using Pet = System.String;

namespace FunctionalProgrammingPlayground.CoreFunctions.Bind;
internal class LanguageExtBind
{
    public static void Run()
    {
        // parseAge combines the check that the string represents a valid integer
        // and the check that the integer is a valid age value
        Func<string, Option<Age2>> parseAge = s => Some(int.Parse(s)).Bind(Age2.Create);

        parseAge("26");       // => Some(26)
        parseAge("notAnAge"); // => None
        parseAge("180");      // => None

        //---

        Console.WriteLine($"Only {ReadAge()}! That's young!");

        // The only difference between the LaYumba implementation and the LanguageExt implemenation
        // is that the two parameters are reversed: Some first, None second
        static Age2 ReadAge() =>
            ParseAge(Prompt("Please enter your age")).Match(
                (age) => age,
                () => ReadAge());

        static Option<Age2> ParseAge(string s) =>
            Some(int.Parse(s)).Bind(Age2.Create);

        static string Prompt(string msg)
        {
            Console.WriteLine(msg);
            return Console.ReadLine();
        }

        //---

        var neighbors = new Neighbor2[]
        {
            new ("John", new [] {"Fluffy", "Thor"}),
            new ("Tim", System.Array.Empty<Pet>()),
            new ("Carl", new [] {"Sybill"})
        };

        var nested = neighbors.Map(n => n.Pets); // => [["Fluffy", "Thor"], [], ["Sybill"]
        var flat = neighbors.Bind(n => n.Pets);  // => ["Fluffy", "Thor", "Sybill"]
    }
}

record Neighbor2(string Name, IEnumerable<Pet> Pets);

public struct Age2
{
    private int Value { get; }

    public static Option<Age2> Create(int age) =>
        IsValid(age) ? Some(new Age2(age)) : None;

    private Age2(int value) => Value = value;

    private static bool IsValid(int age) =>
        0 <= age && age < 120;

    public override string ToString()
    {
        return Value.ToString();
    }
}
