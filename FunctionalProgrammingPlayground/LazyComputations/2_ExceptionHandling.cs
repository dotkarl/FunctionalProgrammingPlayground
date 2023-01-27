using LaYumba.Functional;
using System.Text.Json;
using static LaYumba.Functional.F;


namespace FunctionalProgrammingPlayground.LazyComputations;
public static class ExceptionHandling
{
    public static void Run()
    {
        // # Exception handling with Try

        // You can abstract the details of error handling away
        // using lazy computations.

        // An example:
        Try<Uri> CreateUri(string uri) => () => new Uri(uri);
        CreateUri("http://github.com").Run();
        // => Success(http://github.com)
        CreateUri("rubbish").Run();
        // => Exception(Invalid URI: the format of the URI could not be...)

        // Why does this work?

        // Try is a delegate representing an operation
        // that may throw an exception.

        // A delegate is a type that represents references to methods
        // with a particular parameter list and return type.
        // When you instantiate a delegate,
        // you can associate its instance with any method
        // with a compatible signature and return type.
        // You can invoke (or call) the method through the delegate instance.
        // (source: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/)

        // Try accepts a T, which in this case is a Func<Uri>.
        // Instantiating a new Uri may cause an Exception.
        // Try catches that Exception, wrapping it in an Exceptional monad.
        // So CreateUri returns a monad containing either a Uri, or the Exception.

        // Run is an extension method on Try.
        // It handles the try-catch block for you.
        // By invoking a Func (qua T) via Try<T>'s Run extension,
        // you won't have to worry about catching Exceptions anymore.

        // There also exists a shorthand notation for Try:
        Try(() => new Uri("http://google.com")).Run();
        // => Success(http://google.com)

        // ## Example: extracting JSON safely

        // Let's say, you have a piece of JSON:
        var json = @"
        {
            ""Name"": ""github"",
            ""Uri"": ""http://github.com""
        }";

        // This would be an unsafe way to construct a URI form that object:
        Uri ExtractUriUnsafe(string json)
        {
            var website = JsonSerializer.Deserialize<Website>(json); // Could throw Exception
            return new Uri(website.Uri); // Could throw Exception
        }

        // Let's use Try to make a safe implementation.
        // We'll reuse the CreateUri funciton defined above.
        Try<T> Parse<T>(string s) => () => JsonSerializer.Deserialize<T>(s);

        // An implementation using Bind looks thus:
        Try<Uri> ExtractUriSafeWithBind(string json) =>
            Parse<Website>(json)
                .Bind(website => CreateUri(website.Uri));

        // An implementation using LINQ looks thus:
        Try<Uri> ExtractUriSafeWithLINQ(string json) =>
            from website in Parse<Website>(json)
            from uri in CreateUri(website.Uri)
            select uri;

        // Try it out!
        Console.WriteLine(ExtractUriSafeWithLINQ(json).Run());
        Console.WriteLine(ExtractUriSafeWithLINQ("blah").Run());
        Console.WriteLine(ExtractUriSafeWithLINQ("{}").Run());
        Console.WriteLine(ExtractUriSafeWithLINQ(@"
            {
                ""Name"": ""Github"",
                ""Uri"": ""rubbish""
            }").Run());

    }

    record Website(string Name, string Uri);
}
