using ProjectRootNamespace.Api.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectRootNamespace.Api.Infrastructure.Extensions
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