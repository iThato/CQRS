using System.Linq;
using System.Reflection;
using Autofac;
using Dvt.Common.Extensions;

namespace Dvt.Infrastructure
{
    public static class InfrastructureBootstrap
    {
        public const string FluentValidationClassEnding = "RequestValidator";

        public static void InitialiseScanning(ContainerBuilder builder, string projectAssemlyPrefix, bool useCallingAssembly = false)
        {
            builder.ThrowIfNull(nameof(builder), "Autofac builder must be supplied");
            projectAssemlyPrefix.ThrowIfNullOrEmptyTrimmed(nameof(projectAssemlyPrefix), "The project assembly starting prefix must be supplied");

            Assembly[] assemblies;
            if (useCallingAssembly)//this is for when its called from the unit test project
            {
                assemblies = Assembly
                    .GetCallingAssembly()
                    .GetReferencedAssemblies()
                    .Where(t => t.FullName.ToLower().StartsWith(projectAssemlyPrefix.ToLower()) || t.FullName.ToLower().StartsWith("dvt"))
                    .Select(Assembly.Load)
                    .ToArray();
            }
            else //this is for when its called from everywhere else
            {
                assemblies = Assembly
                    .GetEntryAssembly()
                    .GetReferencedAssemblies()
                    .Where(t => t.FullName.ToLower().StartsWith(projectAssemlyPrefix.ToLower()) || t.FullName.ToLower().StartsWith("dvt"))
                    .Select(Assembly.Load)
                    .ToArray();
            }


            //Set up convention base registration http://autofaccn.readthedocs.io/en/latest/register/scanning.html?highlight=AsImplementedInterfaces
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => !t.Name.EndsWith(
                           FluentValidationClassEnding)) // We will handle FV validators later. IF we do it here we will get the same validators more than once as we resolved via IEnumerable
                .AsImplementedInterfaces();

            HandleFluentValidationImplementations(builder, assemblies);

            //Look for modules that can do overrides.
            builder.RegisterAssemblyModules(assemblies);
        }


        private static void HandleFluentValidationImplementations(ContainerBuilder builder, Assembly[] assemblies)
        {
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith(FluentValidationClassEnding) && t.OnlyHasDefaultConstructor())
                .AsImplementedInterfaces()
                .SingleInstance();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith(FluentValidationClassEnding) && !t.OnlyHasDefaultConstructor())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
