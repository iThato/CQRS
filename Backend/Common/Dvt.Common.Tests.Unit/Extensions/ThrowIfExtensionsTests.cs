using System;
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
    public class ThrowIfExtensionsTests
    {
        [Fact]
        public void IfNull_SupplyNull_ExpectException()
        {
            string item = null;
            var exception = Assert.Throws<ArgumentNullException>(() => item.ThrowIfNull("item"));
            exception.Message.ShouldContain("Parameter name: item");
        }

        [Fact]
        public void IfNull_SupplyNullWithMessage_ExpectExceptionWithMessage()
        {
            string item = null;
            var exception = Assert.Throws<ArgumentNullException>(() => item.ThrowIfNull("item", "This is a test"));
            exception.Message.ShouldStartWith("This is a test");
            exception.Message.ShouldContain("Parameter name: item");
        }

        [Fact]
        public void IfNull_SupplyNotNull_NoException()
        {
            var item = "abc";
            item.ThrowIfNull("item");
            false.ShouldBeFalse();
        }

        [Fact]
        public void IfNullOrEmpty_SupplyEmptyString_ExpectException()
        {
            var item = string.Empty;
            var exception = Assert.Throws<ArgumentNullException>(() => item.ThrowIfNullOrEmpty("item"));
            exception.Message.ShouldContain("Parameter name: item");
        }

        [Fact]
        public void IfNullOrEmpty_SupplyEmptyStringWithMessage_ExpectException()
        {
            var item = string.Empty;
            var exception = Assert.Throws<ArgumentNullException>(() => item.ThrowIfNullOrEmpty("item", "This is a test"));
            exception.Message.ShouldStartWith("This is a test");
            exception.Message.ShouldContain("Parameter name: item");
        }

        [Fact]
        public void IfNullOrEmpty_SupplyString_NoException()
        {
            var item = "Hello";
            item.ThrowIfNullOrEmpty("item");
            true.ShouldBeTrue();
        }

        [Fact]
        public void IfNullOrEmptyTrimmed_SupplyStringWithSpaces_ExpectException()
        {
            var item = "                  ";
            var exception = Assert.Throws<ArgumentNullException>(() => item.ThrowIfNullOrEmptyTrimmed("item"));
            exception.Message.ShouldContain("Parameter name: item");
        }

        [Fact]
        public void IfNullOrEmptyTrimmed_SupplyStringWithSpacesAndMessage_ExpectException()
        {
            var item = "                   ";
            var exception = Assert.Throws<ArgumentNullException>(() => item.ThrowIfNullOrEmptyTrimmed("item", "This is a test"));
            exception.Message.ShouldStartWith("This is a test");
            exception.Message.ShouldContain("Parameter name: item");
        }

        [Fact]

        public void IfNullOrEmptyTrimmed_SupplyStringWithSpacesAndText_ExpectTrue()
        {
            var item = "          f         ";
            item.ThrowIfNullOrEmptyTrimmed("item");
            true.ShouldBeTrue();
        }

        [Fact]
        public void IfFalse_SupplyFalse_ExpectException()
        {
            var item = false;
            var exception = Assert.Throws<ArgumentException>(() => item.ThrowIfFalse("item"));
            exception.Message.ShouldContain("item");
        }

        [Fact]
        public void IfFalse_SupplyTrue_NoException()
        {
            var item = true;
            item.ThrowIfFalse("item");
            true.ShouldBeTrue();
        }

        [Fact]
        public void IfFalse_SupplyFalseWithMessage_ExpectException()
        {
            var item = false;
            var exception = Assert.Throws<ArgumentException>(() => item.ThrowIfFalse("item", "This is a test"));
            exception.Message.ShouldContain("This is a test");
            exception.Message.ShouldContain("item");
        }

        [Fact]
        public void IfTrue_SupplyTrue_ExpectException()
        {
            var item = true;
            var exception = Assert.Throws<ArgumentException>(() => item.ThrowIfTrue("item"));
            exception.Message.ShouldContain("item");
        }

        [Fact]
        public void IfTrue_SupplyTrueWithMessage_ExpectException()
        {
            var item = true;
            var exception = Assert.Throws<ArgumentException>(() => item.ThrowIfTrue("item", "This is a test"));
            exception.Message.ShouldContain("This is a test");
            exception.Message.ShouldContain("item");
        }

        [Fact]
        public void IfTrue_SupplyFalse_ExpectTrue()
        {
            var item = false;
            item.ThrowIfTrue("item");
           true.ShouldBeTrue();
        }
    }
}
