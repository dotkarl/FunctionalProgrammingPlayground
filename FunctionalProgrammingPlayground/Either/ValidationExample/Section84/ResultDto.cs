using LaYumba.Functional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgrammingPlayground.Either.ValidationExample.Section84;
public record ResultDto<T>
{
    public bool Succeeded { get; }
    public bool Failed => !Succeeded;

    public T Data { get; }
    public Error Error { get; }

    internal ResultDto(T data) => (Succeeded, Data) = (true, data);
    internal ResultDto(Error error) => Error = error;
}

public static class EitherExtensions
{
    public static ResultDto<T> ToResult<T>(this Either<Error, T> either) =>
        either.Match(
            error => new ResultDto<T>(error),
            data => new ResultDto<T>(data));
}
