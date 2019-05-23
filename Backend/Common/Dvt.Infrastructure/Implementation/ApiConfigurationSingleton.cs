using System;
using System.Diagnostics.CodeAnalysis;
using Dvt.Common.Extensions;
using Dvt.Infrastructure.Constants;
using Dvt.Infrastructure.Exceptions;
using Dvt.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;

// ReSharper disable UnusedMember.Local

namespace Dvt.Infrastructure.Implementation
{
    // NB. This class should always be thread-safe as it can be used as a singleton
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1404:CodeAnalysisSuppressionMustHaveJustification",
        Justification = "Reviewed. Suppression is OK here.")]
    [SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces", Justification = "This will only be used via an IOC container")]
    public class ApiConfigurationSingleton : IApiConfiguration
    {
        
        public ApiConfigurationSingleton(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private bool? _overrideClock;
        public bool OverrideClock
        {
            get
            {
                if (_overrideClock.HasValue) return _overrideClock.Value;

                _overrideClock= GetAppSettingAsBool("OverrideClock", false);
                return _overrideClock.Value;
            }
        }

        private DateTime? _overrideClockDate;
        public DateTime OverrideClockDate
        {
            get
            {
                if (_overrideClockDate.HasValue) return _overrideClockDate.Value;

                if (!DateTime.TryParse(GetAppSetting("OverrideClockDate", false), out var overrideClockDate))
                    overrideClockDate = DateTime.Now;
                _overrideClockDate = overrideClockDate;
                return _overrideClockDate.Value;
            }
        }

        public string GetAppSetting(string name, bool exceptionIfSettingNotFound = true)
        {
            var appSetting = Configuration[name];
            if (appSetting.IsNullOrEmptyTrimmed() && exceptionIfSettingNotFound)
                throw new ApiException(ConstantsConfiguration.ConfigurationError(name), ApiExceptionCode.ConfigurationError);

            return appSetting;
        }

        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "bool")]
        private bool GetAppSettingAsBool(string name, bool exceptionIfSettingNotFound = true)
        {
            var appSetting = GetAppSetting(name, exceptionIfSettingNotFound);

            if (!bool.TryParse(appSetting, out var result))
                return false;

            return result;
        }

        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "int")]
        private int GetAppSettingAsInt(string name, bool exceptionIfSettingNotFound = true)
        {
            var appSetting = GetAppSetting(name, exceptionIfSettingNotFound);

            if (!int.TryParse(appSetting, out var result))
                return 0;

            return result;
        }
    }
}
