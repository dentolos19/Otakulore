using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace Otakulore.Services.Providers;

public class GogoanimeProvider : IAnimeProvider
{

    public Uri ImageUrl => new("https://gogoanime.gg/img/icon/logo.png");
    public string Name => "Gogoanime";

    public Task<MediaSource[]?> SearchSourcesAsync(string query)
    {
        var htmlDocument = new HtmlWeb().Load("https://gogoanime.gg/search.html?keyword=" + Uri.EscapeDataString(query));
        var searchElements = htmlDocument.DocumentNode.SelectNodes("//div[@class='last_episodes']/ul/li");
        if (searchElements is not { Count: > 0 })
            return Task.FromResult<MediaSource[]?>(null);
        var mediaSources = new List<MediaSource>();
        foreach (var searchElement in searchElements)
        {
            var linkElement = searchElement.SelectSingleNode("./div/a");
            var imageElement = linkElement.SelectSingleNode("./img");
            mediaSources.Add(new MediaSource
            {
                ImageUrl = new Uri(imageElement.Attributes["src"].Value),
                Title = imageElement.Attributes["alt"].Value,
                Data = "https://gogoanime.gg" + linkElement.Attributes["href"].Value
            });
        }
        return Task.FromResult(mediaSources.ToArray());
    }

    public Task<MediaContent[]?> GetContentAsync(MediaSource source)
    {
        var htmlDocument = new HtmlWeb().Load(source.Data.ToString());
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
            mediaContents.Add(new MediaContent
            {
                Name = "Episode " + Regex.Match(nameElement.InnerText, @"\d+").Value,
                Data = "https://gogoanime.gg" + linkElement.Attributes["href"].Value.Trim()
            });
        }
        mediaContents.Reverse();
        return Task.FromResult(mediaContents.ToArray());
    }

    public Task<bool> TryExtractVideoPlayer(MediaContent content, out Uri url)
    {
        var htmlDocument = ServiceUtilities.HtmlWeb.Load(content.Data.ToString());
        url = new Uri("https:" + htmlDocument.DocumentNode.SelectSingleNode("//div[@class='play-video']/iframe").Attributes["src"].Value);
        return Task.FromResult(true);
    }

}