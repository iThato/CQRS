using System;
using Dvt.Infrastructure.Interfaces;

namespace Dvt.Infrastructure.Implementation
{
    // NB. This class should always be thread-safe as it can be used as a singleton
    public class ClockSingleton : IClock
    {
        public static DateTime FirstCreatedDate { get; } = DateTime.Now;

        private readonly Func<DateTime> _currentDateFunc = () => DateTime.Now;

        public ClockSingleton(IApiConfiguration configuration)
        {
            if (!configuration.OverrideClock) return;
            var overrideDate = configuration.OverrideClockDate;
            _currentDateFunc = () =>
                                   overrideDate.AddMilliseconds(DateTime.Now.Subtract(FirstCreatedDate).TotalMilliseconds);
        }

        public DateTime NowAsUtc => _currentDateFunc().ToUniversalTime();
        public DateTime NowAsLocal => _currentDateFunc().ToLocalTime();
        public DateTime NowAsSouthAfrican => InternalNowAsSouthAfrican();
        private DateTime InternalNowAsSouthAfrican()
        {
            string SouthAfricanZoneId = "South Africa Standard Time";
            string UtcZoneId = "UTC";
            TimeZoneInfo SouthAfricanZone = TimeZoneInfo.FindSystemTimeZoneById(SouthAfricanZoneId);
            TimeZoneInfo UTCZone = TimeZoneInfo.FindSystemTimeZoneById(UtcZoneId);
            DateTime returnVal = TimeZoneInfo.ConvertTime(NowAsUtc, UTCZone, SouthAfricanZone);
            return returnVal;
        }
    }
}
