using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using projectRootNamespace.Api.Infrastructure.DataAccess;

namespace projectRootNamespace.Api.DataAccess.Entities
{
    public enum SkuSize
    {
        VerySmall,
        Small,
        Medium,
        Large,
        VeryLarge
    }

    public class Sku : CoreEntity<int>
    {
        [Required, MaxLength(64)]
        public string Model { get; set; }

        [Required]
        public SkuSize Size { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sku>(e =>
            {
                e.HasIndex(p => p.Model);
                e.Property(p => p.Size).HasConversion(new EnumToStringConverter<SkuSize>());
            });
        }
    }
}
