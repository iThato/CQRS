using System;
using System.ComponentModel;
using Dvt.Common.Extensions;
using Shouldly;
using Xunit;

// ReSharper disable InconsistentNaming

namespace Dvt.Common.Tests.Unit.Extensions
{
    [Trait("Common", "Extensions")]
    public class EnumExtensionsTests
    {
        public enum TestEnum
        {
            None = 0,
            [Description("My description")]
            Active,
            InactiveStatus
        }

        [Fact]
        public void ParseEnum_ValidIntEnum_ShouldParse()
        {
            var result = 2.ParseEnum(TestEnum.None);
            result.ShouldBe(TestEnum.InactiveStatus);
        }

        [Fact]
        public void ParseEnum_ValidEnum_ShouldParse()
        {
            var result = "Active".ParseEnum(TestEnum.None);
            result.ShouldBe(TestEnum.Active);
        }

        [Fact]
        public void ParseEnum_NotAnEnumEnum_ShouldThrow()
        {
            var exception = Assert.Throws<ArgumentException>(() => "Active".ParseEnum(1));
            exception.Message.ShouldBe("TEnum must be an enumerated type");
        }

        [Fact]
        public void ParseEnum_EmptySource_ShouldReturnDefault()
        {
            var result = "".ParseEnum(TestEnum.Active);
            result.ShouldBe(TestEnum.Active);
        }

        [Fact]
        public void ParseEnum_InvalidIntEnum_ShouldReturnDefault()
        {
            var result = 7.ParseEnum(TestEnum.None);
            result.ShouldBe(TestEnum.None);
        }

        [Fact]
        public void ParseEnum_InvalidEnum_ShouldReturnDefault()
        {
            var result = "BadValue".ParseEnum(TestEnum.None);
            result.ShouldBe(TestEnum.None);
        }

        [Fact]
        public void GetDescription_HasDescriptionAttribute_ShouldReturnCorrectDescription()
        {
            var result = TestEnum.Active.GetDescription();
            result.ShouldBe("My description");
        }

        [Fact]
        public void GetDescription_HasNoDescriptionAttribute_ShouldReturnEnumStringValue()
        {
            var result = TestEnum.InactiveStatus.GetDescription();
            result.ShouldBe("InactiveStatus");
        }
    }
}
