using System.Linq;
using Dvt.Common.Validators;
using Shouldly;
using Xunit;

// ReSharper disable InconsistentNaming

namespace Dvt.Common.Tests.Unit.Validators
{
    [Trait("Common", "Validators")]
    public class SouthAfricanIdNumberDetectTests
    {
        [Fact]
        public void DectectIdNumbers_EmptyParameter_ReturnsEmptyList()
        {
            var search = string.Empty;
            var result = SouthAfricanIdNumberValidator.DetectIdNumbers(search);
            result.Count().ShouldBe(0);
        }

        [Fact]
        public void DectectIdNumbers_12Digits_ReturnsEmptyList()
        {
            const string search = "730330676508";
            var result = SouthAfricanIdNumberValidator.DetectIdNumbers(search);
            result.Count().ShouldBe(0);
        }

        [Fact]
        public void DectectIdNumbers_14Digits_ReturnsEmptyList()
        {
            const string search = "73033067650826";
            var result = SouthAfricanIdNumberValidator.DetectIdNumbers(search);
            result.Count().ShouldBe(0);
        }

        [Fact]
        public void DectectIdNumbers_13DigitsOfInvalidIdNumber_ReturnsEmptyList()
        {
            const string search = "7303306765082";
            var result = SouthAfricanIdNumberValidator.DetectIdNumbers(search);
            result.Count().ShouldBe(0);
        }

        [Fact]
        public void DectectIdNumbers_13DigitsOfValidIdNumber_ReturnsListOfOneIdNumber()
        {
            const string search = "7303306765083";
            var result = SouthAfricanIdNumberValidator.DetectIdNumbers(search).ToList();

            result.Count.ShouldBe(1);
            result.First().ShouldBe(search);
        }

        [Fact]
        public void DectectIdNumbers_TextContains13DigitsOfValidIdNumber_ReturnsListOfOneIdNumber()
        {
            const string search = "The following 13 digits 7303306765083 represent my uncles\'s Id";

            var result = SouthAfricanIdNumberValidator.DetectIdNumbers(search).ToList();

            result.Count.ShouldBe(1);
            search.ShouldContain(result.First());
        }

        [Fact]
        public void DectectIdNumbers_TextContains13DigitsOfValidIdNumberSeparatedWithDot_ReturnsListOfOneIdNumber()
        {
            const string search = "Only 13 digits 7303306765083.45 represent my uncles\'s Id whoops I added decimals to confuse you";

            var result = SouthAfricanIdNumberValidator.DetectIdNumbers(search).ToList();

            result.Count.ShouldBe(1);
            search.ShouldContain(result.First());
        }

        [Fact]
        public void DectectIdNumbers_TextStartsWith13DigitsOfValidIdNumber_ReturnsListOfOneIdNumber()
        {
            const string search = "7303306765083.45 this represent my uncles\'s Id whoops I added decimals to confuse you";

            var result = SouthAfricanIdNumberValidator.DetectIdNumbers(search).ToList();

            result.Count.ShouldBe(1);
            search.ShouldContain(result.First());
        }

        [Fact]
        public void DectectIdNumbers_TextContainsFourDuplicateValidIdNumber_ReturnsListOfOneIdNumber()
        {
            var search = "This 7501220027080.45 not this 7303306765083.34 and not this 7303306765083";
            search += "but This 7501220027080.45 not this 7303306765083.34 and not this 7303306765083.";
            search += "\n\n\n";
            search += "These are duplicates there is only 2 sets of unique Id Number";

            var result = SouthAfricanIdNumberValidator.DetectIdNumbers(search).ToList();

            result.Count.ShouldBe(2);

            foreach (var id in result)
            {
                search.ShouldContain(id);
            }
        }

        [Fact]
        public void DectectIdNumbers_LargeTextContainsSeveralValidIdNumbersAndSeveralInvalidIdNumbers_ReturnsListOfValidNumbersOnly()
        {
            var search = "This 7501220027080.45 not this 7303306765089.34 and not this 7303306765086";
            search += "but this 7303306765083.34 and not this 7303306765082. There are only two valid Id numbers";

            var result = SouthAfricanIdNumberValidator.DetectIdNumbers(search).ToList();

            result.Count.ShouldBe(2);
            result.Exists(s=> search.Contains(s)).ShouldBeTrue();
        }

        [Fact]
        public void DectectIdNumbers_LargeTextContainsMultiLinesOfSeveralValidIdNumbersAndSeveralInvalidIdNumbers_ReturnsListOfValidNumbersOnly()
        {
            var search = "This 7501220027080.45 not this 7303306765089.34 and not this 7303306765086";
            search += "but this 7303306765083.34 and not this 7303306765082. There are only two valid Id numbers.";
            search += "\n\n\n\n";
            search += "Actually three is four Id Numbers, just another one at the end.";
            search += "\n\n\n\n";
            search += "\n\n\n\n";
            search += "#@#$%^7303303132089.89";

            var result = SouthAfricanIdNumberValidator.DetectIdNumbers(search).ToList();

            result.Count.ShouldBe(3);
            result.Exists(s => search.Contains(s)).ShouldBeTrue();
        }
    }
}
