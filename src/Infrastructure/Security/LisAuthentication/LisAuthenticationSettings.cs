using System;

namespace ProjectRootNamespace.Api.Infrastructure.Security.LisAuthentication
{
    public class LisAuthenticationSettings
    {
        /// <summary>
        /// The base url of Lis (protocol and domain only).
        /// </summary>
        public string PublicKey { get; set; }

        /// <summary>
        /// The base url of Lis (protocol and domain only).
        /// </summary>
        public string BaseUrl { get; set; }
    }
}
