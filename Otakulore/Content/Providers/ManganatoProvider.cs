using HtmlAgilityPack;
using Otakulore.Content.Objects;

namespace Otakulore.Content.Providers;

public class ManganatoProvider : IMangaProvider
{
    private readonly HtmlWeb _htmlWeb = new();

    public string Name => "Manganato";

    public async Task<IList<MediaSource>> GetSources(string query)
    {
        if (string.IsNullOrEmpty(query))
            return Array.Empty<MediaSource>();
        var htmlDocument = await _htmlWeb.LoadFromWebAsync("https://manganato.com/search/story/" + query.Replace(' ', '_'));
        var searchElements = htmlDocument.DocumentNode.SelectNodes("//div[@class='panel-search-story']/div[@class='search-story-item']");
        var sources = new List<MediaSource>();
        if (searchElements is not { Count: > 0 })
            return sources;
        foreach (var searchElement in searchElements)
        {
            var linkElement = searchElement.SelectSingleNode("./a");
            var imageElement = linkElement.SelectSingleNode("./img");
            sources.Add(new MediaSource
            {
                ImageUrl = new Uri(imageElement.Attributes["src"].Value),
                Title = linkElement.Attributes["title"].Value,
                Data = linkElement.Attributes["href"].Value
            });
        }
        return sources;
    }

    public async Task<IList<MediaContent>> GetContents(MediaSource source)
    {
        if (source.Data is not string url)
            return Array.Empty<MediaContent>();
        var htmlDocument = await _htmlWeb.LoadFromWebAsync(url);
        var chapterElements = htmlDocument.DocumentNode.SelectNodes("//ul[@class='row-content-chapter']/li");
        var contents = new List<MediaContent>();
        if (chapterElements is not { Count: > 0 })
            return contents;
        foreach (var chapterElement in chapterElements)
        {
            var linkElement = chapterElement.SelectSingleNode("./a");
            contents.Add(new MediaContent
            {
                Name = linkElement.InnerText,
                Data = linkElement.Attributes["href"].Value
            });
        }
        contents.Reverse();
        return contents;
    }

    public Task<Uri> ExtractMangaReaderUrl(MediaContent content)
    {
        if (content.Data is string url)
            return Task.FromResult(new Uri(url));
        throw new Exception();
    }
}