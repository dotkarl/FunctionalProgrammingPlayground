using LaYumba.Functional;
using System.Text.RegularExpressions;
using Unit = System.ValueTuple;
using static LaYumba.Functional.F;

namespace FunctionalProgrammingPlayground.Either.ValidationExample;
internal class MakeTransferHandler
{
    public Either<Error, Unit> Handle(MakeTransfer cmd) =>
    Validate(cmd)
    .Bind(Save);

    Either<Error, MakeTransfer> Validate(MakeTransfer cmd) =>
        Right(cmd)
        .Bind(ValidateBic)
        .Bind(ValidateDate);

    readonly Regex bicRegex = new Regex("[A-Z]{11}");
    DateTime now;
    Either<Error, MakeTransfer> ValidateBic(MakeTransfer transfer) =>
        bicRegex.IsMatch(transfer.Bic)
            ? transfer
            : Errors.InvalidBic;

    Either<Error, MakeTransfer> ValidateDate(MakeTransfer transfer) =>
        transfer.Date.Date > now.Date
            ? transfer
            : Errors.TransferDateIsPast;


    Either<Error, Unit> Save(MakeTransfer cmd) =>
        default; //TODO

}
