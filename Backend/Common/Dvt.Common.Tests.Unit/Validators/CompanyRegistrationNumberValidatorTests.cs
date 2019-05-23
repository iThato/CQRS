using Dvt.Common.Validators;
using Shouldly;
using Xunit;

// ReSharper disable InconsistentNaming

namespace Dvt.Common.Tests.Unit.Validators
{
    [Trait("Common", "Validators")]
    public class CompanyRegistrationNumberValidatorTests
    {
        [Fact]
        public void IsValid_ValidCompanyVariation1_ShouldPass()
        {
            new CompanyRegistrationNumberValidator("123456789").IsValid.ShouldBeTrue();
        }

        [Fact]
        public void IsValid_ValidCompanyVariation2_ShouldPass()
        {
            new CompanyRegistrationNumberValidator("1234567890").IsValid.ShouldBeTrue();
        }

        [Fact]
        public void IsValid_ValidCompanyVariation3_ShouldPass()
        {
            new CompanyRegistrationNumberValidator("123456789012").IsValid.ShouldBeTrue();
        }

        [Fact]
        public void IsValid_InvalidLength_ShouldBeFalse()
        {
            new CompanyRegistrationNumberValidator("1234567890123").IsValid.ShouldBeFalse();
        }

        [Fact]
        public void IsValid_InvalidChars_ShouldBeFalse()
        {
            new CompanyRegistrationNumberValidator("1234567a9012").IsValid.ShouldBeFalse();
        }

        [Fact]
        public void IsValid_NewCompanyFormat_ShouldBeTrue()
        {
            new CompanyRegistrationNumberValidator("2009/021540/07").IsValid.ShouldBeTrue();
        }

        [Fact]
        public void IsValid_NewCompanyFormatIncorrect_ShouldBeFalse()
        {
            new CompanyRegistrationNumberValidator("2009/02154/07").IsValid.ShouldBeTrue();
        }
    }
}
