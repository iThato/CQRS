using System;
using System.Text;

namespace Dvt.Common.TestUtilities
{
    // copied from http://stackoverflow.com/questions/4286487/is-there-any-lorem-ipsum-generator-in-c/4286571#4286571
    public static class LopsumGenerator
    {
        public static string Generate(int minWords = 1, int maxWords = 100, int minSentences = 1, int maxSentences = 10, int numParagraphs = 0)
        {
            var words = new[]
                            {
                                "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
                                "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
                                "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"
                            };

            var rand = new Random();
            var numSentences = rand.Next(maxSentences - minSentences) + minSentences + 1;
            var numWords = rand.Next(maxWords - minWords) + minWords + 1;

            var result = new StringBuilder();
            for (var p = 0; p < numParagraphs; p++)
            {
                if (numParagraphs > 0)
                    result.Append("<p>");
                for (var s = 0; s < numSentences; s++)
                {
                    for (var w = 0; w < numWords; w++)
                    {
                        if (w > 0)
                            result.Append(" ");
                        result.Append(words[rand.Next(words.Length)]);
                    }
                    result.Append(". ");
                }
                if (numParagraphs > 0)
                    result.Append("</p>");
            }

            return result.ToString();
        }
    }
}
