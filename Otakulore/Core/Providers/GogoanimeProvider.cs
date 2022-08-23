using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace Otakulore.Core.Providers;

public class GogoanimeProvider : IAnimeProvider
{

    public string Name => "Gogoanime";

    public Task<MediaSource[]?> GetSources(string query)
    {
        var htmlDocument = Utilities.HtmlWeb.Load("https://gogoanime.tel/search.html?keyword=" + Uri.EscapeDataString(query));
        var searchElements = htmlDocument.DocumentNode.SelectNodes("//div[@class='last_episodes']/ul/li");
        if (searchElements is not { Count: > 0 })
            return Task.FromResult<MediaSource[]?>(null);
        var mediaSources = new List<MediaSource>();
        foreach (var searchElement in searchElements)
        {
            var linkElement = searchElement.SelectSingleNode("./div/a");
            var imageElement = linkElement.SelectSingleNode("./img");
            mediaSources.Add(new MediaSource
            (
                new Uri(imageElement.Attributes["src"].Value),
                imageElement.Attributes["alt"].Value,
                new Uri("https://gogoanime.tel" + linkElement.Attributes["href"].Value)
            ));
        }
        return Task.FromResult(mediaSources.ToArray());
    }

    public Task<MediaContent[]?> GetContents(MediaSource source)
    {
        var htmlDocument = Utilities.HtmlWeb.Load(source.Url.ToString());
        var id = htmlDocument.DocumentNode.SelectSingleNode("//input[@id='movie_id']").Attributes["value"].Value;
        htmlDocument = new HtmlWeb().Load("https://ajax.gogocdn.net/ajax/load-list-episode?ep_start=0&ep_end=10000&id=" + id);
        var episodeElements = htmlDocument.DocumentNode.SelectNodes("//ul/li");
        if (episodeElements is not { Count: > 0 })
            return Task.FromResult<MediaContent[]?>(null);
        var mediaContents = new List<MediaContent>();
        foreach (var episodeElement in episodeElements)
        {
            var linkElement = episodeElement.SelectSingleNode("./a");
            var nameElement = linkElement.SelectSingleNode("./div[@class='name']");
            mediaContents.Add(new MediaContent(
                "Episode " + Regex.Match(nameElement.InnerText, @"\d+").Value,
                new Uri("https://gogoanime.tel" + linkElement.Attributes["href"].Value.Trim())
            ));
        }
        mediaContents.Reverse();
        return Task.FromResult(mediaContents.ToArray());
    }

    public Task<bool> TryExtractVideoPlayerUrl(MediaContent content, out Uri url)
    {
        var htmlDocument = Utilities.HtmlWeb.Load(content.Url.ToString());
        url = new Uri("https:" + htmlDocument.DocumentNode.SelectSingleNode("//div[@class='play-video']/iframe").Attributes["src"].Value);
        return Task.FromResult(true);
    }

}