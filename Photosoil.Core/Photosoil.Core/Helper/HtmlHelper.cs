using HtmlAgilityPack;

public static class HtmlHelper
{
    public static string CheckAndFixEmptyHtml(string html)
    {
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        if (string.IsNullOrWhiteSpace(htmlDoc.DocumentNode.InnerText))
        {
            return string.Empty;
        }

        return html;
    }
}