using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using projectRootNamespace.Api.Infrastructure;
using Serilog;

namespace projectRootNamespace.Api
{
    public class Program
    {
        private const string COMPONENT_TITLE = "projectName - Api";
        private const string SERVER_URL = "http://0.0.0.0:5000";
        private const string SERVER_BASE_URL = "api";

        public static int Main(string[] args)
        {
            // Initializes the static class that contains static component information
            ComponentInfo.Initialize(COMPONENT_TITLE, SERVER_URL, SERVER_BASE_URL);

            // Confirm that the componentInfo was initialized correctly
            if (ComponentInfo.Title == null || ComponentInfo.Title.Equals(""))
                throw new Exception("ComponentInfo.Title was not properly initialized");
            if (ComponentInfo.ServerUrl == null || ComponentInfo.ServerUrl.Equals(""))
                throw new Exception("ComponentInfo.ServerUrl was not properly initialized");
            if (ComponentInfo.ServerBaseUrl == null || ComponentInfo.ServerBaseUrl.Equals(""))
                throw new Exception("ComponentInfo.ServerBaseUrl was not properly initialized");
            if (ComponentInfo.ServerBaseUrl != "" && ComponentInfo.ServerBaseUrl.EndsWith('/'))
                throw new Exception("ComponentInfo.ServerBaseUrl cannot end with '/'");
            if (ComponentInfo.ServerBaseUrl != "" && ComponentInfo.ServerBaseUrl.StartsWith('/'))
                throw new Exception("ComponentInfo.ServerBaseUrl cannot start with '/'");

            try
            {
                // Starts the host
                using (var host = CreateHostBuilder(args).Build())
                {
                    var env = (IHostEnvironment)host.Services.GetService(typeof(IHostEnvironment));
                    var log = Log.ForContext<Program>();
                    log.Information(System.Reflection.Assembly.GetEntryAssembly().FullName);
                    log.Information("");
                    log.Information("--------------------------------------------------------------------------------");
                    log.Information(ComponentInfo.Title);
                    log.Information("");
                    log.Information("Api: {endpoint}", ComponentInfo.ServerUrl + "/" + ComponentInfo.ServerBaseUrl);
                    log.Information("Doc: {swagger}", ComponentInfo.ServerUrl + "/" + ComponentInfo.ServerBaseUrl + "/docs/");
                    log.Information("Env: {environment}", env.EnvironmentName);
                    log.Information("--------------------------------------------------------------------------------");
                    log.Information("");
                    log.Information("Starting...");

                    // Starts the application synchronously
                    host.Start();
                    log.Information("Started!");
                    log.Information("Use Ctrl-C to exit...");

                    // Waits for the host to be terminated (using Ctrl-C for example)
                    host.WaitForShutdown();
                    log.Information("");
                    log.Information("So long, and thanks for all the fish!");
                    log.Information("");
                }
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(x =>
            {
                x.UseStartup<Startup>();
                x.UseUrls(SERVER_URL);
                x.UseKestrel();
                x.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom
                    .Configuration(hostingContext.Configuration)
                );
            });
        }
    }
}
