namespace Bx.HtmlMinifier.Interfaces
{
    public interface IHtmlMinifierService
    {
        string RemoveWhitespaces(string input);
        string RemoveLineBreaks(string input);
    }
}