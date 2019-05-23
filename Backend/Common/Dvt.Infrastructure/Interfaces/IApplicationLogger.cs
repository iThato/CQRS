using System;
using System.Diagnostics.CodeAnalysis;

namespace Dvt.Infrastructure.Interfaces
{
    /// <summary>
    ///     Verbose
    ///     Verbose is the noisiest level, rarely(if ever) enabled for a production app.
    ///     Debug
    ///     Debug is used for internal system events that are not necessarily observable from the outside, but useful when
    ///     determining how something happened.
    ///     Information
    ///     Information events describe things happening in the system that correspond to its responsibilities and
    ///     functions.Generally these are the observable actions the system can perform.
    ///     Warning
    ///     When service is degraded, endangered, or may be behaving outside of its expected parameters, Warning level events
    ///     are used.
    ///     Error
    ///     When functionality is unavailable or expectations broken, an Error event is used.
    ///     Fatal
    ///     The most critical level, Fatal events demand immediate attention.
    /// </summary>
    public interface IApplicationLogger
    {
        void Verbose(string messageTemplate);

        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
        void Verbose(string messageTemplate, params object[] arguments);

        void Debug(string messageTemplate);

        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
        void Debug(string messageTemplate, params object[] arguments);

        void Info(string messageTemplate);

        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
        void Info(string messageTemplate, params object[] arguments);

        void Warning(string messageTemplate);

        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
        void Warning(string messageTemplate, params object[] arguments);

        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Error")]
        void Error(string messageTemplate);

        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Error")]
        void Error(string messageTemplate, params object[] arguments);

        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Error")]
        void Error(Exception ex, string messageTemplate, params object[] arguments);

        void Fatal(string messageTemplate);

        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
        void Fatal(string messageTemplate, params object[] arguments);
    }
}
