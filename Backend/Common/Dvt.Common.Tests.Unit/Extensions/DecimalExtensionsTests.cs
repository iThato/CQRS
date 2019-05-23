using Dvt.Common.Extensions;
using Dvt.Common.TestUtilities;
using Shouldly;
using Xunit;

// ReSharper disable InconsistentNaming
// ReSharper disable ConvertToConstant.Local
// ReSharper disable ExpressionIsAlwaysNull

namespace Dvt.Common.Tests.Unit.Extensions
{
    [Trait("Common", "Extensions")]
    public class DecimalExtensionsTests
    {
        [Fact]
        public void RoundTo_PositiveDecimals_RoundsCorrectly()
        {
            var total = 27.24579891M;
            var result = total.RoundTo(2);
            result.ShouldBe(27.25M);
        }

        [Fact]
        public void RoundTo_NoDecimal_NoFraction()
        {
            var total = 27.45421M;
            var result = total.RoundTo(0);
            result.ShouldBe(27M);
        }

        [Fact]
        public void CalculatePercentage_PerformCalc_CorrectResult()
        {
            var left = 27.24579891M;
            var right = 27m;
            var result = left.CalculatePercentage(right);
            result.RoundTo(2).ShouldBe(100.91m);
        }

        [Fact]
        public void CalculatePercentage_SupplyNegativeValues_CorrectResult()
        {
            var left = -90M;
            var right = -100m;
            var result = left.CalculatePercentage(right);
            result.RoundTo(2).ShouldBe(90m);
        }

        [Fact]
        public void CalculatePercentage_DivideByZero_ResultIsZero()
        {
            var left = 27.24579891M;
            var right = 0m;
            var result = left.CalculatePercentage(right);
            result.ShouldBe(0m);
        }

        [Fact]
        public void CalculatePercentageNullable_PerformCalc_CorrectResult()
        {
            decimal? left = 27.24579891M;
            decimal? right = 27m;
            var result = left.CalculatePercentage(right);
            result.RoundTo(2).ShouldBe(100.91m);
        }

        [Fact]
        public void CalculatePercentageNullable_DivideByZero_ResultIsZero()
        {
            decimal? left = 27.24579891M;
            decimal? right = 0m;
            var result = left.CalculatePercentage(right);
            result.ShouldBe(0m);
        }

        [Fact]
        public void CalculatePercentageNullable_LeftValueNull_ResultIsZero()
        {
            decimal? left = null;
            decimal? right = 1.1m;

            var result = left.CalculatePercentage(right);
            result.ShouldBe(0m);
        }

        [Fact]
        public void CalculatePercentageNullableRightValueNull_ResultIsZero()
        {
            decimal? left = 27.24579891M;
            decimal? right = null;
            var result = left.CalculatePercentage(right);
            result.ShouldBe(0m);
        }

        [Fact]
        public void EmptyIfZero_ResultIsNull()
        {
            var value = 0.00M;
            var result = value.NullIfZero();
            result.ShouldBeNull();
        }

        [Fact]
        public void ReturnValueIfGreaterThanZero_CorrectResult()
        {
            var value = 2m;
            var result = value.NullIfZero();
            result.ShouldBe("2");
        }

        [Fact]
        public void ToCurrencyWithPositiveValue_CorrectResult()
        {
            using (ThreadCulture.EnZa())
            {
                var value = 3456212.334m;
                var result = value.ToCurrency();
                result.ShouldBe("R 3 456 212.33");
            }
        }

        [Fact]
        public void ToCurrencyWithNegativeValue_CorrectResult()
        {
            using (ThreadCulture.EnZa())
            {
                var value = -3456212.334m;
                var result = value.ToCurrency();
                result.ShouldBe("-R 3 456 212.33");
            }
        }

        [Fact]
        public void ToCurrencyWithZeroValue_CorrectResult()
        {
            using (ThreadCulture.EnZa())
            {
                var value = -0m;
                var result = value.ToCurrency();
                result.ShouldBe("R 0.00");
            }
        }
    }
}
