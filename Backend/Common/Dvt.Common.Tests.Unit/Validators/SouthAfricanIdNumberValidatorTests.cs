using Dvt.Common.Validators;
using Shouldly;
using Xunit;

// ReSharper disable InconsistentNaming

namespace Dvt.Common.Tests.Unit.Validators
{
    [Trait("Common", "Validators")]
    public class SouthAfricanIdNumberValidatorTests
    {
        [Fact]
        public void IsValid_ValidId_Gender_ValidIdNumber_ShouldBeTrue()
        {
            var validator = new SouthAfricanIdNumberValidator("7501220027080");
            validator.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void IsValid_InvalidIdWrongCheckDigit_ShouldBeFalse()
        {
            var validator = new SouthAfricanIdNumberValidator("7303306765082");
            validator.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void IsValid_InvalidIdAlphaChars_ShouldBeFalse()
        {
            var validator = new SouthAfricanIdNumberValidator("73033067a5082");
            validator.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void IsValid_AllZeros_ShouldBeFalse()
        {
            var validator = new SouthAfricanIdNumberValidator("0000000000000");
            validator.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void IsValid_SupplyNull_ShouldBeFalse()
        {
            var validator = new SouthAfricanIdNumberValidator(null);
            validator.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void IsValid_WrongMonthBefore01_ShouldBeFalse()
        {
            var validator = new SouthAfricanIdNumberValidator("7500220027082");
            validator.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void IsValid_WrongMonthAfter12_ShouldBeFalse()
        {
            var validator = new SouthAfricanIdNumberValidator("7513220027085");
            validator.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void IsValid_WronDayBefore01_ShouldBeFalse()
        {
            var validator = new SouthAfricanIdNumberValidator("7501000027086");
            validator.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void IsValid_WrongDayAfter31_ShouldBeFalse()
        {
            var validator = new SouthAfricanIdNumberValidator("7501320027089");
            validator.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void IsValid_InvalidIdWrongLength_ShouldBeFalse()
        {
            var validator = new SouthAfricanIdNumberValidator("7500220027080");
            validator.IsValid.ShouldBeFalse();
        }


        [Fact]
        public void IsValid_InvalidIdNotAllNumeric_ShouldBeFalse()
        {
            var validator = new SouthAfricanIdNumberValidator("73033067650a2");
            validator.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void IsValid_ValidIdNumberWithTrailingSpaces_ShouldBeTrimmedAndPass()
        {
            var validator = new SouthAfricanIdNumberValidator("7303306765083   ");
            validator.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void IsSouthAfrican_ValidIdNumber_ShouldBeTrue()
        {
            var validator = new SouthAfricanIdNumberValidator("7303306765083");
            validator.IsSouthAfrican.ShouldBeTrue();
        }

        [Fact]
        public void Gender_MaleIdNumber_ShouldBeTrue()
        {
            var validator = new SouthAfricanIdNumberValidator("7303306765083");
            validator.IsMale.ShouldBeTrue();
            validator.IsFemale.ShouldBeFalse();
        }

        [Fact]
        public void Gender_FemaleIdNumber_ShouldBeTrue()
        {
            var validator = new SouthAfricanIdNumberValidator("7303303132089");
            validator.IsFemale.ShouldBeTrue();
            validator.IsMale.ShouldBeFalse();
        }
    }
}
