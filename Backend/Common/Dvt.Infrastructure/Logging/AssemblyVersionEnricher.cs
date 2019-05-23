using Dvt.Infrastructure.Structures;
using Serilog.Core;
using Serilog.Events;

namespace Dvt.Infrastructure.Logging
{
    public class AssemblyVersionEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("SmartFastAssemblyVersion", ApplicationVersion.AssemblyVersion));
        }
    }
}
