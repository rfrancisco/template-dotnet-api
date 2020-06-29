using System;
using System.Linq;

namespace projectRootNamespace.Api.Infrastructure.Exceptions
{
    public enum ApiSecurityErrors
    {
        /// <summary>
        /// Used when the user tries to access a protected resource without
        /// providing authentication creadentials.
        /// </summary>
        [SecurityErrorDetails(401, "UNAUTHORIZED", "Authentication is required to access the specified resource")]
        UnauthorizedError,

        /// <summary>
        /// Used when the user tries to access a protected resource without
        /// the necessary access credentials.
        /// </summary>
        [SecurityErrorDetails(403, "FORBIDDED", "Access to the specified resource is denied")]
        ForbiddenError,

        /// <summary>
        /// Used to the user tries to authenticate with invalid credentials.
        /// </summary>
        [SecurityErrorDetails(400, "AUTHENTICATION_FAILED", "The provided credentials are invalid")]
        AuthenticationError,
    }

    public class ApiSecurityException : ApiException
    {
        public ApiSecurityException(ApiSecurityErrors error, string message = null, Exception details = null)
            : base(GetExceptionData(error, message, details))
        {
        }

        private static ApiExceptionData GetExceptionData(ApiSecurityErrors error, string message = null, Exception details = null)
        {
            Type type = error.GetType();
            Array values = System.Enum.GetValues(type);

            foreach (int val in values)
            {
                if (val == (int)error)
                {
                    var memInfo = type.GetMember(type.GetEnumName(val));
                    var att = memInfo[0]
                        .GetCustomAttributes(typeof(SecurityErrorDetailsAttribute), false)
                        .FirstOrDefault() as SecurityErrorDetailsAttribute;

                    if (att != null)
                    {
                        var retVal = new ApiExceptionData();
                        retVal.StatusCode = att.StatusCode;
                        retVal.Code = att.Code;
                        retVal.Message = message ?? att.Message;
                        retVal.Details = details == null ? null : details.Message;
                        return retVal;
                    }
                }
            }

            throw new Exception("The specified SecurityErrorDetailsAttribute was not found");
        }
    }

    #region Auxiliar

    [AttributeUsage(AttributeTargets.All)]
    public class SecurityErrorDetailsAttribute : Attribute
    {
        public int StatusCode { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }

        public SecurityErrorDetailsAttribute(int statusCode, string code, string message) { StatusCode = statusCode; Code = code; Message = message; }
    }

    #endregion
}