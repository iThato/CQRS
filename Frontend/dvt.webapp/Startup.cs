using System;
using System.IO;
using System.Text;
using Autofac;
using dvt.webapp.AppCode;
using dvt.webapp.Helpers;
using Dvt.Features.Core.Entities;
using Dvt.Features.Core.Features.Notification.Command;
using Dvt.Features.Core.Hangfire;
using Dvt.Infrastructure;
using Dvt.Infrastructure.Hangfire;
using Dvt.Infrastructure.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Hangfire;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using wtw.webapp.AppCode.SwaggerFilters;

namespace wtw.webapp
{
    public class Startup
    {
        private const string _appPrefix = "Dvts";
        private const string _apiName = _appPrefix + " API";
        private const string _apiVersion = "v1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Dependency Injection. specifying services that are going to be required to run our application

            //Registering the Context as a service in the IServiceCollection.
            services.AddDbContext<DvtDatabaseContext>(options =>
                                                  options.UseSqlServer(Configuration.GetConnectionString("DvtConnection")));

            //A way to register dependencies or services(implement dependency injection).
            services.AddTransient<JobHandler>();

            //Used to build policies. that are invloved throught the entire project.
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            // define the type oof storage your using..
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DvtConnection")));

            // regestering Mvc service or Dependency Injection. 
            services.AddMvc(options => { options.Filters.Add(new AuthorizeFilter(policy)); }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation().AddControllersAsServices();

            // regestering SwaggerGen service or Dependency Injection. 
            services.AddSwaggerGen(c =>
                                   {
                                       c.SwaggerDoc("v1", new Info { Title = _apiName, Version = _apiVersion });

                                       c.IncludeXmlComments(GetApiXmlCommentsFile());
                                       c.IncludeXmlComments(GetMessagesXmlCommentsFile());

                                       c.DescribeAllEnumsAsStrings();
                                       c.IgnoreObsoleteActions();
                                       c.IgnoreObsoleteProperties();
                                       c.OperationFilter<AuthorisationKeyHeaderOperationFilter>();
                                   });

            //If the app doesn't use the Microsoft.AspNetCore.App metapackage ASP.NET Core Identity
            // registering Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtBearerOptions =>
                              {
                                  jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                                  {
                                      ValidateActor = true,
                                      ValidateAudience = true,
                                      ValidateLifetime = true,
                                      ValidateIssuerSigningKey = true,
                                      ValidIssuer = Configuration["Issuer"],
                                      ValidAudience = Configuration["Audience"],
                                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SigningKey"]))
                                  };
                              });



            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {   

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
          
            //Add Hangfire
            GlobalConfiguration.Configuration.UseActivator(new HangfireActivator(serviceProvider));
           

            app.UseHangfireServer();// specifying that we gonna use hangfire service. And saying that this App is gonna perform the background jobs.
            //followed by the Dashboard which is Web UI that shows all jobs that being used, scheduled.
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireDashboardAuthorizationFilter() }
            });

            app.UseSignalR(options =>
                           {
                               options.MapHub<MessagingHubCommandHandler>("/hub");
                           });

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            if (env.IsDevelopment())
                app.UseMiddleware<StackifyMiddleware.RequestTracerMiddleware>();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
                             {
                                 c.SwaggerEndpoint("/swagger/v1/swagger.json", _apiName + " " + _apiVersion);
                                 c.EnableValidator(null); // Disable validation as it makes external calls
                             });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

        }
        private void OnShutdown()
        {
            Log.CloseAndFlush();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<WebClaimsPrincipal>().As<IPrincipal>();
            InfrastructureBootstrap.InitialiseScanning(builder, _appPrefix);
        }


        private string GetApiXmlCommentsFile()
        {
            return Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "wtw.webapp.xml");
        }

        private string GetMessagesXmlCommentsFile()
        {
            return Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Dvt.Features.Messages.xml");
        }
    }
}
