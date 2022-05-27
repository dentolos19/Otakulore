using HtmlAgilityPack;

namespace Otakulore.Services.Providers;

public class GogoanimeProvider : IProvider
{

    public string Name => "Gogoanime";

    public Task<MediaSource[]?> SearchAsync(string query)
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
                Title = imageElement.Attributes["alt"].Value
            });
        }
        return Task.FromResult<MediaSource[]?>(mediaSources.ToArray());
    }

}