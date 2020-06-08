using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using ProjectName.Api.DataAccess.Seed;

namespace ProjectName.Api.DataAccess
{
    public class MainDbContext : DbContext
    {
        // TODO: Register entities DbSets here

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
            modelBuilder.HasPostgresExtension("uuid-ossp");

            // TODO: Call entities OnModelCreating methods here

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
