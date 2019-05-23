using Dvt.Common.Validators;
using Shouldly;
using Xunit;

// ReSharper disable InconsistentNaming

namespace Dvt.Common.Tests.Unit.Validators
{
    [Trait("Common", "Validators")]
    public class SouthAfricanMobileNumberValidatorTests
    {
        [Fact]
        public void IsValid_ValidMobile_ShouldPass()
        {
            new SouthAfricanMobileNumberValidator("0832223456").IsValid.ShouldBeTrue();
        }

        [Fact]
        public void IsValid_OutOfRangeNumber_ShouldFail()
        {
            new SouthAfricanMobileNumberValidator("0232223456").IsValid.ShouldBeFalse();
        }

        [Fact]
        public void IsValid_InvalidCharacter_ShouldFail()
        {
            new SouthAfricanMobileNumberValidator("08322234a6").IsValid.ShouldBeFalse();
        }
    }
}
