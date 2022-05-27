using HtmlAgilityPack;

namespace Otakulore.Services.Providers;

public class ManganatoProvider : IProvider
{

    public string Name => "Manganato";

    public Task<MediaSource[]?> SearchAsync(string query)
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
                Title = linkElement.Attributes["title"].Value
            });
        }
        return Task.FromResult<MediaSource[]?>(mediaSources.ToArray());
    }

}