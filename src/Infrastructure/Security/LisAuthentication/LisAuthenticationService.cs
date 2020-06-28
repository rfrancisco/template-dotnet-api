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
    public interface ILisAuthenticationService
    {
        /// <summary>
        /// Return the name of the cookie lis uses to store the jwt access token.
        /// </summary>
        /// <returns>The cookie name.</returns>
        string GetAccessTokenCookieName();

        /// <summary>
        /// Return the name of the cookie lis uses to store the jwt refresh token.
        /// </summary>
        /// <returns>The cookie name.</returns>
        string GetRefreshTokenCookieName();

        /// <summary>
        /// Calls the auth server to authenticate the user with the provided
        /// credentials and returns the access and refresh tokens.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The access and refresh tokens.</returns>
        Task<AccessTokenResponseModel> GenerateAccessToken(string username, string password);

        /// <summary>
        /// Calls the auth server to refresh the access token using a refresh
        /// token and return the new access token.
        /// </summary>
        /// <returns>The new access token.</returns>
        Task<AccessTokenResponseModel> RefreshAccessToken();

        /// <summary>
        /// Calls the auth server to get the metadata associated with the
        /// current authenticated user.
        /// </summary>
        /// <returns>The new access token.</returns>
        Task<LisUserInfoDTO> GetAuthenticatedUserInfo();

        /// <summary>
        /// Check if the current authenticated user has any of the given roles
        /// </summary>
        /// <returns>True if has any given role</returns>
        Task<bool> AuthenticatedUserHasRoles(params string[] roles);

        /// <summary>
        /// Check if the current authenticated user has any of the given roles
        /// </summary>
        /// <returns>True if has any given role</returns>
        bool AuthenticatedUserHasRoles(LisUserInfoDTO authenticatedUser, params string[] roles);
    }

    public class LisAuthenticationService : ILisAuthenticationService
    {
        public const string AccessTokenCookieName = "authorization-lis-v3.0";
        public const string RefreshTokenCookieName = "refresh-lis-v3.0";

        public const string AccessTokenCookieNameV2 = "caap-lip-v2.0";

        private const string GenerateAccessTokenPath = "/ws/accounts/data.php";
        private const string RefreshAccessTokenPath = "/ws/data.php";

        private readonly LisAuthenticationSettings _settings;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LisAuthenticationService(
            IOptions<LisAuthenticationSettings> settings,
            IHttpClientFactory clientFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _settings = settings.Value;
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetAccessTokenCookieName()
        {
            return AccessTokenCookieName;
        }

        public string GetRefreshTokenCookieName()
        {
            return RefreshTokenCookieName;
        }

        public async Task<AccessTokenResponseModel> GenerateAccessToken(string username, string password)
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
                        return new AccessTokenResponseModel(cookies);
                    }
                }

                throw new ApiSecurityException(ApiSecurityErrors.AuthenticationError);
            }
            catch (Exception)
            {
                throw new ApiSecurityException(ApiSecurityErrors.AuthenticationError);
            }
        }

        public async Task<AccessTokenResponseModel> RefreshAccessToken()
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
                        return new AccessTokenResponseModel(cookies);
                    }
                }

                throw new ApiSecurityException(ApiSecurityErrors.AuthenticationError);
            }
            catch (Exception)
            {
                throw new ApiSecurityException(ApiSecurityErrors.AuthenticationError);
            }
        }

        public async Task<LisUserInfoDTO> GetAuthenticatedUserInfo()
        {
            var request = CreateUserInfoRequest();
            var client = _clientFactory.CreateClient("lis");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<LisResponse>(responseData);

                if (result.Answer != null)
                    return result.Answer;
            }

            throw new ApiSecurityException(ApiSecurityErrors.UnauthorizedError);
        }

        public async Task<bool> AuthenticatedUserHasRoles(params string[] roles)
        {
            var user = await GetAuthenticatedUserInfo();
            return AuthenticatedUserHasRoles(user, roles);
        }

        public bool AuthenticatedUserHasRoles(LisUserInfoDTO authenticatedUser, params string[] roles)
        {
            var rolesLower = roles.Select(r => r.ToLower());
            return authenticatedUser.Roles.Any(r => rolesLower.Contains(r.Key.ToLower()));
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
            var context = _httpContextAccessor.HttpContext;
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
            var context = _httpContextAccessor.HttpContext;
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
    }

    #region DTOs

    public class AccessTokenResponseModel
    {
        /// <summary>
        /// The access token that was generated by the auth server (lis in our case).
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// The access token expiration date.
        /// </summary>
        public DateTime AccessTokenExpiration { get; set; }

        /// <summary>
        /// The access token cookie that was generated by the auth server (lis in our case).
        /// </summary>
        public string AccessTokenSourceCookie { get; set; }

        /// <summary>
        /// The refresh token that was generated by the auth server (lis in our case).
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// The refresh token expiration date.
        /// </summary>
        public DateTime RefreshTokenExpiration { get; set; }

        /// <summary>
        /// The refresh token cookie that was generated by the auth server (lis in our case).
        /// </summary>
        public string RefreshTokenSourceCookie { get; set; }

        public AccessTokenResponseModel()
        {
        }

        public AccessTokenResponseModel(IEnumerable<string> cookies)
        {
            var accessTokenCookie = cookies.FirstOrDefault(x => x.StartsWith(LisAuthenticationService.AccessTokenCookieName));
            var refreshTokenCookie = cookies.FirstOrDefault(x => x.StartsWith(LisAuthenticationService.RefreshTokenCookieName));
            var accessTokenParts = accessTokenCookie.Split(new char[] { '=', ';' });
            var refreshTokenParts = refreshTokenCookie.Split(new char[] { '=', ';' });

            AccessToken = accessTokenParts[1];
            AccessTokenExpiration = DateTime.Parse(accessTokenParts[3]);
            AccessTokenSourceCookie = accessTokenCookie;
            RefreshToken = refreshTokenParts[1];
            RefreshTokenExpiration = DateTime.Parse(refreshTokenParts[3]);
            RefreshTokenSourceCookie = refreshTokenCookie;
        }
    }

    public class LisResponse
    {
        public LisUserInfoDTO Answer { get; set; }
    }

    public class LisUserInfoDTO
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