using System.Reflection;
using Autofac;
using Dvt.Common.Extensions;
using Dvt.Infrastructure.Behaviours;
using Dvt.Infrastructure.Implementation;
using Dvt.Infrastructure.Interfaces;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Serilog;
using Module = Autofac.Module;

namespace Dvt.Infrastructure
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterMediatr(builder);
            RegisterInfrastructureSingletons(builder);
            SetupFluentValidation();
        }

        private static void RegisterMediatr(ContainerBuilder builder)
        {
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            // Last registered will be called first
            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(DomainEventDispatcherPostProcessoBehavior<,>)).As(typeof(IRequestPostProcessor<,>));
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(SecurityBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(SeriLogRequestLoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            builder.Register<ServiceFactory>(ctx =>
                                             {
                                                 var c = ctx.Resolve<IComponentContext>();
                                                 return t => c.Resolve(t);
                                             });
        }

        private static void RegisterInfrastructureSingletons(ContainerBuilder builder)
        {
            builder.RegisterType<ClockSingleton>().As<IClock>().SingleInstance();
            builder.RegisterType<ApiConfigurationSingleton>().As<IApiConfiguration>().SingleInstance();
            builder.RegisterType<SeriLoggerSingleton>().As<IApplicationLogger>().SingleInstance();
            builder.RegisterInstance(Log.Logger).As<ILogger>().ExternallyOwned();
        }


        /* We use a convention of TransferObject on all mediator inputs. We catch it here so that if FV
         generates a property name for an error message we can remove the 'TransferObject' from the name
        */
        private static void SetupFluentValidation()
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;
            ValidatorOptions.PropertyNameResolver = (type, member, expr) =>
                                                    {
                                                        if (member.IsNull()) return null;
                                                        const string transferobject = "transferobject";
                                                        return member.Name.ToLower().StartsWith(transferobject)
                                                            ? member.Name.Remove(0, transferobject.Length)
                                                            : member.Name;
                                                    };
        }
    }
}
