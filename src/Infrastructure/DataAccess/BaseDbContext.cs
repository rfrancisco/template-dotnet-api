using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace projectRootNamespace.Api.Infrastructure.DataAccess
{
    public abstract class BaseDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        protected readonly IHostEnvironment _environment;

        public BaseDbContext(
            DbContextOptions options,
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
            modelBuilder.HasPostgresExtension("postgis");
            // Add a global filter to exclude deleted elements by default
            modelBuilder.HasGlobalFilter<ISoftDeletableEntity>(x => !x.Deleted);
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
