using System.Xml;
using Dvt.Common.Extensions;
using Shouldly;
using Xunit;

// ReSharper disable InconsistentNaming

namespace Dvt.Common.Tests.Unit.Extensions
{
    [Trait("Common", "Extensions")]
    public class XmlExtensionsTests
    {
        private XmlDocument DocumentUnderTest { get; set; }

        public XmlExtensionsTests()
        {
            DocumentUnderTest = new XmlDocument();
            DocumentUnderTest.LoadXml("<bookstore>" +
                        "<book genre='novel' ISBN='1-861001-57-5' inStock='false'>" +
                        "<title firstBook='true'>Pride And Prejudice</title>" +
                        "<author>Michael O'Douglas</author>" +
                        "<genre></genre>" +
                        "</book>" +
                        "</bookstore>");
        }

        [Fact]
        public void SelectNodeInnerText_LoadXml_ShouldGetInnerText()
        {
            var result = DocumentUnderTest.SelectNodeInnerText("bookstore/book/title");
            result.ShouldBe("Pride And Prejudice");
        }

        [Fact]
        public void SelectNodeInnerText_LoadXml_CantFindInnerText()
        {
            var result = DocumentUnderTest.SelectNodeInnerText("bookstore/book/genre");
            result.ShouldBe(string.Empty);
        }

        [Fact]
        public void SelectAttributeBoolean_LoadXml_IsTrue()
        {
            var node = DocumentUnderTest.SelectSingleNode("bookstore/book/title");
            var result = node.SelectAttributeBoolean("firstBook");
            result.ShouldBeTrue();
        }

        [Fact]
        public void SelectAttributeBoolean_LoadXmlNoAttribute_IsFalse()
        {
            var node = DocumentUnderTest.SelectSingleNode("bookstore");
            var result = node.SelectAttributeBoolean("address");
            result.ShouldBeFalse();
        }

        [Fact]
        public void ReplaceSpecialCharacters_LoadXmlWithSpecialCharacters_ShouldReplace()
        {
            var item = DocumentUnderTest.SelectNodeInnerText("bookstore/book/author");
            var result = item.ReplaceSpecialCharacters();
            result.ShouldBe("Michael O&apos;Douglas");
        }

        [Fact]
        public void ReplaceSpecialCharacters_LoadXmlWithNoSpecialCharacters_ShouldNotReplace()
        {
            var item = DocumentUnderTest.SelectNodeInnerText("/bookstore/book/title");
            var result = item.ReplaceSpecialCharacters();
            result.ShouldBe("Pride And Prejudice");
        }
    }
}
