using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectRootNamespace.Api.Infrastructure.DataAccess
{
    public class CoreEntity : ISoftDeletableEntity, IAuditableEntity, IIdentityEntity<int>
    {
        /// <summary>
        /// The record unique identifier.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The user that created the record.
        /// </summary>
        [Required]
        public string CreatedBy { get; set; }

        /// <summary>
        /// The date of the record creation.
        /// </summary>
        [Required]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// The last user to modified the record.
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// The date of the record last modification.
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Flag that indicates if the record is deleted.
        /// </summary>
        [Required, DefaultValue(false)]
        public bool Deleted { get; set; }
    }
}