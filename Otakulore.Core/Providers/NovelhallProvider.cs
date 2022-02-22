using Otakulore.Core.Helpers;

namespace Otakulore.Core.Providers;

public class NovelhallProvider : INovelProvider
{

    private readonly string _url = "https://novelhall.com";

    public string ImageUrl => _url + "/statics/default/images/logo.b5b4c.png";
    public string Name => "Novelhall";

    public IList<MediaSource> GetSources(string query)
    {
        var website = Utilities.HtmlWeb.Load(_url + "/index.php?s=so&module=book&keyword=" + query.Replace(' ', '+'));
        var entries = website.DocumentNode.SelectNodes("//table/tbody/tr");
        if (entries is not { Count: > 0 })
            return Array.Empty<MediaSource>();
        var sources = new List<MediaSource>();
        foreach (var category in entries)
        {
            var link = category.SelectSingleNode("./td[2]/a");
            sources.Add(new MediaSource
            {
                Title = link.InnerText,
                Url = _url + link.Attributes["href"].Value
            });
        }
        return sources;
    }

    public IList<MediaContent> GetContents(MediaSource source)
    {
        var website = Utilities.HtmlWeb.Load(source.Url);
        var chapters = website.DocumentNode.SelectNodes("//div[@id='morelist']/ul/li");
        var contents = new List<MediaContent>();
        foreach (var chapter in chapters)
        {
            var link = chapter.SelectSingleNode("./a");
            contents.Add(new MediaContent
            {
                Name = link.InnerText,
                Url = source.Url + link.Attributes["href"].Value
            });
        }
        return contents;
    }

    public string GetText(MediaContent content)
    {
        var website = Utilities.HtmlWeb.Load(content.Url);
        return website.DocumentNode.SelectSingleNode("//article/div[@class='entry-content']").InnerText;
    }

}