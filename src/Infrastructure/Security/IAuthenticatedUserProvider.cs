using System.Security.Claims;

namespace ProjectName.Api.Infrastructure.Security
{
    /// <summary>
    /// Interface used to specify how to retrieve the authenticated user information
    /// </summary>
    public interface IAuthenticatedUserProvider
    {
        /// <summary>
        /// Retrieve the authenticated user information
        /// </summary>
        IAuthenticatedUser GetAuthenticatedUser(ClaimsPrincipal user);
    }
}