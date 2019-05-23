using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Dvt.Common.Structures;
using Dvt.Common.Validators;
using Shouldly;
using Xunit;

// ReSharper disable InconsistentNaming

namespace Dvt.Common.Tests.Unit.Validators
{
    [Trait("Common", "Validators")]
    public class DataAnnotationValidatorTests
    {
        private const string RangeError = "Not in range";
        protected class BaseTest
        {
            public string Name { get; set; }
        }
        protected class TestClass : BaseTest
        {
            [Required]
            public string X { get; set; }

            [Required]
            [Range(1, 2, ErrorMessage = RangeError)]
            public int Y { get; set; }
        }

        [Fact]
        public void ValidateDataAnnotations_NoAnnotations_ShouldPass()
        {
            var test = new BaseTest();
            IList<ValidationError> errorList = new List<ValidationError>();
            var result = test.ValidateDataAnnotations(ref errorList);
            result.ShouldBeTrue();
            errorList.Count.ShouldBe(0);
        }

        [Fact]
        public void ValidateDataAnnotations_ValidData_ShouldPass()
        {
            var test = new TestClass
                           {
                               Name = null,
                               X = "a",
                               Y = 1
                           };
            IList<ValidationError> errorList = new List<ValidationError>();
            var result = test.ValidateDataAnnotations(ref errorList);
            result.ShouldBeTrue();
            errorList.Count.ShouldBe(0);
        }

        [Fact]
        public void ValidateDataAnnotations_InvalidData_ShouldFail()
        {
            var test = new TestClass
            {
                Name = null,
                X = "",
                Y = 3
            };
            IList<ValidationError> errorList = new List<ValidationError>();
            var result = test.ValidateDataAnnotations(ref errorList);
            result.ShouldBeFalse();
            errorList.Count.ShouldBe(2);
            errorList.Any(e => e.PropertyName == "X" && e.ErrorMessage.Contains("required")).ShouldBeTrue();
            errorList.Any(e => e.PropertyName == "Y" && e.ErrorMessage == RangeError).ShouldBeTrue();
        }
    }
}
