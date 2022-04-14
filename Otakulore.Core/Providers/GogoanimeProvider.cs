using System.Text.RegularExpressions;

namespace Otakulore.Core.Providers;

public class GogoanimeProvider : IAnimeProvider
{

    public string ImageUrl => Url + "/img/icon/logo.png";
    public string Name => "Gogoanime";

    private const string Url = "https://gogoanime.film";
    private const string CdnUrl = "https://ajax.gogocdn.net/ajax/load-list-episode?ep_start=0&ep_end=10000&id=";

    public IList<MediaSource> GetSources(string query)
    {
        var htmlDocument = Utilities.HtmlWeb.Load(Url + "/search.html?keyword=" + Uri.EscapeDataString(query));
        var searchElements = htmlDocument.DocumentNode.SelectNodes("//div[@class='last_episodes']/ul/li");
        if (searchElements is not { Count: > 0 })
            return Array.Empty<MediaSource>();
        var sources = new List<MediaSource>();
        foreach (var searchElement in searchElements)
        {
            var linkElement = searchElement.SelectSingleNode("./div/a");
            var imageElement = linkElement.SelectSingleNode("./img");
            sources.Add(new MediaSource(
                imageElement.Attributes["src"].Value,
                imageElement.Attributes["alt"].Value,
                Url + linkElement.Attributes["href"].Value));
        }
        return sources;
    }

    public IList<MediaContent> GetContents(MediaSource source)
    {
        var htmlDocument = Utilities.HtmlWeb.Load(source.Url);
        var id = htmlDocument.DocumentNode.SelectSingleNode("//input[@id='movie_id']").Attributes["value"].Value;
        htmlDocument = Utilities.HtmlWeb.Load(CdnUrl + id);
        var episodeElements = htmlDocument.DocumentNode.SelectNodes("//ul/li");
        if (episodeElements is not { Count: > 0 })
            return Array.Empty<MediaContent>();
        var contents = new List<MediaContent>();
        foreach (var episodeElement in episodeElements)
        {
            var linkElement = episodeElement.SelectSingleNode("./a");
            var nameElement = linkElement.SelectSingleNode("./div[@class='name']");
            contents.Add(new MediaContent("Episode " + Regex.Match(nameElement.InnerText, @"\d+").Value, Url + linkElement.Attributes["href"].Value.Trim()));
        }
        contents.Reverse();
        return contents;
    }

    public bool TryExtractVideoUrl(MediaContent content, out string url)
    {
        url = string.Empty;
        return false;
    }

    public bool TryExtractVideoPlayerUrl(MediaContent content, out string url)
    {
        var htmlDocument = Utilities.HtmlWeb.Load(content.Url);
        url = "https:" + htmlDocument.DocumentNode.SelectSingleNode("//div[@class='play-video']/iframe").Attributes["src"].Value;
        return true;
    }

}