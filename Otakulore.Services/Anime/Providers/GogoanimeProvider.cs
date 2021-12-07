using HtmlAgilityPack;

namespace Otakulore.Services.Anime.Providers;

public class GogoanimeProvider : IAnimeProvider
{

    public string Name => "Gogoanime";
    public string Author => "dentolos19";
    public string Website => "https://gogoanime.wiki";

    public IEnumerable<IMediaInfo> SearchAnime(string query)
    {
        query = Uri.EscapeDataString(query);
        var website = new HtmlWeb().Load($"{Website}/search.html?keyword={query}");
        var animeElements = website.DocumentNode.SelectNodes("//div[@class='last_episodes']/ul/li");
        var animeList = new List<AnimeInfo>();
        foreach (var animeElement in animeElements)
        {
            var linkElement = animeElement.SelectSingleNode("./div/a");
            animeList.Add(new AnimeInfo
            {
                ImageUrl = linkElement.SelectSingleNode("./img").Attributes["src"].Value,
                Title = linkElement.Attributes["title"].Value,
                Url = Website + linkElement.Attributes["href"].Value
            });
        }
        return animeList.ToArray();
    }

    public IEnumerable<IMediaContent> GetAnimeEpisodes(IMediaInfo info)
    {
        throw new NotImplementedException();
    }

    public string GetAnimeEpisodeSource(IMediaContent content)
    {
        throw new NotImplementedException();
    }

}