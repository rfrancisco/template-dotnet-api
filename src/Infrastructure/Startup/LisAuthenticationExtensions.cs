using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using projectRootNamespace.Api.Infrastructure.Security;
using projectRootNamespace.Api.Infrastructure.Security.LisAuthentication;

namespace projectRootNamespace.Api.Infrastructure.Startup
{
    /// <summary>
    /// Extension methods to setup lis integration
    /// </summary>
    public static class LisAuthenticationServiceCollectionExtensions
    {
        public static void AddLisAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("Settings:Authentication:Lis");
            var settings = section.Get<LisAuthenticationSettings>();
            services.Configure<LisAuthenticationSettings>(section);

            services.AddScoped<IAuthenticationService, LisAuthenticationService>();

            AddLisAuthentication(services, settings.PublicKey);
            AddLisHttpClient(services, settings.BaseUrl);
        }

        /// <summary>
        /// Setup Lis authentication services.
        /// </summary>
        /// <param name="services">The mapify public key used to validate the token signature.</param>
        /// <param name="publicKey">The mapify public key used to validate the token signature.</param>
        public static IServiceCollection AddLisAuthentication(this IServiceCollection services, string publicKey)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.SecurityTokenValidators.Clear();
                options.SecurityTokenValidators.Add(new JwtSecurityTokenHandler());
                options.TokenValidationParameters = GetTokenValidationParameters(publicKey);
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = (context) =>
                    {
                        // Try to get the token from a header, a cookie or from querystring.
                        // The header is the usual option. The cookie is used by the chrome
                        // extension, and the querystring is usefull when debugging.
                        var token = (string)context.Request.Headers["Authorization"] ??
                            context.Request.Cookies[LisAuthenticationService.AccessTokenCookieName] ??
                            (string)context.Request.Query["Authorization"] ??
                            "";

                        // Remove 'Bearer ' if present
                        if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                            token = token.Substring("Bearer ".Length);

                        // If the token is missing check if we have a refresh token.
                        // If we have a refresh token we try to generate a new access token using it
                        if (token == null || token == "")
                        {
                            var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
                            var logger = loggerFactory.CreateLogger("Startup");
                            var cookies = context.HttpContext.Request.Cookies;
                            var req = context.HttpContext.Request;
                            var res = context.HttpContext.Response;

                            if (cookies.ContainsKey(LisAuthenticationService.RefreshTokenCookieName))
                            {
                                try
                                {
                                    var svc = context.HttpContext.RequestServices.GetRequiredService<IAuthenticationService>();
                                    var newToken = svc.RefeshAccessToken().GetAwaiter().GetResult();

                                    // Append the new tokens cookies to the response
                                    res.Headers.Clear();
                                    res.Headers.Append("Set-Cookie", newToken.AccessTokenSourceCookie);
                                    res.Headers.Append("Set-Cookie", newToken.RefreshTokenSourceCookie);

                                    // This is used to pass the new tokens forward since we cannot
                                    // replace the cookies that we received
                                    context.HttpContext.Items.Add("AccessToken", newToken.AccessToken);
                                    context.HttpContext.Items.Add("RefreshToken", newToken.RefreshToken);

                                    token = newToken.AccessToken;
                                }
                                catch (Exception ex)
                                {
                                    logger.LogDebug(ex, "Error refreshing access token");
                                }
                            }
                        }

                        // Sets the token
                        context.Token = token;

                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }

        public static IServiceCollection AddLisHttpClient(this IServiceCollection services, string lisBaseUrl)
        {
            services.AddHttpClient("lis", options => options.BaseAddress = new Uri(lisBaseUrl))
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    return new HttpClientHandler()
                    {
                        AllowAutoRedirect = false,
                        UseCookies = false
                    };
                });

            return services;
        }

        /// <summary>
        /// Returns the token validation parameters.
        /// </summary>
        /// <param name="key">The public key used to validate the token signature.</param>
        /// <returns>The token validation parameters.</returns>
        private static TokenValidationParameters GetTokenValidationParameters(string key)
        {
            var keyBytes = Convert.FromBase64String(key);
            var asymmetricKeyParameter = PublicKeyFactory.CreateKey(keyBytes);
            var rsaKeyParameters = (RsaKeyParameters)asymmetricKeyParameter;
            var rsaParameters = new RSAParameters
            {
                Modulus = rsaKeyParameters.Modulus.ToByteArrayUnsigned(),
                Exponent = rsaKeyParameters.Exponent.ToByteArrayUnsigned()
            };
            var rsa = new RSACryptoServiceProvider();

            rsa.ImportParameters(rsaParameters);

            var validationParameters = new TokenValidationParameters()
            {
                RequireExpirationTime = false,
                RequireSignedTokens = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new RsaSecurityKey(rsa)
            };

            return validationParameters;
        }
    }

    public static class LisAuthenticationApplicationBuilderExtensions
    {

        public static void UseLisAuthentication(this IApplicationBuilder app)
        {

            app.UseAuthentication();

            app.Use(async (context, next) =>
            {
                // Map payload.roles in the mapify token into roles claims
                var identity = context.User.Identity as ClaimsIdentity;
                if (identity.IsAuthenticated)
                {
                    var claims = identity.Claims;
                    var payload = claims.FirstOrDefault(x => x.Type == "payload").Value;
                    var roles = JsonConvert.DeserializeAnonymousType(payload, new { Roles = new List<string>() }).Roles;
                    foreach (var role in roles)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, role));
                    }
                }

                await next.Invoke();
            });

            app.UseAuthorization();

        }
    }
}