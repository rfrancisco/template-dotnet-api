namespace ProjectName.Api.Infrastructure
{
    using System;
    using System.Text.Json;

    public class ApiException : Exception
    {
        public int StatusCode { get; set; }
        public string Code { get; set; }
        public string Location { get; set; }
        public object Details { get; set; }

        public ApiException(ApiExceptionData data) : base(data.Message)
        {
            StatusCode = data.StatusCode;
            Code = data.Code;
            Details = data.Details;
            Location = data.Location;
        }

        public ApiException(int statusCode, string code, string message = "", string location = null, object details = null) : base(message)
        {
            StatusCode = statusCode;
            Code = code;
            Location = location;
            Details = details;
        }

        public string ToString(bool includeDetails = false)
        {
            var error = new
            {
                Status = this.StatusCode,
                Code = this.Code,
                Message = this.Message,
                Details = includeDetails ? this.Details : null,
                Location = this.Location
            };

            return JsonSerializer.Serialize(error, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public override string ToString()
        {
            return ToString(false);
        }
    }

    public class ApiExceptionData
    {
        public int StatusCode { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string Location { get; set; }
        public object Details { get; set; }

        public ApiExceptionData()
        {
            StatusCode = 400;
            Code = null;
            Message = null;
            Location = null;
            Details = null;
        }

        public ApiExceptionData(int statusCode = 400, string code = "", string location = "", string message = "", Exception innerException = null)
        {
            StatusCode = statusCode;
            Code = code;
            Message = message;
            Location = location;
            Details = innerException == null ? null : innerException.Message;
        }
    }
}