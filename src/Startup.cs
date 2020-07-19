using Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using projectRootNamespace.Api.Infrastructure;
using projectRootNamespace.Api.DataAccess;
using projectRootNamespace.Api.Infrastructure.Startup;

namespace projectRootNamespace.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        // This method gets called by the runtime.
        // Use this method to add services to the DI container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Load and register app settings in DI container
            services.Configure<Settings>(Configuration.GetSection("Settings"));
            // Standard infrastructure stuff
            services.AddSwagger();
            services.AddEntityFramework(Configuration.GetConnectionString("Db"));
            services.AddHealthChecks().AddDbContextCheck<MainDbContext>();
            //services.AddLisAuthentication(Configuration);
            //services.AddCustomAuthentication(Configuration);
            services.AddControllers();
            services.AddCustomExceptionHandler();
            services.AddCoreServices(ServiceLifetime.Scoped, "projectRootNamespace.Api.Services");
        }

        // This method gets called by the runtime.
        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsStaging())
                app.UseDeveloperExceptionPage();

            app.UsePathBase("/" + ComponentInfo.ServerBaseUrl);
            app.UseStaticFiles();
            app.UseSerilogRequestLogging();
            app.UseSwagger();
            app.UseRouting();
            app.UseCustomExceptionHandler();
            //app.UseLisAuthentication();
            //app.UseCustomAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
        }
    }
}
