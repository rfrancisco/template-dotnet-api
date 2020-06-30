using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using projectRootNamespace.Api.DataAccess.Seed;
using projectRootNamespace.Api.Infrastructure.DataAccess;
using projectRootNamespace.Api.DataAccess.Entities;

namespace projectRootNamespace.Api.DataAccess
{
    public class MainDbContext : BaseDbContext
    {
        // TODO: Register entities DbSets here

        public MainDbContext(
            DbContextOptions<MainDbContext> options,
            ILoggerFactory logger,
            IHostEnvironment env) : base(options, logger, env) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: Call entities OnModelCreating methods here
            Product.OnModelCreating(modelBuilder);
            Sku.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);

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
    }
}
