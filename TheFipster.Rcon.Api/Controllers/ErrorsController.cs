using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TheFipster.Rcon.Api.Exceptions;
using TheFipster.Rcon.Api.Models;

namespace TheFipster.Rcon.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public ErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;
            var code = 500;

            if (exception is RconHostException) code = 502;

            Response.StatusCode = code;
            return new ErrorResponse(exception);
        }
    }
}
