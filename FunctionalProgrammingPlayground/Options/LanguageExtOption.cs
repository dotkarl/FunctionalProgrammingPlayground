using LanguageExt;
// From the documentation (https://louthy.github.io/language-ext/LanguageExt.Core/Prelude/): 
// "Because it's so fundamental, you'll want to add this to the top of every code file:"
using static LanguageExt.Prelude;
// Of course, starting from C# 10, you could use a global using directive for this

namespace FunctionalProgrammingPlayground.Options
{
    public static class LanguageExtOption
    {
        public static IEnumerable<string> Run()
        {
            // Different ways of initializing Options / Some / None

            // None
            Option<string> none1 = new OptionNone();
            Option<string> none2 = OptionNone.Default;
            Option<string> none3 = Option<string>.None;
            // This is the preferred way, it uses the static LanguageExt.Prelude above
            Option<string> none4 = None; 

            // Some
            Option<string> some1 = new Some<string>("some1");
            Option<string> some2 = Option<string>.Some("some2");
            // This is the preferred way, it uses the static LanguageExt.Prelude above
            Option<string> some3 = Some("some3");

            yield return Greet(none1);
            yield return Greet(none2);
            yield return Greet(none3);
            yield return Greet(none4);
            yield return Greet(some1);
            yield return Greet(some2);
            yield return Greet(some3);
        }

        private static string Greet(Option<string> greetee) => greetee.Match(
            // First, match Some
            // Then, match None
            (name) => $"Hello, {name}",
            () => "Sorry, who?");

        // This is precisely the opposite of the way it is in LaYumba
    }
}
