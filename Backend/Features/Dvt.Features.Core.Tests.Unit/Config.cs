using Autofac;
using Autofac.Extensions.DependencyInjection;
using Dvt.Features.Core.Entities;
using Dvt.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using wtw.webapp.Controllers;
//using wtw.webapp.Controllers;

namespace Dvt.Features.Core.Tests.Unit
{

    public class Config
    {
        public const string FluentValidationClassEnding = "RequestValidator";
        private const string _appPrefix = "Dvt";
        public static IConfigurationRoot GetConfigurationRoot()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configbuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables();

            return configbuilder.Build();
        }

        public static IMediator GetMediator()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(typeof(DvtDatabaseContext).Assembly).AsImplementedInterfaces();

            var config = GetConfigurationRoot();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<DvtDatabaseContext>(options =>
                                                                  options.UseSqlServer(config.GetConnectionString("DvtConnection")));
            serviceCollection.AddAutofac();
            serviceCollection.AddSingleton<IConfiguration>(config);

            InfrastructureBootstrap.InitialiseScanning(builder, _appPrefix, true);
            builder.Populate(serviceCollection);

            builder.RegisterType<CourseController>()
            .As<CourseController>();

            return builder.Build().Resolve<IMediator>();
        }

    }
}
