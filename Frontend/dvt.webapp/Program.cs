using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace wtw.webapp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                                           {
                                               var env = hostingContext.HostingEnvironment;

                                               config.AddJsonFile("appsettings.json", false)
                                                   .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

                                               config.AddEnvironmentVariables();
                                           })
                .ConfigureLogging((hostingContext, config) => { config.ClearProviders(); })
                .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                                .ReadFrom.Configuration(hostingContext.Configuration))
                .Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services.AddAutofac())
                .UseStartup<Startup>();
    }
}
