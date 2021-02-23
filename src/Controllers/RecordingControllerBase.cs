using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.Controllers
{
    public abstract class RecordingControllerBase : ControllerBase
    {
        private ILogger logger;

        protected RecordingControllerBase(ILoggerFactory loggerFactory, string loggerName)
        {
            this.logger = loggerFactory.CreateLogger(loggerName);
        }



        [HttpPost]
        public async Task<IActionResult> Post()
        {
            await this.ReadReqestAndLogAsync(MethodBase.GetCurrentMethod());
            return this.Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put()
        {
            await this.ReadReqestAndLogAsync(MethodBase.GetCurrentMethod());
            return this.Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            await this.ReadReqestAndLogAsync(MethodBase.GetCurrentMethod());
            return this.Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await this.ReadReqestAndLogAsync(MethodBase.GetCurrentMethod());
            return this.Ok();
        }

        protected async Task ReadReqestAndLogAsync(MethodBase methodBase)
        {
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var payload = await reader.ReadToEndAsync();
                this.logger.LogInformation($"{methodBase.Name}: {payload}");
            }

        }
    }
}
