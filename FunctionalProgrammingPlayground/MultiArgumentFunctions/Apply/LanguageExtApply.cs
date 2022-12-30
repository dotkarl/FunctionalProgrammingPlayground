using static LanguageExt.Prelude;

namespace FunctionalProgrammingPlayground.MultiArgumentFunctions.Apply;
internal class LanguageExtApply
{
    public static void Run()
    {
        var multiply = (int x) => (int y) => x * y;
        var multiplyBy3 = Some(3).Map(multiply);

        var twelve = Some(3).Map(multiply).Apply(Some(4)); // => Some(12)
        var none = Some(3).Map(multiply).Apply(None); // => None
    }
}
