namespace Otakulore.Content.Providers;

public class ManganatoProvider : IProvider
{

    public string Name => "Manganato";

    public Task<MediaSource[]?> GetSources(string? query)
    {
        if (query is null)
            return Task.FromResult<MediaSource[]?>(null);
        var htmlDocument = Utilities.HtmlWeb.Load("https://manganato.com/search/story/" + query.Replace(' ', '_'));
        var searchElements = htmlDocument.DocumentNode.SelectNodes("//div[@class='panel-search-story']/div[@class='search-story-item']");
        if (searchElements is not { Count: > 0 })
            return Task.FromResult<MediaSource[]?>(null);
        var mediaSources = new List<MediaSource>();
        foreach (var searchElement in searchElements)
        {
            var linkElement = searchElement.SelectSingleNode("./a");
            var imageElement = linkElement.SelectSingleNode("./img");
            mediaSources.Add(new MediaSource
            (
                new Uri(imageElement.Attributes["src"].Value),
                linkElement.Attributes["title"].Value,
                new Uri(linkElement.Attributes["href"].Value)
            ));
        }
        return Task.FromResult(mediaSources.ToArray());
    }

    public Task<MediaContent[]?> GetContents(MediaSource source)
    {
        var htmlDocument = Utilities.HtmlWeb.Load(source.Url.ToString());
        var chapterElements = htmlDocument.DocumentNode.SelectNodes("//ul[@class='row-content-chapter']/li");
        if (chapterElements is not { Count: > 0 })
            return Task.FromResult<MediaContent[]?>(null);
        var mediaContents = new List<MediaContent>();
        foreach (var chapterElement in chapterElements)
        {
            var linkElement = chapterElement.SelectSingleNode("./a");
            mediaContents.Add(new MediaContent
            (
                linkElement.InnerText,
                new Uri(linkElement.Attributes["href"].Value)
            ));
        }
        mediaContents.Reverse();
        return Task.FromResult(mediaContents.ToArray());
    }

}