using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Otakulore.Providers.Objects;

namespace Otakulore.Providers.Providers;

public class GogoanimeProvider : IAnimeProvider
{
    private readonly HtmlWeb _htmlWeb = new();

    public string Name => "Gogoanime";

    public async Task<IList<MediaSource>> GetSources(string query)
    {
        var htmlDocument = await _htmlWeb.LoadFromWebAsync("https://gogoanime.tel/search.html?keyword=" + Uri.EscapeDataString(query));
        var searchElements = htmlDocument.DocumentNode.SelectNodes("//div[@class='last_episodes']/ul/li");
        var sources = new List<MediaSource>();
        if (searchElements is not { Count: > 0 })
            return sources;
        foreach (var searchElement in searchElements)
        {
            var linkElement = searchElement.SelectSingleNode("./div/a");
            var imageElement = linkElement.SelectSingleNode("./img");
            sources.Add(new MediaSource
            {
                ImageUrl = new Uri(imageElement.Attributes["src"].Value),
                Title = imageElement.Attributes["alt"].Value,
                Data = "https://gogoanime.tel" + linkElement.Attributes["href"].Value
            });
        }
        return sources;
    }

    public async Task<IList<MediaContent>> GetContents(MediaSource source)
    {
        if (source.Data is not string url)
            return Array.Empty<MediaContent>();
        var htmlDocument = await _htmlWeb.LoadFromWebAsync(url);
        var mediaId = htmlDocument.DocumentNode.SelectSingleNode("//input[@id='movie_id']").Attributes["value"].Value;
        htmlDocument = await _htmlWeb.LoadFromWebAsync("https://ajax.gogocdn.net/ajax/load-list-episode?ep_start=0&ep_end=10000&id=" + mediaId);
        var episodeElements = htmlDocument.DocumentNode.SelectNodes("//ul/li");
        var contents = new List<MediaContent>();
        if (episodeElements is not { Count: > 0 })
            return contents;
        foreach (var episodeElement in episodeElements)
        {
            var linkElement = episodeElement.SelectSingleNode("./a");
            var nameElement = linkElement.SelectSingleNode("./div[@class='name']");
            contents.Add(new MediaContent
            {
                Name = "Episode " + Regex.Match(nameElement.InnerText, @"\d+").Value,
                Data = "https://gogoanime.tel" + linkElement.Attributes["href"].Value.Trim()
            });
        }
        contents.Reverse();
        return contents;
    }

    public async Task<Uri> ExtractAnimePlayerUrl(MediaContent content)
    {
        if (content.Data is not string url)
            throw new Exception();
        var htmlDocument = await _htmlWeb.LoadFromWebAsync(url);
        return new Uri("https:" + htmlDocument.DocumentNode.SelectSingleNode("//div[@class='play-video']/iframe").Attributes["src"].Value);
    }
}