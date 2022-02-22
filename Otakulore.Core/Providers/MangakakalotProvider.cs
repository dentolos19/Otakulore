using Otakulore.Core.Helpers;

namespace Otakulore.Core.Providers;

public class MangakakalotProvider : IMangaProvider
{

    private const string _url = "https://mangakakalot.com";

    public string ImageUrl => _url + "/themes/home/icons/logo.png";
    public string Name => "Mangakakalot";

    public IList<MediaSource> GetSources(string query)
    {
        var website = Utilities.HtmlWeb.Load(_url + "/search/story/" + query.Replace(' ', '_'));
        var mangas = website.DocumentNode.SelectNodes("//div[@class='panel_story_list']/div[@class='story_item']");
        if (mangas is not { Count: > 0 })
            return Array.Empty<MediaSource>();
        var sources = new List<MediaSource>();
        foreach (var manga in mangas)
        {
            var link = manga.SelectSingleNode("./a");
            var image = link.SelectSingleNode("./img");
            sources.Add(new MediaSource
            {
                ImageUrl = image.Attributes["src"].Value,
                Title = image.Attributes["alt"].Value,
                Url = link.Attributes["href"].Value
            });
        }
        return sources;
    }

    public IList<MediaContent> GetContents(MediaSource source)
    {
        var website = Utilities.HtmlWeb.Load(source.Url);
        var chapters = website.DocumentNode.SelectNodes("//div[@class='panel-story-chapter-list']/ul/li");
        var contents = new List<MediaContent>();
        foreach (var chapter in chapters)
        {
            var link = chapter.SelectSingleNode("./a");
            contents.Add(new MediaContent
            {
                Name = link.InnerText,
                Url = link.Attributes["href"].Value
            });
        }
        contents.Reverse();
        return contents;
    }

}