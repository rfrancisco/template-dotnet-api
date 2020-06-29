using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace projectRootNamespace.Api.Infrastructure.DataAccess
{
    public class CoreEntity<TEntityKey> : ISoftDeletableEntity, IAuditableEntity, IIdentityEntity<TEntityKey>
    {
        /// <summary>
        /// The record unique identifier.
        /// </summary>
        [Key]
        public TEntityKey Id { get; set; }

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
        [Required]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// The date of the record last modification.
        /// </summary>
        [Required]
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Flag that indicates if the record is deleted.
        /// </summary>
        [Required, DefaultValue(false)]
        public bool Deleted { get; set; }
    }
}
