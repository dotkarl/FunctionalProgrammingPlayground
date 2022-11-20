using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FunctionalProgrammingPlayground.Either.ValidationExample.Section85;
public class MakeTransferController : ControllerBase
{
    ILogger<MakeTransferController> logger;

    [HttpPost, Route("api/transfers/book")]
    public IActionResult MakeTransfer([FromBody] MakeTransfer transfer) =>
        new MakeTransferHandlerUsingValidation()
            .Handle(transfer)
            .Match(
                BadRequest, // Validation failed
                result => result.Match(
                    OnFaulted, // An exception was thrown
                    _ => Ok()
                    ));
    // Note that the return type of Handle is Validation<Exceptional<Unit>>;
    // this can be read from left to right to indicate the possible outcomes:
    // - Validation failure
    // - Exception thrown
    // - Correct result
    // The corresponding code should be read in the same vein.

    IActionResult OnFaulted(Exception ex)
    {
        logger.LogError(ex.Message);
        return StatusCode(500, Errors.UnexpectedError);
    }
}
