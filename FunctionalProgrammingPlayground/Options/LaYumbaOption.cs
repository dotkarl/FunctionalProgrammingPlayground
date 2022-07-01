using LaYumba.Functional;
// Note that these functions are contained in a static class!
using static LaYumba.Functional.F;

namespace FunctionalProgrammingPlayground.Options
{
    public class LaYumbaOption
    {
        public static IEnumerable<string> Run()
        {            
            Option<string> none1 = None;

            Option<string> some1 = Some("some");

            yield return Greet(none1);
            yield return Greet(some1);
        }

        private static string Greet(Option<string> greetee) 
            => greetee.Match
            (
                // First, match None
                // Then, match Some
                () => "Sorry, who?",
                (name) => $"Hello, {name}"
            );

        // This is precisely the opposite of the way it is in LanguageExt
    }
}
