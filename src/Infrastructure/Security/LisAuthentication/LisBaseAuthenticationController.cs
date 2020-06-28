using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjectRootNamespace.Api.Infrastructure.Security.LisAuthentication
{
    [Route("/authentication")]
    public abstract class LisBaseAuthenticationController : BaseController
    {
        private readonly ILisAuthenticationService _svc;
        private readonly LisAuthenticationSettings _settings;

        public LisBaseAuthenticationController(ILisAuthenticationService svc, IOptions<LisAuthenticationSettings> settings)
        {
            _svc = svc;
            _settings = settings.Value;
        }

        /// <summary>
        /// Allow a user to signin. If successful returns the jwt that was
        /// generated and appends an authotization cookie to the response.
        /// </summary>
        /// <param name="model">The user credentials</param>
        /// <response code="200">Returns the new acccess and refresh token.</response>
        /// <response code="400">If the credentials dont match a valid user</response>
        [AllowAnonymous]
        [HttpPut("signIn")]
        [SwaggerOperation(Tags = new[] { "Authentication" })]
        public async Task<ActionResult<AccessTokenResponseModel>> SignIn(LisSignInDTO model)
        {
            var result = await _svc.GenerateAccessToken(model.Username, model.Password);

            var res = HttpContext.Response;
            res.Headers.Append("Set-Cookie", result.AccessTokenSourceCookie);
            res.Headers.Append("Set-Cookie", result.RefreshTokenSourceCookie);

            return result;
        }

        /// <summary>
        /// Allow a user to signout by removing the access and refresh tokens
        /// cookies.
        /// </summary>
        /// <response code="200">Always</response>
        [HttpPut("signOut")]
        [SwaggerOperation(Tags = new[] { "Authentication" })]
        public void SignOut()
        {
            var res = HttpContext.Response;
            var options = new CookieOptions()
            {
                // The domain must match the lis domain
                Domain = new System.Uri(_settings.BaseUrl).Host,
            };
            res.Cookies.Delete(LisAuthenticationService.AccessTokenCookieName, options);
            res.Cookies.Delete(LisAuthenticationService.RefreshTokenCookieName, options);
            res.Cookies.Delete(LisAuthenticationService.AccessTokenCookieNameV2, options);
        }

        /// <summary>
        /// Generates a new access token using a refresh token. If successful
        /// returns the jwt that was generated and appends an authotization
        /// cookie to the response.
        /// </summary>
        /// <response code="200">Returns the new acccess and refresh token.</response>
        /// <response code="400">If the credentials dont match a valid user</response>
        [HttpPut("refreshToken")]
        [SwaggerOperation(Tags = new[] { "Authentication" })]
        public ActionResult RefreshToken()
        {
            // The token refresh is actualy done by the middleware
            return Ok();
        }

        /// <summary>
        /// Returns information regarding the authenticated user.
        /// </summary>
        /// <response code="200">Returns information for the authenticated user.</response>
        [HttpGet("userInfo")]
        [SwaggerOperation(Tags = new[] { "Authentication" })]
        public async Task<ActionResult<LisUserInfoDTO>> GetUserInfo()
        {
            return await _svc.GetAuthenticatedUserInfo();
        }
    }

    #region DTOs

    public class LisSignInDTO
    {
        [Required]
        [StringLength(64)]
        public string Username { get; set; }

        [Required]
        [StringLength(64)]
        public string Password { get; set; }
    }

    #endregion
}
