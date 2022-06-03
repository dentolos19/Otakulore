using HtmlAgilityPack;

namespace Otakulore.Services.Providers;

public class ManganatoProvider : IProvider
{

    public Uri ImageUrl => new("https://manganato.com/themes/hm/images/logo.png");
    public string Name => "Manganato";

    public Task<MediaSource[]?> SearchSourcesAsync(string query)
    {
        var htmlDocument = new HtmlWeb().Load("https://manganato.com/search/story/" + query.Replace(' ', '_'));
        var searchElements = htmlDocument.DocumentNode.SelectNodes("//div[@class='panel-search-story']/div[@class='search-story-item']");
        if (searchElements is not { Count: > 0 })
            return Task.FromResult<MediaSource[]?>(null);
        var mediaSources = new List<MediaSource>();
        foreach (var searchElement in searchElements)
        {
            var linkElement = searchElement.SelectSingleNode("./a");
            var imageElement = linkElement.SelectSingleNode("./img");
            mediaSources.Add(new MediaSource
            {
                ImageUrl = new Uri(imageElement.Attributes["src"].Value),
                Title = linkElement.Attributes["title"].Value,
                Data = linkElement.Attributes["href"].Value
            });
        }
        return Task.FromResult<MediaSource[]?>(mediaSources.ToArray());
    }

    public Task<MediaContent[]?> GetContentAsync(MediaSource source)
    {
        var htmlDocument = new HtmlWeb().Load(source.Data.ToString());
        var chapterElements = htmlDocument.DocumentNode.SelectNodes("//ul[@class='row-content-chapter']/li");
        if (chapterElements is not { Count: > 0 })
            return Task.FromResult<MediaContent[]?>(null);
        var mediaContents = new List<MediaContent>();
        foreach (var chapterElement in chapterElements)
        {
            var linkElement = chapterElement.SelectSingleNode("./a");
            mediaContents.Add(new MediaContent
            {
                Name = linkElement.InnerText,
                Data = linkElement.Attributes["href"].Value
            });
        }
        mediaContents.Reverse();
        return Task.FromResult<MediaContent[]?>(mediaContents.ToArray());
    }

}