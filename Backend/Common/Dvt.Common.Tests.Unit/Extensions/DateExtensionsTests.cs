using System;
using Dvt.Common.Extensions;
using Dvt.Common.TestUtilities;
using Shouldly;
using Xunit;

// ReSharper disable InconsistentNaming

namespace Dvt.Common.Tests.Unit.Extensions
{
    [Trait("Common", "Extensions")]
    public class DateExtensionsTests
    {
        [Fact]
        public void IsMinValue_SupplyMinValue_IsTrue()
        {
            var date = new DateTime(0001, 01, 01);
            var result = date.IsMinValue();
            result.ShouldBeTrue();
        }

        [Fact]
        public void IsMinValue_SupplyNonMinValue_IsFalse()
        {
            var date = new DateTime(2011, 05, 10);
            var result = date.IsMinValue();
            result.ShouldBeFalse();
        }

        [Fact]
        public void IsMaxValue_SupplyNonMaxValue_IsFalse()
        {
            var date = new DateTime(1988, 04, 03);
            var result = date.IsMaxValue();
            result.ShouldBeFalse();
        }

        [Fact]
        public void IsMaxValue_SupplyMaxValue_IsTrue()
        {
            var date = DateTime.MaxValue;
            var result = date.IsMaxValue();
            result.ShouldBeTrue();
        }

        [Fact]
        public void IsMinOrMaxValue_SupplyMinValue_IsTrue()
        {
            var date = new DateTime(0001, 01, 01);
            var result = date.IsMinOrMaxValue();
            result.ShouldBeTrue();
        }

        [Fact]
        public void IsMinOrMaxValue_SupplyMaxValue_IsTrue()
        {
            var date = DateTime.MaxValue;
            var result = date.IsMinOrMaxValue();
            result.ShouldBeTrue();
        }

        [Fact]
        public void IsMinOrMaxValue_SupplyNonMinOrMaxValue_IsFalse()
        {
            var date = new DateTime(1984, 12, 03);
            var result = date.IsMinOrMaxValue();
            result.ShouldBeFalse();
        }

        [Fact]
        public void EndOfMonth_SupplyDate_ReturnEndOfMonth()
        {
            var date = new DateTime(2012, 09, 10);
            var result = date.EndOfMonth();
            result.ShouldBe(new DateTime(2012, 09, 30));
        }

        [Fact]
        public void EndOfMonth_SupplyLeapYearDate_ReturnEndOfMonth()
        {
            var date = new DateTime(2012, 02, 06);
            var result = date.EndOfMonth();
            result.ShouldBe(new DateTime(2012, 02, 29));
        }

        [Fact]
        public void StartOfMonth_SupplyDate_ReturnStartOfMonth()
        {
            var date = new DateTime(2012, 09, 10);
            var result = date.StartOfMonth();
            result.ShouldBe(new DateTime(2012, 09, 01));
        }

        [Fact]
        public void IsWeekend_SupplyWeekend_IsTrue()
        {
            var date = new DateTime(2012, 09, 09);
            var result = date.IsWeekend();
            result.ShouldBeTrue();
        }

        [Fact]
        public void IsWeekend_SupplyNonWeekend_IsFalse()
        {
            var date = new DateTime(2012, 09, 10);
            var result = date.IsWeekend();
            result.ShouldBeFalse();
        }

        [Fact]
        public void IsLeapYear_SupplyLeapYear_IsTrue()
        {
            var date = new DateTime(2012, 09, 10);
            var result = date.IsLeapYear();
            result.ShouldBeTrue();
        }

        [Fact]
        public void IsLeapYear_SupplyNonLeapYear_IsFalse()
        {
            var date = new DateTime(2011, 09, 10);
            var result = date.IsLeapYear();
            result.ShouldBeFalse();
        }

        [Fact]
        public void CalculateAge_BeforeBirthDay_ShouldMatch()
        {
            using (ThreadCulture.EnZa())
            {
                //var age = DateExtensions.CalculateAge(new DateTime(2014, 04, 10), new DateTime(2000, 01, 01));
                //age.ShouldBe(14);
            }
        }

        [Fact]
        public void CalculateAge_JustBeforeBirthDay_ShouldMatch()
        {
            using (ThreadCulture.EnZa())
            {
                //var age = DateExtensions.CalculateAge(new DateTime(2014, 04, 08), new DateTime(2000, 04, 09));
                //age.ShouldBe(13);
            }
        }

        [Fact]
        public void CalculateAge_OnBirthDay_ShouldMatch()
        {
            using (ThreadCulture.EnZa())
            {
                //var age = DateExtensions.CalculateAge(new DateTime(2014, 04, 09), new DateTime(2000, 04, 09));
                //age.ShouldBe(14);
            }
        }

        [Fact]
        public void CalculateAge_UseActualMethod_ShouldBeGreaterThan()
        {
            using (ThreadCulture.EnZa())
            {
                var age = new DateTime(2000, 04, 09).CalculateAge();
                age.ShouldBeGreaterThan(10);
            }
        }


        [Fact]
        public void ToIso8601_SupplyDateAndTime_ShouldBeIsoFormat()
        {
            var date = DateTime.Now;
            var result = date.ToIso8601();
            result.ShouldBe(date.ToString(DateExtensions.FormatIso8601));
        }

        [Fact]
        public void ToIso8601_SupplyDateAndTime_ShouldStripTime()
        {
            var date = DateTime.Now;
            var result = date.ToIso8601(true);
            result.ShouldBe(date.Date.ToString(DateExtensions.FormatIso8601));
        }

        [Fact]
        public void ToIso8601Date_SupplyDateAndTime_ShouldBeIsoDateFormat()
        {
            var date = DateTime.Now;
            var result = date.ToIso8601Date();
            result.ShouldBe(date.ToString(DateExtensions.FormatIso8601Date));
        }

        [Fact]
        public void ZeroMilliseconds_SupplyDateAndTime_MillisecondsShouldBeTruncated()
        {
            var date = DateTime.Now;
            var result = date.ZeroMilliseconds();
            result.Millisecond.ShouldBe(0);
        }
    }
}
