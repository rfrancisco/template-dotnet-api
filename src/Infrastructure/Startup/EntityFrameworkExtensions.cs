using projectRootNamespace.Api.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace projectRootNamespace.Api.Infrastructure.Startup
{
    /// <summary>
    /// Extension methods to setup entity framework
    /// </summary>
    public static class EntityFrameworkServiceCollectionExtensions
    {
        public static void AddEntityFramework(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MainDbContext>(options =>
                options.UseNpgsql(connectionString, options =>
                    options.UseNetTopologySuite()));
        }
    }
}
