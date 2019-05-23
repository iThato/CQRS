using System.Xml;

namespace Dvt.Common.Extensions
{
    public static class XmlExtensions
    {
        public static string SelectNodeInnerText(this XmlNode node, string xpathQuery)
        {
            node.ThrowIfNull("node", "Xml node must be supplied for selecting inner text.");
            var resultNode = node.SelectSingleNode(xpathQuery);
// ReSharper disable PossibleNullReferenceException
            return resultNode.IsNull() ? string.Empty : resultNode.InnerText;
// ReSharper restore PossibleNullReferenceException
        }

        public static bool SelectAttributeBoolean(this XmlNode node, string selectAttribute)
        {
            node.ThrowIfNull("node", "Xml node must be supplied for selecting attributes.");
// ReSharper disable PossibleNullReferenceException
            var resultAttribute = node.Attributes[selectAttribute];
// ReSharper restore PossibleNullReferenceException
            if (resultAttribute.IsNull()) return false;
            return bool.TryParse(resultAttribute.InnerText, out var result) && result;
        }

        public static string ReplaceSpecialCharacters(this string xml)
        {
            xml.ThrowIfNullOrEmptyTrimmed("xml", "xml");

            var escapedXml = xml.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");

            // Added the slight variation of an apostrophe below as this happens when you copy
            // content from Microsoft Word which has apostrophe's embedded.
            return escapedXml.Replace("’", "&apos;");
        }
    }
}
