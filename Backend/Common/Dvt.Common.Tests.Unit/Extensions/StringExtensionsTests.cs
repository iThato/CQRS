using System;
using System.Text;
using Dvt.Common.Extensions;
using Shouldly;
using Xunit;

// ReSharper disable InconsistentNaming
// ReSharper disable ConvertToConstant.Local
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable ExpressionIsAlwaysNull

namespace Dvt.Common.Tests.Unit.Extensions
{
    [Trait("Common", "Extensions")]
    public class StringExtensionsTests
    {
        [Fact]
        public void NotNullOrEmptyTrimmed_SupplyNonNull_IsTrue()
        {
            var item = "Test";
            var result = item.NotNullOrEmptyTrimmed();
            result.ShouldBeTrue();
        }

        [Fact]
        public void NotNullOrEmptyTrimmed_SupplyEmptyString_IsFalse()
        {
            var item = string.Empty;
            var result = item.NotNullOrEmptyTrimmed();
            result.ShouldBeFalse();
        }

        [Fact]
        public void NotNullOrEmpty_SupplyNonNull_IsTrue()
        {
            var item = "Test";
            var result = item.NotNullOrEmpty();
            result.ShouldBeTrue();
        }

        [Fact]
        public void NotNullEmpty_SupplyNull_IsFalse()
        {
            string item = null;
            var result = item.NotNullOrEmpty();
            result.ShouldBeFalse();
        }

        [Fact]
        public void RemoveAllWhitespace_HasWhitespace_ShouldRemove()
        {
            var item = "This is a test ";
            var result = item.RemoveAllWhiteSpace();
            result.ShouldBe("Thisisatest");
        }

        [Fact]
        public void RemoveAllWhitespace_HasSequentialWhitespace_ShouldRemove()
        {
            var item = "               a          ";
            var result = item.RemoveAllWhiteSpace();
            result.ShouldBe("a");
        }

        [Fact]
        public void RemoveAllWhitespace_NoWhitespace_ShouldRemainUnchanged()
        {
            var item = "This!is!a!test!";
            var result = item.RemoveAllWhiteSpace();
            result.ShouldBe(item);
        }

        [Fact]
        public void RemoveNewLineCharacters_HasCharacters_ShouldRemove()
        {
            var item = "This is \r\na test";
            var result = item.RemoveNewLineCharacters();
            result.ShouldBe("This is a test");
        }

        [Fact]
        public void RemoveCharacters_HasCharacters_ShouldRemove()
        {
            var item = "This is a test";
            var result = item.RemoveCharacters(new [] {'a', 'i'});
            result.ShouldBe("Ths s  test");
        }

        [Fact]
        public void RemoveCharacters_NoMatch_ShouldReturnSource()
        {
            var item = "This is a test";
            var result = item.RemoveCharacters(new[] { 'x', 'y' });
            result.ShouldBe("This is a test");
        }

        [Fact]
        public void RemoveNewLineCharacters_NoCharacters_ShouldRemainUnchanged()
        {
            var item = "This is a test";
            var result = item.RemoveNewLineCharacters();
            result.ShouldBe("This is a test");
        }

        [Fact]
        public void EmptyIfNull_SupplyNull_IsEmpty()
        {
            string item = null;
            var result = item.EmptyIfNull();
            result.ShouldBe(string.Empty);
        }

        [Fact]
        public void EmptyIfNull_SupplyNotNull_IsNotEmpty()
        {
            var item = "This is a test";
            var result = item.EmptyIfNull();
            result.ShouldBe("This is a test");
        }

        [Fact]
        public void EmptyIfNullOrEmptyTrimmed_SupplyNotNull_IsNotEmpty()
        {
            var item = "This is a test";
            var result = item.EmptyIfNullOrEmptyTrimmed();
            result.ShouldBe("This is a test");
        }

        [Fact]
        public void EmptyIfNullOrEmptyTrimmed_SupplyNull_ResultIsEmpty()
        {
            string item = null;
            var result = item.EmptyIfNullOrEmptyTrimmed();
            result.ShouldBe(string.Empty);
        }

