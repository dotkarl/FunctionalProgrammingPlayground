using Microsoft.AspNetCore.Mvc;
using Unit = System.ValueTuple;

namespace FunctionalProgrammingPlayground.Either.ValidationExample.Section84;
public class MakeTransferResultController : ControllerBase
{
    // This always returns an HTTP 200,
    // either containing the result (a Unit in this case),
    // or an error message.

    // Whether you would want to use this abstraction for your API
    // has less to do with functional programming,
    // and more with API design.
    [HttpPost, Route("api/transfers/future")]
    public ResultDto<Unit> MakeTransfer([FromBody] MakeTransfer transfer) =>
        new MakeTransferHandler()
            .Handle(transfer)
            .ToResult();
}
