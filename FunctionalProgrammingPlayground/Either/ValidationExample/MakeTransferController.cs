using LaYumba.Functional;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Unit = System.ValueType;
using static LaYumba.Functional.F;

namespace FunctionalProgrammingPlayground.Either.ValidationExample;
public class MakeTransferController : ControllerBase
{
    [HttpPost, Route("transfers/book")]
    public void MakeTransfer([FromBody] MakeTransfer request) => 
        Handle(request);

    Either<Error, Unit> Handle(MakeTransfer cmd) =>
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
