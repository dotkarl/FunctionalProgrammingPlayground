using Microsoft.AspNetCore.Mvc;

namespace FunctionalProgrammingPlayground.Either.ValidationExample.Section84;
internal class MakeTransferStatusCodeController : ControllerBase
{
    // When valiation fails, return a BadRequest,
    // otherwise, return an OK.
    // This is a rather controversial choice,
    // as BadRequest is more often used for syntactical errors,
    // not semantic ones.

    // Whether you would want to "lower" validation failures
    // to BadRequest statuscodes for your API
    // has less to do with functional programming,
    // and more with API design.
    [HttpPost, Route("api/transfers/future")]
    public IActionResult MakeTransfer([FromBody] MakeTransfer transfer) =>
        new MakeTransferHandler()
            .Handle(transfer)
            .Match<IActionResult>(
                _ => BadRequest(),
                _ => Ok());
}
