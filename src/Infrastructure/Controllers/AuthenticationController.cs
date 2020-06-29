using Microsoft.AspNetCore.Mvc;
using projectRootNamespace.Api.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace projectRootNamespace.Api.Infrastructure.Controllers
{
    [Route("/authentication")]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _svc;

        public AuthenticationController(IAuthenticationService svc) =>
            _svc = svc;

        /// <summary>
        /// Allow a user to signin.
        /// </summary>
        /// <remarks>
        /// If successful returns the jwt that was generated and appends an
        /// authotization cookie to the response.
        /// </remarks>
        /// <param name="dto">The dto containing the sign-in credentials.</param>
        /// <response code="200">Returns the new acccess and refresh token.</response>
        /// <response code="400">If the credentials dont match a valid user.</response>
        [AllowAnonymous]
        [HttpPut("signIn")]
        public Task<AccessTokenDTO> SignIn(SignInCredentialsDTO dto) =>
            _svc.SignIn(dto);

        /// <summary>
        /// Allow a user to signout by removing the access and refresh tokens
        /// cookies.
        /// </summary>
        /// <response code="200">If successful.</response>
        /// <response code="401">Request is not unauthorized.</response>
        [HttpPut("signOut")]
        public Task SignOut() =>
            _svc.SignOut();

        /// <summary>
        /// Generates a new access token using a refresh token.
        /// </summary>
        /// <remarks>
        /// If successful returns the jwt that was generated and appends an
        /// authotization cookie to the response.
        /// </remarks>
        /// <response code="200">Returns the new acccess and refresh token.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="401">Request is not unauthorized.</response>
        [HttpPut("refreshToken")]
        public Task<AccessTokenDTO> RefeshAccessToken() =>
            _svc.RefeshAccessToken();

        /// <summary>
        /// Returns information regarding the authenticated user.
        /// </summary>
        /// <response code="200">Returns information for the authenticated user.</response>
        /// <response code="401">Request is not unauthorized.</response>
        [HttpGet("userInfo")]
        public Task<UserInfoDTO> GetUserInfo() =>
            _svc.GetUserInfo();
    }

}
