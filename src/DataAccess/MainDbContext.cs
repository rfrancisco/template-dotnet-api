using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using ProjectRootNamespace.Api.DataAccess.Seed;
using ProjectRootNamespace.Api.DataAccess.Entities;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using ProjectRootNamespace.Api.Infrastructure.DataAccess;
using System;

namespace ProjectRootNamespace.Api.DataAccess
{
    public class MainDbContext : DbContext
    {
        // TODO: Register entities DbSets here
        public DbSet<Product> Products { get; set; }

        private readonly ILoggerFactory _loggerFactory;
        protected readonly IHostEnvironment _environment;

        public MainDbContext(
            DbContextOptions<MainDbContext> options,
            ILoggerFactory loggerFactory,
            IHostEnvironment environment) : base(options)
        {
            _loggerFactory = loggerFactory;
            _environment = environment;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: Call entities OnModelCreating methods here

            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.HasPostgresExtension("postgis");
            modelBuilder.ApplyGlobalFilters<ISoftDeletableEntity>(x => !x.Deleted);

            switch (_environment.EnvironmentName)
            {
                case "Development":
                    DevSeedDataGenerator.SeedData(modelBuilder);
                    break;
                case "Staging":
                    StgSeedDataGenerator.SeedData(modelBuilder);
                    break;
                case "Production":
                    PrdSeedDataGenerator.SeedData(modelBuilder);
                    break;
                default:
                    throw new System.Exception("Unexpected environment: " + _environment.EnvironmentName);
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Fail safe mechanism to ensure that all string properties
            // are trimmed before being saved
            foreach (var entity in this.ChangeTracker.Entries())
            {
                var properties = entity.Properties.ToList().Where(x => x.Metadata.ClrType == typeof(string));
                foreach (var property in properties)
                {
                    property.CurrentValue = property.CurrentValue?.ToString().Trim();
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
