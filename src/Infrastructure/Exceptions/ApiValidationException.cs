using System;
using System.Linq;

namespace ProjectRootNamespace.Api.Infrastructure
{
    public enum ApiValidationErrors
    {
        /// <summary>
        /// Generic validation error
        /// </summary>
        [ValidationDetails("VALIDATION_ERROR", "A validation error has occured")]
        ValidationError,

        /// <summary>
        /// Used when trying to create or update a record with a fiscal number
        /// that is already in use.
        /// </summary>
        [ValidationDetails("DUPLICATE_FISCAL_NUMBER", "A contact with the same fiscal number already exists")]
        DuplicateFiscalNumber,

        /// <summary>
        /// Used when trying to create or update a record with a invalid fiscal number
        /// </summary>
        [ValidationDetails("INVALID_FISCAL_NUMBER", "Invalid fiscal number")]
        InvalidFiscalNumber,

        /// <summary>
        /// Used when trying to create or update a vehicle with a license plate
        /// that is already in use.
        /// </summary>
        [ValidationDetails("DUPLICATE_LICENSE_PLATE", "A vehicle with the same license plate already exists")]
        DuplicateLicensePlate,

        /// <summary>
        /// Used when a dynamic business rule evaluation fails.
        /// </summary>
        [ValidationDetails("DYNAMIC_RULE_ERROR", "A dynamic rule error has occured")]
        DynamicRuleValidationError,

        /// <summary>
        /// Used when no price rule is found for a given product.
        /// </summary>
        [ValidationDetails("NO_APPLICABLE_PRICE_RULES_FOUND", "No applicable price rule was found")]
        NoApplicablePriceRuleFoundError,

        /// <summary>
        /// Used when try save an invalid process document type.
        /// </summary>
        [ValidationDetails("CAN_NOT_UPDATE_PROCESS_SALE", "This process can not be updated because its has already a sale associated.")]
        CanNotUpdateProcessSale,

        /// <summary>
        /// Used when try save an invalid process document type.
        /// </summary>
        [ValidationDetails("CAN_NOT_UPDATE_PROCESS", "This process can not be updated because it's status is in a non editable state.")]
        CanNotUpdateProcess,

        /// <summary>
        /// Used when try to upload a file that exceed the size limit.
        /// </summary>
        [ValidationDetails("UPLOAD_MAX_FILE_SIZE", "The upload file exceeds the max.")]
        UploadMaxFileSize,

        /// <summary>
        /// Used when try to upload a file that doesn't have a valid content type.
        /// </summary>
        [ValidationDetails("UPLOAD_NOT_ALLOWED_CONTENT_TYPE", "The upload file is not valid.")]
        UploadNotAllowedContentType,

        /// <summary>
        /// Used when a license plate has an invalid format
        /// </summary>
        [ValidationDetails("NOT_ALLOWED_LICENSE_PLATE_FORMAT", "The license plate format is not allowed.")]
        NotAllowedLicensePlateFormat,

        /// <summary>
        /// Used when a license plate has an invalid format
        /// </summary>
        [ValidationDetails("PROCESS_TO_RENEW_MUST_BE_ISSUED", "The process to be renewed must be issued.")]
        ProcessToRenewMustBeIssued,

        /// <summary>
        /// Used when a license plate has an invalid format
        /// </summary>
        [ValidationDetails("PROCESS_TO_RENEW_MUST_EXPIRE_IN_30_DAYS", "The process to be renewed must expire in less than 30 days.")]
        ProcessToRenewMustExpireIn30Days,

        /// <summary>
        /// Used when a license plate has an invalid format
        /// </summary>
        [ValidationDetails("PROCESS_OF_VEHICLE_MUST_BE_ISSUED", "The process of this vehicle must be issued.")]
        ProcessOfVehicleMustBeIssued,

        /// <summary>
        /// Duplicated product notification template
        /// </summary>
        [ValidationDetails("DUPLICATE_PRODUCT_NOTIFICATION_TEMPLATE", "The product notification template is unique by Product, Method and Type.")]
        DuplicateNotificationTemplate,

        /// <summary>
        /// Product notification template must have at least one attachment
        /// </summary>
        [ValidationDetails("PRODUCT_NOTIFICATION_TEMPLATE_REQUIRED_ATTACHMENT", "The product notification template must have at least one attachment for this Method.")]
        NotificationTemplateRequiredAttachment,

        /// <summary>
        /// Product notification template must have at least one attachment
        /// </summary>
        [ValidationDetails("NOTIFICATION_TEMPLATE_NOT_FOUND", "The product don't have any notification template for the current contact and notification type.")]
        NoNotificationTemplateFound

    }

    public class ApiValidationException : ApiException
    {
        public new string Message { get; private set; }

        public ApiValidationException(ApiValidationErrors error, string message = null, Exception details = null)
            : base(GetExceptionData(error, message, details))
        {
        }

        private static ApiExceptionData GetExceptionData(ApiValidationErrors error, string message = null, Exception details = null)
        {
            Type type = error.GetType();
            Array values = System.Enum.GetValues(type);

            foreach (int val in values)
            {
                if (val == (int)error)
                {
                    var memInfo = type.GetMember(type.GetEnumName(val));
                    var att = memInfo[0]
                        .GetCustomAttributes(typeof(ValidationDetailsAttribute), false)
                        .FirstOrDefault() as ValidationDetailsAttribute;

                    if (att != null)
                    {
                        var retVal = new ApiExceptionData();
                        retVal.StatusCode = 400;
                        retVal.Code = att.Code;
                        retVal.Message = message ?? att.Message;
                        retVal.Details = details == null ? null : details.Message;
                        return retVal;
                    }
                }
            }

            throw new Exception("The specified ValidationDetailsAttribute was not found");
        }
    }

    #region Auxiliar

    [AttributeUsage(AttributeTargets.All)]
    public class ValidationDetailsAttribute : Attribute
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public ValidationDetailsAttribute(string code, string message) { Code = code; Message = message; }
    }


    #endregion
}