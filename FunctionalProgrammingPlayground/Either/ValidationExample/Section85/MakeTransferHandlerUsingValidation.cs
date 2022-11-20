using LaYumba.Functional;
using System.Text.RegularExpressions;
using Unit = System.ValueTuple;
using static LaYumba.Functional.F;

namespace FunctionalProgrammingPlayground.Either.ValidationExample.Section85;
internal class MakeTransferHandlerUsingValidation
{
    // The return type of Validation<Exceptional<Unit>> denotes
    // possible technical errors down the line, wrapped by
    // business rule violations earlier in the process
    public Validation<Exceptional<Unit>> Handle(MakeTransfer transfer) =>
        Validate(transfer)
        .Map(Save);

    // The return type of Validation<T> denotes
    // possible business rule violations
    Validation<MakeTransfer> Validate(MakeTransfer transfer) =>
        Valid(transfer)
        .Bind(ValidateBic)
        .Bind(ValidateDate);

    readonly Regex bicRegex = new Regex("[A-Z]{11}");
    DateTime now;
    Validation<MakeTransfer> ValidateBic(MakeTransfer transfer) =>
        bicRegex.IsMatch(transfer.Bic)
            ? transfer
            : Errors.InvalidBic;

    Validation<MakeTransfer> ValidateDate(MakeTransfer transfer) =>
        transfer.Date.Date > now.Date
            ? transfer
            : Errors.TransferDateIsPast;

    // The return type of Exceptional denotes
    // possible unexpected technical errors
    Exceptional<Unit> Save(MakeTransfer transfer)
    {
        try
        {
            // Save logic
        }
        catch (Exception ex)
        {
            return ex;
        }
        return Unit();
    }

}
