using System;
using Dvt.Common.Extensions;
using Dvt.Common.TestUtilities;
using Shouldly;
using Xunit;

// ReSharper disable InconsistentNaming
// ReSharper disable ConvertToConstant.Local

namespace Dvt.Common.Tests.Unit.Extensions
{
    [Trait("Common", "Extensions")]
    public class NumberExtensionsTests
    {
        [Fact]
        public void IsOdd_SupplyOddNumber_IsTrue()
        {
            var item = 7;
            var result = item.IsOdd();
            result.ShouldBeTrue();
        }

        [Fact]
        public void IsOdd_SupplyEvenNumber_IsFalse()
        {
            var item = 6;
            var result = item.IsOdd();
            result.ShouldBeFalse();
        }

        [Fact]
        public void IsEven_SupplyEvenNumber_IsTrue()
        {
            var item = 6;
            var result = item.IsEven();
            result.ShouldBeTrue();
        }

        [Fact]
        public void IsEven_SupplyOddNumber_IsFalse()
        {
            var item = 7;
            var result = item.IsEven();
            result.ShouldBeFalse();
        }

        [Fact]
        public void Times_SupplyCounterAndAction_Iterates()
        {
            var loopCounter = 0;
            50.Times(i => loopCounter++);
            loopCounter.ShouldBe(50);
        }

        [Fact]
        public void FromEpochDate_SupplyValidDate_IsCorrect()
        {
            using (ThreadCulture.EnZa())
            {
                var date = 1389865750L.FromEpochDate();
                date.ToString("dd/MM/yyyy HH:mm:ss tt").ShouldBe("16/01/2014 09:49:10 AM");
            }
        }

        [Fact]
        public void FromEpochDate_SupplyNullDate_IsMinDate()
        {
            long? epochDate = null;
// ReSharper disable ExpressionIsAlwaysNull
            var date = epochDate.FromEpochDate();
// ReSharper restore ExpressionIsAlwaysNull
            date.IsMinValue().ShouldBeTrue();
        }

        [Fact]
        public void FromEpochDate_SupplyNullableDate_IsCorrect()
        {
            using (ThreadCulture.EnZa())
            {
                long? epochDate = 1389865750;
                var date = epochDate.FromEpochDate();
                date.ToString("dd/MM/yyyy HH:mm:ss tt").ShouldBe("16/01/2014 09:49:10 AM");
            }
        }

        [Fact]
        public void FromEpochDate_SupplyFutureDate_IsCorrect()
        {
            using (ThreadCulture.EnZa())
            {
                var testDate = new DateTime(3022, 12, 13, 22, 59, 58);
                var epoch = testDate.ToEpochDate();
                var date = epoch.FromEpochDate();
                date.ToString("dd/MM/yyyy HH:mm:ss tt").ShouldBe("13/12/3022 22:59:58 PM");
            }
        }

        [Fact]
        public void ToEpochDate_SupplyDate_IsCorrectEpochDate()
        {
            var testDate = new DateTime(2014, 01, 16, 09, 49, 10);
            var epoch = testDate.ToEpochDate();
            epoch.ShouldBe(1389865750);
        }


        [Theory]
        [InlineData(512L, "512.0 bytes", 1)]
        [InlineData(3234L, "3.2 KB", 1)]
        [InlineData(5442880L, "5.2 MB", 1)]
        [InlineData(5442880000L, "5.1 GB", 1)]
        [InlineData(544288000033333L, "495 TB", 0)]
        [InlineData(5442880L, "5.19 MB", 2)]
        [InlineData(-5442880L, "-5.2 MB", 2)]
        [InlineData(0, "0", 2)]
        public void FileSuffix_SuppyBytes_OutputIsCorrect(long input, string expected, int precision)
        {
            var result = input.SizeSuffix(precision);
            result.ShouldBe(expected);
        }
    }
}
