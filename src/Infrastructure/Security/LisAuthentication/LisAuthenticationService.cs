using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProjectRootNamespace.Api.Infrastructure.Exceptions;

namespace ProjectRootNamespace.Api.Infrastructure.Security.LisAuthentication
{
    public class LisAuthenticationService : IAuthenticationService
    {
        public const string AccessTokenCookieName = "authorization-lis-v3.0";
        public const string RefreshTokenCookieName = "refresh-lis-v3.0";
        public const string AccessTokenCookieNameV2 = "caap-lip-v2.0";
        private const string GenerateAccessTokenPath = "/ws/accounts/data.php";
        private const string RefreshAccessTokenPath = "/ws/data.php";

        private readonly LisAuthenticationSettings _settings;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContext;

        public LisAuthenticationService(
            IOptions<LisAuthenticationSettings> settings,
            IHttpClientFactory clientFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _settings = settings.Value;
            _clientFactory = clientFactory;
            _httpContext = httpContextAccessor;
        }

        public async Task<AccessTokenDTO> SignIn(SignInCredentialsDTO dto)
        {
            var result = await GenerateAccessToken(dto.Username, dto.Password);

            var res = _httpContext.HttpContext.Response;
            res.Headers.Append("Set-Cookie", result.AccessTokenSourceCookie);
            res.Headers.Append("Set-Cookie", result.RefreshTokenSourceCookie);

            return result;
        }

        public Task SignOut()
        {
            var res = _httpContext.HttpContext.Response;
            var options = new CookieOptions()
            {
                // The domain must match the lis domain
                Domain = new System.Uri(_settings.BaseUrl).Host,
            };
            res.Cookies.Delete(LisAuthenticationService.AccessTokenCookieName, options);
            res.Cookies.Delete(LisAuthenticationService.RefreshTokenCookieName, options);
            res.Cookies.Delete(LisAuthenticationService.AccessTokenCookieNameV2, options);

            return Task.CompletedTask;
        }

        public async Task<AccessTokenDTO> RefeshAccessToken()
        {
            try
            {
                var request = CreateRefreshAccessTokenRequest();
                var client = _clientFactory.CreateClient("lis");
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<LisResponse>(responseData);

                    if (result?.Answer?.Id_user != null)
                    {
                        var cookies = response.Headers.GetValues("Set-Cookie");
                        var accessTokenCookie = cookies.FirstOrDefault(x => x.StartsWith(AccessTokenCookieName));
                        var refreshTokenCookie = cookies.FirstOrDefault(x => x.StartsWith(RefreshTokenCookieName));
                        var accessTokenParts = accessTokenCookie.Split(new char[] { '=', ';' });
                        var refreshTokenParts = refreshTokenCookie.Split(new char[] { '=', ';' });
                        var retVal = new AccessTokenDTO()
                        {
                            AccessToken = accessTokenParts[1],
                            AccessTokenExpiration = DateTime.Parse(accessTokenParts[3]),
                            AccessTokenSourceCookie = accessTokenCookie,
                            RefreshToken = refreshTokenParts[1],
                            RefreshTokenExpiration = DateTime.Parse(refreshTokenParts[3]),
                            RefreshTokenSourceCookie = refreshTokenCookie,
                        };
                        return retVal;
                    }
                }

                throw new ApiSecurityException(ApiSecurityErrors.AuthenticationError);
            }
            catch (Exception)
            {
                throw new ApiSecurityException(ApiSecurityErrors.AuthenticationError);
            }
        }

        public async Task<UserInfoDTO> GetUserInfo()
        {
            var request = CreateUserInfoRequest();
            var client = _clientFactory.CreateClient("lis");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<LisResponse>(responseData);

                if (result.Answer != null)
                    return new UserInfoDTO()
                    {
                        UniqueIdentifier = result.Answer.Id_user,
                        DisplayName = result.Answer.DisplayName,
                        Name = result.Answer.Name,
                        Email = result.Answer.Email,
                        Photo = result.Answer.Photo,
                        Roles = result.Answer.Roles.Select(x => x.Key)
                    };
            }

