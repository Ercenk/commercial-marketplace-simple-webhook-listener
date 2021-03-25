
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : RecordingControllerBase
    {
        public ResourceController(ILoggerFactory loggerFactory) : base(loggerFactory, "ManagedApp")
        {
        }

    }
}
