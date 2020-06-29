using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
            var isUser = dto.Username == "user" && dto.Password == "pass";
            var isAdmin = dto.Username == "admin" && dto.Password == "pass";

            if (!isUser && !isAdmin)
                throw new ApiSecurityException(ApiSecurityErrors.AuthenticationError);

            var claims = new[]
            {
                new Claim(ClaimTypes.Upn, dto.Username),
                new Claim("display_name", isUser ? "System User" : "System Administrator"),
                new Claim(ClaimTypes.Email, isUser ? "user@focus.com" : "admin@focus.com"),
                new Claim("photo", "https://static.wixstatic.com/media/a2d517_b155553cfec248378a8b6d73c7c17d5e~mv2.png/v1/fill/w_573,h_469/Focus%20bc%20favicon.png"),
                new Claim(ClaimTypes.Role, isUser ? "Users" : "Admins"),
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
            // Clears the authorization cookie containing the access token
            var options = new CookieOptions();
            options.Expires = DateTimeOffset.Now.AddDays(-1);
            options.HttpOnly = true;
            _httpContext.Response.Cookies.Append("Authorization", "", options);

            return Task.CompletedTask;
        }

        public Task<AccessTokenDTO> RefeshAccessToken()
        {
            throw new ApiValidationException("NOT_SUPPORTED", "Refresh token is not supported in this provider");
        }

        public Task<UserInfoDTO> GetUserInfo()
        {
            var claims = _httpContext.User.Claims;
            var retVal = new UserInfoDTO()
            {
                UniqueIdentifier = claims.FirstOrDefault(x => x.Type == ClaimTypes.Upn).Value,
                DisplayName = claims.FirstOrDefault(x => x.Type == "display_name").Value,
                Email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value,
                Photo = claims.FirstOrDefault(x => x.Type == "photo").Value,
                Roles = claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value)
            };

            return Task.FromResult(retVal);
        }
    }
}