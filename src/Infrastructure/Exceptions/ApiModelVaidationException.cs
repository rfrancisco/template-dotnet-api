using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ProjectName.Api.Infrastructure
{
    public class ApiModelValidationException : ApiException
    {
        public new string Message { get; private set; }

        public ApiModelValidationException(ModelStateDictionary context)
            : base(GetExceptionData(context))
        {
        }

        private static ApiExceptionData GetExceptionData(ModelStateDictionary context)
        {
            var errors = context.Where(e => e.Value.Errors.Count > 0)
                                .Select(e => new { Name = e.Key, Message = e.Value.Errors.First().ErrorMessage }).ToArray();

            var retVal = new ApiExceptionData();
            retVal.StatusCode = 400;
            retVal.Code = "INVALID_MODEL";
            retVal.Message = "The model is invalid";
            retVal.Details = errors;
            return retVal;
        }
    }
}