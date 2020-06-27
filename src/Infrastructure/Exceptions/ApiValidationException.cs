namespace ProjectRootNamespace.Api.Infrastructure
{
    public class ApiValidationException : ApiException
    {
        public ApiValidationException(string code, string message)
            : base(new ApiExceptionData(400, code, message))
        {
        }
    }
}