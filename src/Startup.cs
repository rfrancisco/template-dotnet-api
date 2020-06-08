using Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectName.Api.Infrastructure.Extensions;
using ProjectName.Api.Infrastructure;
using ProjectName.Api.DataAccess;

namespace ProjectName.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime.
        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Load and register app settings in DI container
            services.Configure<Settings>(Configuration.GetSection("Settings"));
            // Standard infrastructure stuff
            services.AddSwagger();
            services.AddEntityFramework(Configuration.GetConnectionString("db"));
            services.AddHealthChecks().AddDbContextCheck<MainDbContext>();
            services.AddControllers();
            services.AddCoreServices();
        }

        // This method gets called by the runtime.
        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsStaging())
                app.UseDeveloperExceptionPage();

            app.UsePathBase("/" + ComponentInfo.ServerBaseUrl);
            app.UseStaticFiles();
            app.UseSerilogRequestLogging();
            app.UseSwagger();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
        }
    }
}
