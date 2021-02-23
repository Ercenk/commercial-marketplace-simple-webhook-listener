using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
