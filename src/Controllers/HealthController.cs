using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjectRootNamespace.Api.Controllers
{
    [Route("health")]
    public class HealthController : Controller
    {
        private readonly HealthCheckService _healthCheckService;

        public HealthController(HealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        /// <summary>
        /// Get component health status
        /// </summary>
        /// <remarks>
        /// Provides an indication about the health of the API.
        /// The HealthChecks API already creates an endpoint for this. This one
        /// was only created because the default one doesn't show up in swagger.
        /// </remarks>
        /// <response code="200">API is healthy</response>
        /// <response code="503">API is unhealthy or in degraded state</response>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Component" })]
        public async Task<IActionResult> Get()
        {
            var report = await _healthCheckService.CheckHealthAsync();
            return report.Status == HealthStatus.Healthy
                ? Ok("Healthy")
                : StatusCode((int)HttpStatusCode.ServiceUnavailable, "Unhealthy");
        }
    }
}