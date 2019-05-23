using Dvt.Common.Extensions;
using Shouldly;
using Xunit;

// ReSharper disable InconsistentNaming
// ReSharper disable ExpressionIsAlwaysNull
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable ConvertToConstant.Local

namespace Dvt.Common.Tests.Unit.Extensions
{
    [Trait("Common", "Extensions")]
    public class ObjectExtensionsTests
    {
        [Fact]
        public void IsNull_SupplyNull_IsTrue()
        {
            string item = null;
            var result = item.IsNull();
            result.ShouldBeTrue();
        }

        [Fact]
        public void IsNull_SupplyNonNull_IsFalse()
        {
            var item = "abc";
            var result = item.IsNull();
            result.ShouldBeFalse();
        }

        [Fact]
        public void NotNull_SupplyNonNull_IsTrue()
        {
            var item = "abc";
            var result = item.NotNull();
            result.ShouldBeTrue();
        }

        [Fact]
        public void NotNull_SupplyNull_IsFalse()
        {
            string item = null;
            var result = item.NotNull();
            result.ShouldBeFalse();
        }
    }
}
