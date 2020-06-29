using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace projectRootNamespace.Api.Infrastructure.Startup
{
    /// <summary>
    /// Extension methods for setting up core services.
    /// </summary>
    public static class CoreServiceCollectionExtensions
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            // Allow access to the current http context from anywhere. For example
            // we would inject IHttpContextAccessor into a service to get access to
            // the user that is associated with the current http request that 
            // originated the call to the specified service.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add all services in the projectRootNamespace.Api.Services namespace
            // to the DI container as Transient (one instance per request scope).
            // Depending
            RegisterServices(services, ServiceLifetime.Scoped, "projectRootNamespace.Api.Services");
        }

        #region Auxiliar methods

        /// <summary>
        /// Locate and register the services that match the specified namespace.
        /// </summary>
        /// <remarks>
        /// Register services in the target IServiceCollection that match
        /// the namespace and that are located in the current assembly.
        /// </remarks>
        /// <param name="services">The services collection where the target services are to be registered.</param>
        /// <param name="lifetime">Specifies the lifetime of the services.</param>
        /// <param name="ns">The namespace that will be used to match the services to register.</param>
        private static void RegisterServices(IServiceCollection services, ServiceLifetime lifetime, string ns)
        {
            RegisterServices(services, lifetime, type =>
            {
                var tns = type.Namespace;
                var sc = StringComparison.CurrentCulture;
                return !string.IsNullOrEmpty(tns) && tns.StartsWith(ns, sc);
            });
        }

        /// <summary>
        /// Locate and register the services that match the predicate.
        /// </summary>
        /// <remarks>
        /// Register services in the target IServiceCollection that match
        /// the predicate and that are located in the specified assembly.
        /// </remarks>
        /// <param name="services">The services collection where the target services are to be registered.</param>
        /// <param name="lifetime">Specifies the lifetime of the services.</param>
        /// <param name="predicate">The preticate that will be used to match the services to register.</param>
        private static void RegisterServices(IServiceCollection services, ServiceLifetime lifetime, Func<Type, bool> predicate)
        {
            // Load services matching the predicate
            var allServices = Assembly.GetAssembly(typeof(Program)).GetTypes()
                .Where(predicate)
                .Where(x => x.GetTypeInfo().IsClass && !x.GetTypeInfo().IsAbstract && x.GetTypeInfo().IsVisible).ToArray();

            foreach (var type in allServices)
            {
                var allInterfaces = type.GetInterfaces();
                var mainInterfaces = allInterfaces.Except(allInterfaces.SelectMany(x => x.GetInterfaces()));
                foreach (var itype in mainInterfaces)
                {
                    switch (lifetime)
                    {
                        case ServiceLifetime.Scoped:
                            services.AddScoped(itype, type);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(itype, type);
                            break;
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(itype, type);
                            break;
                    }
                }
            }
        }

        #endregion

    }
}
