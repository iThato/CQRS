using Dvt.Common.Extensions;
using Shouldly;
using Xunit;

// ReSharper disable InconsistentNaming
// ReSharper disable ConvertToConstant.Local
// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace Dvt.Common.Tests.Unit.Extensions
{
    [Trait("Common", "Extensions")]
    public class BooleanExtensionsTests
    {
        [Fact]
        public void ToYesNo_SupplyTrue_IsYes()
        {
            var result = true.ToYesNo();
            result.ShouldBe("Yes");
        }

        [Fact]
        public void ToYesNo_SupplyFalse_IsNo()
        {
            var result = false.ToYesNo();
            result.ShouldBe("No");
        }

        [Fact]
        public void ToBool_BoolHasWhiteSpaceAndCapitals_IsTrue()
        {
            var item = "True  ";
            var result = item.ToBool();
            result.ShouldBeTrue();
        }

        [Fact]
        public void ToBool_EmptySource_IsFalse()
        {
            var item = "";
            var result = item.ToBool();
            result.ShouldBeFalse();
        }

        [Fact]
        public void ToBool_NotBool_IsFalse()
        {
            var item = "Hello";
            var result = item.ToBool();
            result.ShouldBeFalse();
        }

        [Fact]
        public void ToBool_SupplyNumberBoolValue_IsTrue()
        {
            var item = "1";
            var result = item.ToBool();
            result.ShouldBeTrue();
        }

        [Fact]
        public void ToBool_SupplyNumberNonBoolValue_IsFalse()
        {
            var item = "7";
            var result = item.ToBool();
            result.ShouldBeFalse();
        }

        [Fact]
        public void ToBool_SupplyTrue_ReturnOne()
        {
            var item = true;

            var result = item.ToByte();

            result.ShouldBe((byte)1);
        }

        [Fact]
        public void ToBool_SupplyFalse_ReturnZero()
        {
            var item = false;
            var result = item.ToByte();
            result.ShouldBe((byte)0);
        }
    }
}
