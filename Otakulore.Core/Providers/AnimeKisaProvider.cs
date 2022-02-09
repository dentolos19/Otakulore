namespace Otakulore.Core.Providers;

public class AnimeKisaProvider : IAnimeProvider
{

    private const string _url = "https://animekisa.tv";

    public string ImageUrl => _url + "/img/ah_logo.png";
    public string Name => "AnimeKisa";

    public IList<MediaSource> GetSources(string query)
    {
        var website = Utilities.HtmlWeb.Load(_url + "/search?q=" + query.Replace(' ', '+'));
        var categories = website.DocumentNode.SelectNodes("//div[@class='similarboxmain']");
        if (categories is not { Count: > 0 })
            return Array.Empty<MediaSource>();
        var sources = new List<MediaSource>();
        foreach (var category in categories)
        {
            var links = category.SelectNodes("./div/a");
            foreach (var link in links)
                try
                {
                    var item = link.SelectSingleNode("./div[@class='similarcmain']/div[@class='similarc']");
                    sources.Add(new MediaSource
                    {
                        ImageUrl = _url + item.SelectSingleNode("./div[@class='similarpic']/img").Attributes["src"].Value,
                        Title = item.SelectSingleNode("./div[@class='similard']/div[@class='centered']/div[@class='similardd']").InnerText,
                        Url = _url + link.Attributes["href"].Value
                    });
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
        var website = Utilities.HtmlWeb.Load(source.Url);
        var episodes = website.DocumentNode.SelectNodes("//div[@class='infoepbox']/a");
        var contents = new List<MediaContent>();
        foreach (var episode in episodes)
            contents.Add(new MediaContent
            {
                Name = "Episode " + episode.SelectSingleNode("./div/div/div/div[@class='infoept2']/div").InnerText,
                Url = _url + "/" + episode.Attributes["href"].Value
            });
        contents.Reverse();
        return contents;
    }

}