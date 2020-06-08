using System;

namespace ProjectName.Api.Infrastructure.DataAccess
{
    public interface IAuditableEntity
    {
        /// <summary>
        /// The user that created the record.
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// The date of the record creation.
        /// </summary>
        DateTime CreatedOn { get; set; }

        /// <summary>
        /// The last user to modified the record.
        /// </summary>
        string UpdatedBy { get; set; }

        /// <summary>
        /// The date of the record last modification.
        /// </summary>
        DateTime UpdatedOn { get; set; }
    }
}