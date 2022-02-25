namespace Otakulore.Core.Providers;

public class AnimeKisaProvider : IAnimeProvider
{

    private const string Url = "https://animekisa.tv";

    public string ImageUrl => Url + "/img/ah_logo.png";
    public string Name => "AnimeKisa";

    public IList<MediaSource> GetSources(string query)
    {
        var htmlDocument = Utilities.HtmlWeb.Load(Url + "/search?q=" + query.Replace(' ', '+'));
        var categoryElements = htmlDocument.DocumentNode.SelectNodes("//div[@class='similarboxmain']");
        if (categoryElements is not { Count: > 0 })
            return Array.Empty<MediaSource>();
        var sources = new List<MediaSource>();
        foreach (var searchElement in categoryElements)
        {
            var linkElements = searchElement.SelectNodes("./div/a");
            foreach (var linkElement in linkElements)
                try
                {
                    var element = linkElement.SelectSingleNode("./div[@class='similarcmain']/div[@class='similarc']");
                    sources.Add(new MediaSource(Url + element.SelectSingleNode("./div[@class='similarpic']/img").Attributes["src"].Value, element.SelectSingleNode("./div[@class='similard']/div[@class='centered']/div[@class='similardd']").InnerText, Url + linkElement.Attributes["href"].Value));
                }
                catch
                {
                    // do nothing
                }
        }
        return sources;
    }

    public IList<MediaContent> GetContents(MediaSource source)
    {
        var htmlDocument = Utilities.HtmlWeb.Load(source.Url);
        var episodeElements = htmlDocument.DocumentNode.SelectNodes("//div[@class='infoepbox']/a");
        var contents = new List<MediaContent>();
        foreach (var episode in episodeElements)
            contents.Add(new MediaContent("Episode " + episode.SelectSingleNode("./div/div/div/div[@class='infoept2']/div").InnerText, Url + "/" + episode.Attributes["href"].Value));
        contents.Reverse();
        return contents;
    }

}