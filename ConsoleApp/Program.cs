using FunctionalProgrammingPlayground.Options;

var result = LanguageExtOption.Run();
foreach (var greeting in result)
{
    Console.WriteLine(greeting);
}
