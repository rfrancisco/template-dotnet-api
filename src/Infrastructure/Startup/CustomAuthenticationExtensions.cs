using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using projectRootNamespace.Api.Infrastructure.Security;
using projectRootNamespace.Api.Infrastructure.Security.CustomAuthentication;

namespace projectRootNamespace.Api.Infrastructure.Startup
{
    /// <summary>
    /// Extension methods to setup lis integration
    /// </summary>
    public static class CustomAuthenticationServiceCollectionExtensions
    {
        public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("Settings:Authentication:Custom");
            var settings = section.Get<CustomAuthenticationSettings>();
            services.Configure<CustomAuthenticationSettings>(section);

            services.AddScoped<IAuthenticationService, CustomAuthenticationService>();

            AddCustomAuthentication(services, settings.Secret, settings.Issuer);
        }

        /// <summary>
        /// Setup custom authentication services.
        /// </summary>
        private static IServiceCollection AddCustomAuthentication(this IServiceCollection services, string secret, string issuer)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.SecurityTokenValidators.Clear();
                options.SecurityTokenValidators.Add(new JwtSecurityTokenHandler());
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    ValidateAudience = false,
                    ValidIssuers = new[] { issuer },
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = (context) =>
                    {
                        // Try to get the token from a header, a cookie or from querystring.
                        // The header is the usual option. The cookie is used by the chrome
                        // extension, and the querystring is usefull when debugging.
                        var token = (string)context.Request.Headers["Authorization"] ??
                        context.Request.Cookies["Authorization"] ??
                        (string)context.Request.Query["Authorization"] ??
                        "";

                        // Remove 'Bearer ' if present
                        if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                            token = token.Substring("Bearer ".Length);

                        // Sets the token
                        context.Token = token;

                        // Append the authorization token to the request so it can be
                        // forwarded when the request gets proxied to the internal api
                        if (!context.Request.Headers.ContainsKey("Authorization"))
                            context.Request.Headers.Add("Authorization", "Bearer " + token);

                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }

    }

    public static class CustomAuthenticationApplicationBuilderExtensions
    {

        public static void UseCustomAuthentication(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}