        [Fact]
        public void EmptyIfNullOrEmptyTrimmed_SupplyEmptyStringGreaterThanZero_ResultIsEmpty()
        {
            var item = "                         ";
            var result = item.EmptyIfNullOrEmptyTrimmed();
            result.ShouldBe(string.Empty);
        }

        [Fact]
        public void Truncate_ValueLengthGreatherThanLength_ShouldShorten()
        {
            var item = "Elephant";
            var result = item.Truncate(5);
            result.ShouldBe("Eleph");
        }

        [Fact]
        public void Truncate_ValueLengthLessThanLength_ShouldRemainUnchanged()
        {
            var item = "Elephant";
            var result = item.Truncate(10);
            result.ShouldBe("Elephant");
        }

        [Fact]
        public void Truncate_ValueIsNull_IsEmpty()
        {
            string item = null;
            var result = item.Truncate(2);
            result.ShouldBe(string.Empty);
        }

        [Fact]
        public void Truncate_LengthIsLessThanZero_IsEmpty()
        {
            var item = "Elephant";
            var result = item.Truncate(-5);
            result.ShouldBe(string.Empty);
        }

        [Fact]
        public void ToChar_SupplyString_ReturnChar()
        {
            var item = "A";
            var result = item.ToChar();
            result.ShouldBe(Convert.ToChar(item));
        }

        [Fact]
        public void Utf8ByteArrayToString_SupplyByte_ReturnString()
        {
            var item = Encoding.ASCII.GetBytes("edcba");
            var result = item.Utf8ByteArrayToString();
            result.ShouldBe(Encoding.ASCII.GetString(item));
        }

        [Fact]
        public void StringToUtf8ByteArray_SupplyString_ReturnByte()
        {
            var item = "abcd";
            var result = item.StringToUtf8ByteArray();
            result.ShouldBe(Encoding.ASCII.GetBytes(item));
        }

        [Fact]
        public void ToTitleCase_SupplyValueAlreadyTitleCased_StringShouldNotHaveChanged()
        {
            var item = "Fabio";
            var result = item.ToTitleCase();
            result.ShouldBe(item);
        }

        [Fact]
        public void ToTitleCase_SupplyValueNotTitleCased_StringShouldBeTitleCased()
        {
            var item = "fabio";
            var result = item.ToTitleCase();
            result.ShouldBe("Fabio");
        }

        [Fact]
        public void ToTitleCase_SupplySentence_EachWordShouldBeTitleCased()
        {
            var item = "the big BROWN fox a";
            var result = item.ToTitleCase();
            result.ShouldBe("The Big BROWN Fox A");
        }

        [Fact]
        public void ToTitleCase_SupplyNull_ShouldBeNull()
        {
            string item = null;
            var result = item.ToTitleCase();
            result.ShouldBeNull();
        }

        [Fact]
        public void ToTitleCase_SupplyLowerCaseSentence_EachWordShouldBeTitleCased()
        {
            var item = "the big brown fox";
            var result = item.ToTitleCase();
            result.ShouldBe("The Big Brown Fox");
        }

        [Fact]
        public void ToTitleCase_SupplyDifferentCulture_ShouldWork()
        {
            var item = "the big BROWN fox";
            var result = item.ToTitleCase();
            result.ShouldBe("The Big BROWN Fox");
        }

        [Fact]
        public void StripNonAsciiCharaters_StringWithNonAnsi_ShouldRemove()
        {
            var item = "This♦ subject#@!$#@%^$%^has none ASCII*&^*&)_*) characters+(_+)(*&^%$#:* ######### Ç€‘";
            var result = item.StripNonAsciiCharaters();
            result.ShouldBe("This subject#@!$#@%^$%^has none ASCII*&^*&)_*) characters+(_+)(*&^%$#:* ######### ");
        }

        [Fact]
        public void CamelCaseToWords_MixedCase_ShouldConvert()
        {
            var item = "helloThisIsATest";
            var result = item.CamelCaseToWords();
            result.ShouldBe("Hello This Is A Test");
        }

        [Fact]
        public void CamelCaseToWords_NothingToConver_ShouldReturnInput()
        {
            var item = "";
            var result = item.CamelCaseToWords();
            result.ShouldBe("");
        }
    }
}
