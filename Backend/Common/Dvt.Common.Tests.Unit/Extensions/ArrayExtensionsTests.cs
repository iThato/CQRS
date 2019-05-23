using Dvt.Common.Extensions;
using Shouldly;
using Xunit;

// ReSharper disable InconsistentNaming

namespace Dvt.Common.Tests.Unit.Extensions
{
    [Trait("Common","Extensions")]
    public class ArrayExtensionsTests
    {
        [Fact]
        public void Union_SupplyTwoArrays_ArraysCombinedLengthShouldEqualNewArrayLength()
        {
            var item = new byte[5];
            var array = new byte[3];
            var result = item.Union(array).Length;
            result.ShouldBe(8);
        }

        [Fact]
        public void Union_SupplyTwoArrays_ArraysCombinedShouldEqualNewArray()
        {
            var item = new byte[] { 1, 2, 3, 4, 5 };
            var array = new byte[] { 6, 7, 8 };
            var newArray = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var result = item.Union(array);
            result.ShouldBe(newArray);
        }

        [Fact]
        public void RemoveFirstBytes_SupplyKnownArray_NewArrayLengthShouldHaveRemovedBytes()
        {
            var item = new byte[5];
            var result = item.RemoveFirstBytes(2).Length;
            result.ShouldBe(3);
        }

        [Fact]
        public void CopyBytes_FixedArray_NewArrayLengthShouldMatchNumberOfBytes()
        {
            var item = new byte[5];
            var result = item.CopyBytes(3).Length;
            result.ShouldBe(3);
        }

        [Fact]
        public void CopyBytes_SupplyKnownValues_NewArrayShouldHaveCorrectValues()
        {
            var item = "edcba".StringToUtf8ByteArray();
            var expectedResult = "edc".StringToUtf8ByteArray();
            var result = item.CopyBytes(3);
            result.ShouldBe(expectedResult);
        }

        [Fact]
        public void IsEqualTo_SupplyIdenticalArrays_ShouldReturnTrue()
        {
            var item = "edcba".StringToUtf8ByteArray();
            var target = "edcba".StringToUtf8ByteArray();
            var result = item.IsEqualTo(target);
            result.ShouldBeTrue();
        }

        [Fact]
        public void NotEqualTo_SupplyNonIdenticalArrays_ShouldReturnTrue()
        {
            var item = "edcba".StringToUtf8ByteArray();
            var target = "edcbe".StringToUtf8ByteArray();
            var result = item.NotEqualTo(target);
            result.ShouldBeTrue();
        }
    }
}
