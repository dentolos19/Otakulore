namespace Otakulore.Core.Providers;

public class NovelhallProvider : INovelProvider
{

    private const string Url = "https://novelhall.com";

    public string ImageUrl => Url + "/statics/default/images/logo.b5b4c.png";
    public string Name => "Novelhall";

    public IList<MediaSource> GetSources(string query)
    {
        var htmlDocument = Utilities.HtmlWeb.Load(Url + "/index.php?s=so&module=book&keyword=" + query.Replace(' ', '+'));
        var searchElements = htmlDocument.DocumentNode.SelectNodes("//table/tbody/tr");
        if (searchElements is not { Count: > 0 })
            return Array.Empty<MediaSource>();
        var sources = new List<MediaSource>();
        foreach (var searchElement in searchElements)
        {
            var linkElement = searchElement.SelectSingleNode("./td[2]/a");
            sources.Add(new MediaSource(string.Empty, linkElement.InnerText, Url + linkElement.Attributes["href"].Value));
        }
        return sources;
    }

    public IList<MediaContent> GetContents(MediaSource source)
    {
        var htmlDocument = Utilities.HtmlWeb.Load(source.Url);
        var chapterElements = htmlDocument.DocumentNode.SelectNodes("//div[@id='morelist']/ul/li");
        var contents = new List<MediaContent>();
        foreach (var chapterElement in chapterElements)
        {
            var linkElement = chapterElement.SelectSingleNode("./a");
            contents.Add(new MediaContent(linkElement.InnerText, source.Url + linkElement.Attributes["href"].Value));
        }
        return contents;
    }

    /*
    public string GetText(MediaContent content)
    {
        var website = Utilities.HtmlWeb.Load(content.Url);
        return website.DocumentNode.SelectSingleNode("//article/div[@class='entry-content']").InnerText;
    }
    */

}