using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ProjectRootNamespace.Api.Infrastructure.Extensions
{
    /// <summary>
    /// Extension methods for setting up custom exception handler middleware services.
    /// </summary>
    public static class ExceptionHandlingServiceCollectionExtensions
    {
        /// <summary>
        /// Note: This needs to be called after 'services.AddControllers()'
        /// </summary>
        public static void AddCustomExceptionHandler(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    // Ensure that model validation errors return the same type of messages as the middleware
                    var details = new ApiModelValidationException(actionContext.ModelState);
                    return new BadRequestObjectResult(details.ToString(true));
                };
            });

        }
    }

    public static class ExceptionHandlingApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }

    }

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IHostEnvironment environment)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory?.CreateLogger<ExceptionHandlingMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
            _environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
                if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                    await context.Response.WriteAsync(new ApiSecurityException(ApiSecurityErrors.UnauthorizedError, null).ToString());
                if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
                    await context.Response.WriteAsync(new ApiSecurityException(ApiSecurityErrors.ForbiddenError).ToString());
            }
            catch (ApiException ex)

            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = ex.StatusCode;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(ex.ToString(_environment.IsDevelopment()));

                return;
            }
        }
    }
}