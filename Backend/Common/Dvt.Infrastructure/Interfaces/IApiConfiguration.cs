using System;

namespace Dvt.Infrastructure.Interfaces
{
    public interface IApiConfiguration
    {
        bool OverrideClock { get; }

        DateTime OverrideClockDate { get; }

        string GetAppSetting(string name, bool exceptionIfSettingNotFound = true);

    }
}
