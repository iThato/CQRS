using System.Globalization;
using System.Threading;

namespace Dvt.Infrastructure
{
    public static class DomainCulture
    {
        public static void SetDefaultDomainCulture()
        {
            var newCulture = (CultureInfo) Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            newCulture.DateTimeFormat.ShortTimePattern = "HH:mm";
            newCulture.DateTimeFormat.DateSeparator = "/";
            newCulture.DateTimeFormat.TimeSeparator = ":";
            newCulture.NumberFormat.CurrencyDecimalSeparator = ".";
            newCulture.NumberFormat.NumberDecimalSeparator = ".";
            newCulture.NumberFormat.PercentDecimalSeparator = ".";
            newCulture.NumberFormat.CurrencySymbol = "R";
            newCulture.NumberFormat.PercentSymbol = "%";
            CultureInfo.DefaultThreadCurrentCulture = newCulture;
        }
    }
}
