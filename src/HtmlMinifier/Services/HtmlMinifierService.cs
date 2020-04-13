using System.Text.RegularExpressions;
using Bx.HtmlMinifier.Interfaces;

namespace Bx.HtmlMinifier.Services
{
    public class HtmlMinifierService : IHtmlMinifierService
    {
        public string RemoveWhitespaces(string input)
        {
            var beginWhitespaceRegex = new Regex("^\\s*", RegexOptions.Compiled | RegexOptions.Multiline);
            var doubleWhitespaceRegex = new Regex("\\s\\s", RegexOptions.Compiled | RegexOptions.Multiline);
            var beforeTagWhitespaceRegex = new Regex("\\s+<", RegexOptions.Compiled | RegexOptions.Multiline);
            var afterTagWhitespaceRegex = new Regex(">\\s+", RegexOptions.Compiled | RegexOptions.Multiline);

            input = beginWhitespaceRegex.Replace(input, string.Empty);
            input = doubleWhitespaceRegex.Replace(input, " ");
            input = beforeTagWhitespaceRegex.Replace(input, "<");
            input = afterTagWhitespaceRegex.Replace(input, ">");

            return input;
        }

        public string RemoveLineBreaks(string input)
        {
            var lineBreakRegex = new Regex("[\\n\\r]", RegexOptions.Compiled | RegexOptions.Multiline);

            input = lineBreakRegex.Replace(input, string.Empty);

            return input;
        }
    }
}