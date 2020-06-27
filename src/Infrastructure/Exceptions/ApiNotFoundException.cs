namespace ProjectRootNamespace.Api.Infrastructure
{
    public class ApiNotFoundException : ApiException
    {
        public ApiNotFoundException(string message = null)
            : base(new ApiExceptionData(404, "RECORD_NOT_FOUND", message ?? "The specified record could not be found"))
        {
        }
    }
}