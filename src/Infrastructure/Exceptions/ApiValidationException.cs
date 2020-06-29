namespace projectRootNamespace.Api.Infrastructure.Exceptions
{
    public class ApiValidationException : ApiException
    {
        public ApiValidationException(string code, string message)
            : base(new ApiExceptionData(400, code, message))
        {
        }
    }
}