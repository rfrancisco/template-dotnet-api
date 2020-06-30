using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using projectRootNamespace.Api.Infrastructure.DataAccess;

namespace projectRootNamespace.Api.DataAccess.Entities
{
    public class Product : CoreEntity<int>
    {
        [Required, MaxLength(64)]
        public string Name { get; set; }

        [Required, MaxLength(1024)]
        public string Description { get; set; }

        public ICollection<Sku> Skus { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(e =>
            {
                e.HasIndex(p => p.Name).IsUnique();
            });
        }
    }
}
