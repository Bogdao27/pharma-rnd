using Microsoft.AspNetCore.Mvc;
using PharmaRnd.API.Response;

namespace PharmaRnd.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ApplicationController : ControllerBase
    {
        public override OkObjectResult Ok(object? value)
        {
            var envelope = Envelope.Ok(value);

            return new(envelope);
        }
    }
}
