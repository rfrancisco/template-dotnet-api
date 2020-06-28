using System.ComponentModel.DataAnnotations;
using ProjectRootNamespace.Api.Infrastructure.DataAccess;

namespace ProjectRootNamespace.Api.DataAccess.Entities
{
    public class Product : CoreEntity
    {
        [Required, MaxLength(64)]
        public string Name { get; set; }

        [Required, MaxLength(1024)]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Stock { get; set; }
    }
}
