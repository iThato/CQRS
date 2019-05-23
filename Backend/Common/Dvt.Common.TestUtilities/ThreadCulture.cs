using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using Dvt.Common.Extensions;

namespace Dvt.Common.TestUtilities
{
    internal class ThreadCultureParameters
    {
        public string ShortDatePattern { get; set; }
        public string ShortTimePattern { get; set; }
        public string DateSeparator { get; set; }
        public string TimeSeparator { get; set; }

        public string CurrencySymbol { get; set; }
        public string CurrencyDecimalSeparator { get; set; }
        public int CurrencyDecimalDigits { get; set; }

        public string NumberDecimalSeparator { get; set; }
        public int NumberDecimalDigits { get; set; }

        public string PercentSymbol { get; set; }
        public string PercentDecimalSeparator { get; set; }
        public int PercentDecimalDigits { get; set; }
    }

    public static class ThreadCulture
    {
        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "Wtw.Common.TestUtilities.ThreadCulture+CultureBase")]
        public static void SetDefaultSystemCultureToEnZa()
        {
            var culture = new ThreadCultureParameters
            {
                CurrencyDecimalDigits = 2,
                CurrencyDecimalSeparator = ".",
                CurrencySymbol = "R",
                NumberDecimalDigits = 2,
                NumberDecimalSeparator = ".",
                PercentDecimalDigits = 2,
                PercentDecimalSeparator = ".",
                PercentSymbol = "%",
                ShortDatePattern = "yyyy/MM/dd",
                ShortTimePattern = "HH:mm",
                DateSeparator = "/",
                TimeSeparator = ":"
            };
// ReSharper disable once ObjectCreationAsStatement    Note the creation of the new CultureBase below will force the current thread culture to be set
            new CultureBase("en-za", culture);
            CultureInfo.DefaultThreadCurrentCulture = Thread.CurrentThread.CurrentCulture;
        }

        public static IDisposable EnZa()
        {
            var culture = new ThreadCultureParameters
            {
                CurrencyDecimalDigits = 2,
                CurrencyDecimalSeparator = ".",
                CurrencySymbol = "R",
                NumberDecimalDigits = 2,
                NumberDecimalSeparator = ".",
                PercentDecimalDigits = 2,
                PercentDecimalSeparator = ".",
                PercentSymbol = "%",
                ShortDatePattern = "yyyy/MM/dd",
                ShortTimePattern = "HH:mm",
                DateSeparator = "/",
                TimeSeparator = ":"
            };
            return new CultureDisposable("en-za", culture);
        }

        public static IDisposable EnUs()
        {
            return new CultureDisposable("en-us");
        }

        private class CultureBase
        {
            [SuppressMessage("ReSharper", "FunctionComplexityOverflow")]
            public CultureBase(string culture, ThreadCultureParameters overrideThreadCultureParameters = null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
                if (overrideThreadCultureParameters.IsNull()) return;

// ReSharper disable PossibleNullReferenceException
                if (overrideThreadCultureParameters.ShortDatePattern.NotNullOrEmptyTrimmed())
// ReSharper restore PossibleNullReferenceException
                    Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = overrideThreadCultureParameters.ShortDatePattern;
                if (overrideThreadCultureParameters.DateSeparator.NotNullOrEmptyTrimmed())
                    Thread.CurrentThread.CurrentCulture.DateTimeFormat.DateSeparator = overrideThreadCultureParameters.DateSeparator;
                if (overrideThreadCultureParameters.ShortTimePattern.NotNullOrEmptyTrimmed())
                    Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = overrideThreadCultureParameters.ShortTimePattern;
                if (overrideThreadCultureParameters.TimeSeparator.NotNullOrEmptyTrimmed())
                    Thread.CurrentThread.CurrentCulture.DateTimeFormat.TimeSeparator = overrideThreadCultureParameters.TimeSeparator;

                if (overrideThreadCultureParameters.CurrencySymbol.NotNullOrEmptyTrimmed())
                    Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol = overrideThreadCultureParameters.CurrencySymbol;
                if (overrideThreadCultureParameters.CurrencyDecimalSeparator.NotNullOrEmptyTrimmed())
                    Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = overrideThreadCultureParameters.CurrencyDecimalSeparator;
                if (overrideThreadCultureParameters.CurrencyDecimalDigits > 0)
                    Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalDigits = overrideThreadCultureParameters.CurrencyDecimalDigits;

                //See https://msdn.microsoft.com/en-us/library/system.globalization.numberformatinfo.currencynegativepattern(v=vs.110).aspx
                Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyNegativePattern = 9;
                //See https://msdn.microsoft.com/en-us/library/system.globalization.numberformatinfo.currencypositivepattern(v=vs.110).aspx
                Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyPositivePattern = 2;

                if (overrideThreadCultureParameters.PercentSymbol.NotNullOrEmptyTrimmed())
                    Thread.CurrentThread.CurrentCulture.NumberFormat.PercentSymbol = overrideThreadCultureParameters.PercentSymbol;
                if (overrideThreadCultureParameters.PercentDecimalSeparator.NotNullOrEmptyTrimmed())
                    Thread.CurrentThread.CurrentCulture.NumberFormat.PercentDecimalSeparator = overrideThreadCultureParameters.PercentDecimalSeparator;
                if (overrideThreadCultureParameters.PercentDecimalDigits > 0)
                    Thread.CurrentThread.CurrentCulture.NumberFormat.PercentDecimalDigits = overrideThreadCultureParameters.PercentDecimalDigits;

                if (overrideThreadCultureParameters.NumberDecimalSeparator.NotNullOrEmptyTrimmed())
                    Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = overrideThreadCultureParameters.NumberDecimalSeparator;
                if (overrideThreadCultureParameters.NumberDecimalDigits > 0)
                    Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalDigits = overrideThreadCultureParameters.NumberDecimalDigits;
            }
        }

        private class CultureDisposable : CultureBase, IDisposable
        {
            private readonly CultureInfo _originalCulture;
            private readonly CultureInfo _originalUiCulture;

            public CultureDisposable(string culture, ThreadCultureParameters overrideThreadCultureParameters = null)
                : base(culture, overrideThreadCultureParameters)
            {
                _originalCulture = Thread.CurrentThread.CurrentCulture;
                _originalUiCulture = Thread.CurrentThread.CurrentUICulture;
            }

            public void Dispose()
            {
                Thread.CurrentThread.CurrentCulture = _originalCulture;
                Thread.CurrentThread.CurrentUICulture = _originalUiCulture;
            }
        }
    }
}
