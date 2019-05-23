using Autofac;
using Module = Autofac.Module;

namespace Dvt.Features.Core
{
    // Use this module if you need to register something in Features.Core that does not use default conventions or you want a very specific lifetime
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<MySpecificLoader>().As<ILoader>();
        }

    }
}
