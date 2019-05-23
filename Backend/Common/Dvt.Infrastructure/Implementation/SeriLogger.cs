using System;
using Dvt.Infrastructure.Interfaces;
using Serilog;

namespace Dvt.Infrastructure.Implementation
{
    // NB!! This class must be thread-safe as it is registered in IOC as a singleton
    public class SeriLogger : IApplicationLogger
    {
        public SeriLogger(ILogger logger)
        {
            Logger = logger;
        }
        //    Verbose
        //        Verbose is the noisiest level, rarely(if ever) enabled for a production app.
        //    Debug
        //        Debug is used for internal system events that are not necessarily observable from the outside, but useful when determining how something happened.
        //    Information
        //        Information events describe things happening in the system that correspond to its responsibilities and functions.Generally these are the observable actions the system can perform.
        //    Warning
        //        When service is degraded, endangered, or may be behaving outside of its expected parameters, Warning level events are used.
        //    Error
        //        When functionality is unavailable or expectations broken, an Error event is used.
        //    Fatal
        //        The most critical level, Fatal events demand immediate attention.

        public ILogger Logger { get; }

        public void Debug(string messageTemplate)
        {
            Logger.Debug(messageTemplate);
        }

        public void Debug(string messageTemplate, params object[] arguments)
        {
            Logger.Debug(messageTemplate, arguments);
        }

        public void Info(string messageTemplate)
        {
            Logger.Information(messageTemplate);
        }

        public void Info(string messageTemplate, params object[] arguments)
        {
            Logger.Information(messageTemplate, arguments);
        }

        public void Warning(string messageTemplate)
        {
            Logger.Warning(messageTemplate);
        }

        public void Warning(string messageTemplate, params object[] arguments)
        {
            Logger.Warning(messageTemplate, arguments);
        }

        public void Error(string messageTemplate)
        {
            Logger.Error(messageTemplate);
        }

        public void Error(string messageTemplate, params object[] arguments)
        {
            Logger.Error(messageTemplate, arguments);
        }

        public void Error(Exception ex, string messageTemplate, params object[] arguments)
        {
            Logger.Error(messageTemplate, arguments);
            Logger.Error("{@Exception}", ex);
        }

        public void Fatal(string messageTemplate)
        {
            Logger.Fatal(messageTemplate);
        }

        public void Fatal(string messageTemplate, params object[] arguments)
        {
            Logger.Fatal(messageTemplate, arguments);
        }

        public void Verbose(string messageTemplate)
        {
            Logger.Verbose(messageTemplate);
        }

        public void Verbose(string messageTemplate, params object[] arguments)
        {
            Logger.Verbose(messageTemplate, arguments);
        }
    }
}
