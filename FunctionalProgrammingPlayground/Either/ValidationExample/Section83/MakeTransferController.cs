using Microsoft.AspNetCore.Mvc;

namespace FunctionalProgrammingPlayground.Either.ValidationExample.Section83;
public class MakeTransferController : ControllerBase
{
    [HttpPost, Route("transfers/book")]
    public void MakeTransfer([FromBody] MakeTransfer request) => 
        new MakeTransferHandler().Handle(request);

}
