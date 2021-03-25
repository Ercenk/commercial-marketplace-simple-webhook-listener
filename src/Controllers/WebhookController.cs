using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookController : RecordingControllerBase
    {
        public WebhookController(ILoggerFactory loggerFactory) : base(loggerFactory, "WebHook")
        {

        }


        public override Task<IActionResult> Post()
        {
            StringValues authorizationHeaderValues;

            if (!Request.Headers.TryGetValue("Authorization", out authorizationHeaderValues))
            {
                // No authorization header received.
                // Webhook returns do not matter.
                return base.Post();
            }

            var authorizationToken = authorizationHeaderValues.ToString().Split(' ')[1];

            try
            {
                var token = ValidateToken(authorizationToken);
            }
            catch(Exception e)
            {
                // Token is not valid
                throw new ApplicationException("Token is not valid!");
            }

            return base.Post();
        }

        static JwtSecurityToken ValidateToken(string token)
        {
            string discoveryEndpoint = "https://login.microsoftonline.com/common/v2.0/.well-known/openid-configuration";

            var httpClient = new HttpClient
            {
                MaxResponseContentBufferSize = 1024 * 1024 * 10 // 10 MB
            };

            var configManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                discoveryEndpoint, 
                new OpenIdConnectConfigurationRetriever(),
                new HttpDocumentRetriever(httpClient) { RequireHttps = true });

            var config = configManager.GetConfigurationAsync().Result;

            var tenantId = "6ebb869a-f2fc-455f-b3c3-c82173d556ea";
            var appId = "5f06f0a5-a804-4f80-a6da-4c07f437f1ae";

            var validationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                // this is my client ID recorded on the Technical Configuration page on Partner Center
                ValidAudience = appId,
                ValidateIssuer = true,
                ValidIssuer = $"https://sts.windows.net/{tenantId}/",
                IssuerSigningKeys = config.SigningKeys,
                ValidateLifetime = false
            };

            JwtSecurityTokenHandler tokendHandler = new JwtSecurityTokenHandler();

            SecurityToken jwt;

            var result = tokendHandler.ValidateToken(token, validationParameters, out jwt);

            return jwt as JwtSecurityToken;
        }
    }
}
