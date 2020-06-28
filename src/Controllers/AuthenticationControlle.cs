using Microsoft.AspNetCore.Mvc;
using ProjectRootNamespace.Api.Infrastructure;
using ProjectRootNamespace.Api.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace ProjectRootNamespace.Api.Controllers
{
    [Route("/authentication")]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _svc;

        public AuthenticationController(IAuthenticationService svc)
        {
            _svc = svc;
        }

        /// <summary>
        /// Allow a user to signin. If successful returns the jwt that was
        /// generated and appends an authotization cookie to the response.
        /// </summary>
        /// <param name="dto">The dto containing the sign-in credentials</param>
        /// <response code="200">Returns the new acccess and refresh token.</response>
        /// <response code="400">If the credentials dont match a valid user</response>
        [AllowAnonymous]
        [HttpPut("signIn")]
        [SwaggerOperation(Tags = new[] { "Authentication" })]
        public Task<AccessTokenDTO> SignIn(SignInCredentialsDTO dto) =>
            _svc.SignIn(dto);

        /// <summary>
        /// Allow a user to signout by removing the access and refresh tokens
        /// cookies.
        /// </summary>
        /// <response code="200">Always</response>
        [HttpPut("signOut")]
        [SwaggerOperation(Tags = new[] { "Authentication" })]
        public Task SignOut() =>
            _svc.SignOut();

        /// <summary>
        /// Generates a new access token using a refresh token. If successful
        /// returns the jwt that was generated and appends an authotization
        /// cookie to the response.
        /// </summary>
        /// <response code="200">Returns the new acccess and refresh token.</response>
        /// <response code="400">If the credentials dont match a valid user</response>
        [HttpPut("refreshToken")]
        [SwaggerOperation(Tags = new[] { "Authentication" })]
        public Task RefeshAccessToken() =>
            _svc.RefeshAccessToken("");

        /// <summary>
        /// Returns information regarding the authenticated user.
        /// </summary>
        /// <response code="200">Returns information for the authenticated user.</response>
        [HttpGet("userInfo")]
        [SwaggerOperation(Tags = new[] { "Authentication" })]
        public Task<UserInfoDTO> GetUserInfo() =>
            _svc.GetUserInfo();
    }

}
