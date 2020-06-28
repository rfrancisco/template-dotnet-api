using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectRootNamespace.Api.Infrastructure.Exceptions;

namespace ProjectRootNamespace.Api.Infrastructure.Security.CustomAuthentication
{
    public class CustomAuthenticationService : IAuthenticationService
    {
        private readonly HttpContext _httpContext;
        private readonly CustomAuthenticationSettings _settings;

        public CustomAuthenticationService(
            IHttpContextAccessor contextAccessor,
            IOptions<CustomAuthenticationSettings> settings)
        {
            _httpContext = contextAccessor.HttpContext;
            _settings = settings.Value;
        }

        public Task<AccessTokenDTO> SignIn(SignInCredentialsDTO dto)
        {
            if (dto.Username != "user" || dto.Password != "pass")
            {
                throw new ApiSecurityException(ApiSecurityErrors.AuthenticationError);
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Upn, dto.Username)
            };

            // Generates the Token
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(_settings.Secret));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _settings.Issuer,
                Audience = _settings.Issuer,
                Expires = DateTime.Now.AddSeconds(_settings.Expiration * 3600),
                IssuedAt = DateTime.Now,
                SigningCredentials = signingCredentials
            };

            var securityToken = tokenHandler.CreateToken(tokenDescription);
            var accessToken = tokenHandler.WriteToken(securityToken);

            // Write a cookie with the access token
            var options = new CookieOptions();
            options.Expires = DateTimeOffset.Now.AddHours(_settings.Expiration);
            options.HttpOnly = true;
            _httpContext.Response.Cookies.Append("Authorization", accessToken, options);

            return Task.FromResult(new AccessTokenDTO()
            {
                AccessToken = accessToken
            });
        }

        public Task SignOut()
        {
            return null;
        }

        public Task<AccessTokenDTO> RefeshAccessToken(string refreshToken)
        {
            return null;
        }

        public Task<UserInfoDTO> GetUserInfo()
        {
            return null;
        }
    }
}