using System.Collections.Generic;

namespace ProjectRootNamespace.Api.Infrastructure.Security
{
    /// <summary>
    /// Interface that represents an authenticated user
    /// </summary>
    public interface IAuthenticatedUser
    {
        /// <summary>
        /// The display name for the user (uaualy the username, email of fullname)
        /// </summary>
        string DisplayName { get; set; }

        /// <summary>
        /// (Required) The user unique identifier (usualy the username or email)
        /// </summary>
        string UniqueIdentifier { get; set; }

        /// <summary>
        /// (Optional) The user email address
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// (Optional) The user avatar
        /// </summary>
        string Avatar { get; set; }

        /// <summary>
        /// (Optional) Aditional information regarding the authenticated user
        /// </summary>
        IDictionary<string, string> Metadata { get; set; }
    }
}