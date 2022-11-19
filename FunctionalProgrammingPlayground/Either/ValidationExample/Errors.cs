using LaYumba.Functional;

namespace FunctionalProgrammingPlayground.Either.ValidationExample;
public static class Errors
{
    public static Error InvalidBic => new InvalidBicError();
    public static Error TransferDateIsPast => new TransferDateIsPastError();
}

public sealed record InvalidBicError() : 
    Error("The beneficiary's BIC/SWIFT code is invalid");

public sealed record TransferDateIsPastError() :
    Error("Transfer date cannot be in the past");
