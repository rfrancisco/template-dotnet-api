using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace projectRootNamespace.Api.Infrastructure.DataAccess
{
    public class CoreEntity<TEntityKey> : ISoftDeletableEntity, IAuditableEntity, IIdentityEntity<TEntityKey>
    {
        [Key]
        public TEntityKey Id { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string UpdatedBy { get; set; }

        [Required]
        public DateTime UpdatedOn { get; set; }

        [Required, DefaultValue(false)]
        public bool Deleted { get; set; }
    }
}
