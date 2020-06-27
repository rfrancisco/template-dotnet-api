using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ProjectRootNamespace.Api.Infrastructure.DataAccess;

namespace ProjectRootNamespace.Api.DataAccess.Entities
{
    public class Product : ISoftDeletableEntity, IIdentityEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(64)]
        public string Name { get; set; }

        [Required, MaxLength(1024)]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required, DefaultValue(false)]
        public bool Deleted { get; set; }
    }
}