            throw new ApiSecurityException(ApiSecurityErrors.UnauthorizedError);
        }

        #region Auxiliar methods

        private async Task<AccessTokenDTO> GenerateAccessToken(string username, string password)
        {
            try
            {
                var request = CreateGenerateAccessTokenRequest(username, password);
                var client = _clientFactory.CreateClient("lis");
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    if (responseData.Contains("success"))
                    {
                        var cookies = response.Headers.GetValues("Set-Cookie");
                        var accessTokenCookie = cookies.FirstOrDefault(x => x.StartsWith(AccessTokenCookieName));
                        var refreshTokenCookie = cookies.FirstOrDefault(x => x.StartsWith(RefreshTokenCookieName));
                        var accessTokenParts = accessTokenCookie.Split(new char[] { '=', ';' });
                        var refreshTokenParts = refreshTokenCookie.Split(new char[] { '=', ';' });
                        var retVal = new AccessTokenDTO()
                        {
                            AccessToken = accessTokenParts[1],
                            AccessTokenExpiration = DateTime.Parse(accessTokenParts[3]),
                            AccessTokenSourceCookie = accessTokenCookie,
                            RefreshToken = refreshTokenParts[1],
                            RefreshTokenExpiration = DateTime.Parse(refreshTokenParts[3]),
                            RefreshTokenSourceCookie = refreshTokenCookie,
                        };
                        return retVal;
                    }
                }

                throw new ApiSecurityException(ApiSecurityErrors.AuthenticationError);
            }
            catch (Exception)
            {
                throw new ApiSecurityException(ApiSecurityErrors.AuthenticationError);
            }
        }

        private HttpRequestMessage CreateGenerateAccessTokenRequest(string username, string password)
        {
            var retVal = new HttpRequestMessage(HttpMethod.Post, _settings.BaseUrl + GenerateAccessTokenPath);
            retVal.Content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"feature", "auth-login"},
                {"service", "login"},
                {"u", username},
                {"p", CreatePasswordHash(password)}
            });
            return retVal;
        }

        private HttpRequestMessage CreateRefreshAccessTokenRequest()
        {
            var context = _httpContext.HttpContext;
            var cookies = context.Request.Cookies;
            var refreshToken = context.Items["RefreshToken"] ?? cookies[RefreshTokenCookieName];
            var retVal = new HttpRequestMessage(HttpMethod.Post, _settings.BaseUrl + RefreshAccessTokenPath);
            retVal.Content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "feature", "cookie" },
                { "service", "validate" }
            });
            retVal.Headers.Add("Cookie", $"{RefreshTokenCookieName}={refreshToken}");
            return retVal;
        }

        private HttpRequestMessage CreateUserInfoRequest()
        {
            var context = _httpContext.HttpContext;
            var cookies = context.Request.Cookies;
            var refreshToken = context?.Items["RefreshToken"] ?? cookies[RefreshTokenCookieName];
            var accessToken = context?.Items["AccessToken"] ?? cookies[AccessTokenCookieName];
            var retVal = new HttpRequestMessage(HttpMethod.Post, _settings.BaseUrl + RefreshAccessTokenPath);
            retVal.Content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "feature", "cookie" },
                { "service", "validate" }
            });
            retVal.Headers.Add("Cookie", $"{AccessTokenCookieName}={accessToken}; {RefreshTokenCookieName}={refreshToken}");
            return retVal;
        }

        private string CreatePasswordHash(string input)
        {
            // Step 1, calculate MD5 hash from input
            var md5 = System.Security.Cryptography.MD5.Create();
            var inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);

            // Step 2, convert byte array to hex string
            var sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
                sb.Append(hashBytes[i].ToString("X2"));

            return sb.ToString().ToLower();
        }

        #endregion

        #region DTOs

        private class LisResponse
        {
            public LisUserInfoDTO Answer { get; set; }
        }

        private class LisUserInfoDTO
        {
            public string Name { get; set; }
            public string Id_user { get; set; }
            public string Email { get; set; }
            public string Photo { get; set; }
            public string DisplayName { get; set; }
            public IDictionary<string, string> Roles { get; set; }
        }

        #endregion
    }
}