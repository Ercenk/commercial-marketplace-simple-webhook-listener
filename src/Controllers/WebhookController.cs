using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookController : RecordingControllerBase
    {
        public WebhookController(ILoggerFactory loggerFactory) : base(loggerFactory, "WebHook")
        {

        }
    }
}
