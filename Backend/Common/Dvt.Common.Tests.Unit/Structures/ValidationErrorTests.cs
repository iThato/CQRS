using Dvt.Common.Structures;
using Shouldly;
using Xunit;

// ReSharper disable InconsistentNaming

namespace Dvt.Common.Tests.Unit.Structures
{
    [Trait("Common", "Structures")]
    public class ValidationErrorTests
    {
        [Fact]
        public void ValidationError_CreateWithMessageOnly_ShouldOnlySetMessage()
        {
            var result = new ValidationError("boom");
            result.ErrorMessage.ShouldBe("boom");
            result.PropertyName.ShouldBe("");
        }

        [Fact]
        public void ValidationError_CreateWithPropertyAndMessage_ShouldSetBothProperties()
        {
            var result = new ValidationError("prop", "boom");
            result.ErrorMessage.ShouldBe("boom");
            result.PropertyName.ShouldBe("prop");
        }

        [Fact]
        public void ValidationError_CreateWithParameters_ShouldFormatCorrectly()
        {
            var result = new ValidationError("prop", "boom {0} - {1}", 1, "A");
            result.ErrorMessage.ShouldBe("boom 1 - A");
            result.PropertyName.ShouldBe("prop");
        }

        [Fact]
        public void ValidationError_VerifyToString_ShouldBeValid()
        {
            var result = new ValidationError("prop", "boom");
            result.ToString().ShouldBe("PropertyName: prop, ErrorMessage: boom");
        }

        [Fact]
        public void ValidationError_Create_ShouldHaveNothingSets()
        {
            var result = new ValidationError();
            result.ErrorMessage.ShouldBeNull();
            result.PropertyName.ShouldBeNull();
        }
    }
}

