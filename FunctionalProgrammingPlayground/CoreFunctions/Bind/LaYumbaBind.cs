using LaYumba.Functional;
using static LaYumba.Functional.F;
using Pet = System.String;

namespace FunctionalProgrammingPlayground.CoreFunctions.Bind;
public class LaYumbaBind
{
    public static void Run()
    {
        // parseAge combines the check that the string represents a valid integer
        // and the check that the integer is a valid age value
        Func<string, Option<Age>> parseAge = s => Int.Parse(s).Bind(Age.Create);

        parseAge("26");       // => Some(26)
        parseAge("notAnAge"); // => None
        parseAge("180");      // => None

        //---

        Console.WriteLine($"Only {ReadAge()}! That's young!");

        static Age ReadAge() =>
            ParseAge(Prompt("Please enter your age")).Match(
                () => ReadAge(),
                (age) => age);

        static Option<Age> ParseAge(string s) =>
            Int.Parse(s).Bind(Age.Create);

        static string Prompt(string msg)
        {
            Console.WriteLine(msg);
            return Console.ReadLine();
        }

        //---

        var neighbors = new Neighbor[]
        {
            new ("John", new [] {"Fluffy", "Thor"}),
            new ("Tim", Array.Empty<Pet>()),
            new ("Carl", new [] {"Sybill"})
        };

        var nested = neighbors.Map(n => n.Pets); // => [["Fluffy", "Thor"], [], ["Sybill"]
        var flat = neighbors.Bind(n => n.Pets);  // => ["Fluffy", "Thor", "Sybill"]
    }
}

record Neighbor(string Name, IEnumerable<Pet> Pets);

public struct Age
{
    private int Value { get; }

    public static Option<Age> Create(int age) =>
        IsValid(age) ? Some(new Age(age)) : None;

    private Age(int value) => Value = value;

    private static bool IsValid(int age) =>
        0 <= age && age < 120;

    public override string ToString()
    {
        return Value.ToString();
    }
}
