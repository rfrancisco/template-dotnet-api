using System;

namespace projectRootNamespace.Api.Infrastructure.Security.CustomAuthentication
{
    public class CustomAuthenticationSettings
    {
        /// <summary>
        /// String used to sign and verify jwt Tokens.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// The domain that issued the jwt token.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Number of hours until the jwt token expires
        /// </summary>
        public int Expiration { get; set; }
    }
}
