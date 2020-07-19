using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace projectRootNamespace.Api.Infrastructure.Startup
{
    /// <summary>
    /// Extension methods for setting up swagger services.
    /// </summary>
    public static class SwaggerServiceCollectionExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = ComponentInfo.Title, Version = "v1" });
                var basePath = AppContext.BaseDirectory;
                var xmlFile = (new Program()).GetType().Assembly.ManifestModule.Name.Replace(".dll", ".xml");
                var xmlPath = Path.Combine(basePath, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.EnableAnnotations();
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }
    }

    public static class SwaggerApplicationBuilderExtensions
    {
        public static void UseSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger(c => c.RouteTemplate = "docs/{documentName}/swagger.json");

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("v1/swagger.json", ComponentInfo.Title);
                options.DocumentTitle = ComponentInfo.Title;
                options.RoutePrefix = "docs";
                // Apply cusyom styles
                options.InjectStylesheet("custom.css");
                options.InjectJavascript("custom.js");
            });
        }
    }

    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var requiresAuthorization = context.MethodInfo.DeclaringType
                .GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<AuthorizeAttribute>()
                .Any();
            var allowAnonymous = context.MethodInfo.DeclaringType
                .GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<AllowAnonymousAttribute>()
                .Any();

            if (requiresAuthorization && !allowAnonymous)
            {
                if (!operation.Responses.ContainsKey("401"))
                    operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized :)" });
                if (!operation.Responses.ContainsKey("403"))
                    operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden :)" });

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                            new List<string>()
                        }
                    }
                };
            }
        }
    }
}
