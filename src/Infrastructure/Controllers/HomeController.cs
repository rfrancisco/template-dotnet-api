using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ProjectRootNamespace.Api.Infrastructure;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjectRootNamespace.Api.Controllers
{
    [AllowAnonymous]
    [Route("")]
    public class HomeController : BaseController
    {
        protected readonly IHostEnvironment _environment;
        protected readonly Settings _settings;

        public HomeController(
            IOptions<Settings> settings,
            IHostEnvironment environment)
        {
            _settings = settings.Value;
            _environment = environment;
        }

        /// <summary>
        /// Get the index page for this component
        /// </summary>
        /// <response code="200">Returns component information.</response>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Component" })]
        public virtual ContentResult GetIndex()
        {
            var title = ComponentInfo.Title;
            var environment = _environment.EnvironmentName;

            var view = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <title>{title}</title>
                    <style>
                        body {{ font-family: arial,sans-serif; font-size: 13px; }}
                    </style>
                </head>
                <body>
                    <h3>{title}</h3>
                </body>
                </html>";

            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = view
            };
        }

        /// <summary>
        /// Get the version of this component
        /// </summary>
        /// <response code="200">Returns component version.</response>
        [HttpGet("version")]
        [SwaggerOperation(Tags = new[] { "Component" })]
        public string GetVersion()
        {
            return _settings.Version;
        }
    }
}