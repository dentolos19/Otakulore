using HtmlAgilityPack;
using OpenQA.Selenium;

namespace Otakulore.Services.Anime.Providers;

public class GogoanimeProvider : IAnimeProvider
{

    public string Name => "Gogoanime";
    public string Author => "dentolos19";
    public string Website => "https://gogoanime.wiki";

    public IEnumerable<IMediaInfo> SearchAnime(string query)
    {
        query = Uri.EscapeDataString(query);
        var website = ScrapingServices.HtmlWeb.Load($"{Website}/search.html?keyword={query}");
        var animeElements = website.DocumentNode.SelectNodes("//div[@class='last_episodes']/ul/li");
        if (animeElements is not { Count: > 0 })
            return Array.Empty<AnimeInfo>();
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
        if (info is not AnimeInfo animeInfo)
            return Array.Empty<AnimeEpisode>(); // TODO: throw exception
        var webDriver = ScrapingServices.WebDriver;
        webDriver.Navigate().GoToUrl(animeInfo.Url);
        var pages = webDriver.FindElements(By.XPath("//ul[@id='episode_page']/li"));
        var animeEpisodes = new List<AnimeEpisode>();
        foreach (var page in pages)
        {
            page.Click();
            var website = new HtmlDocument();
            website.LoadHtml(webDriver.PageSource);
            var episodeElements = website.DocumentNode.SelectNodes("//ul[@id='episode_related']/li");
            if (episodeElements is null)
                continue;
            var subAnimeEpisodes = new List<AnimeEpisode>();
            foreach (var episodeElement in episodeElements)
            {
                var linkElement = episodeElement.SelectSingleNode("./a");
                var episodeNumber = int.Parse(linkElement.SelectSingleNode("./div[@class='name']").InnerText.Trim()[3..]);
                subAnimeEpisodes.Add(new AnimeEpisode
                {
                    Name = $"Episode {episodeNumber}",
                    Url = Website + linkElement.Attributes["href"].Value.Trim()
                });
            }
            subAnimeEpisodes.Reverse();
            animeEpisodes.AddRange(subAnimeEpisodes);
        }
        return animeEpisodes;
    }

    public string GetAnimeEpisodeSource(IMediaContent content)
    {
        if (content is not AnimeEpisode animeEpisode)
            return string.Empty; // TODO: throw exception
        var website = ScrapingServices.HtmlWeb.Load(animeEpisode.Url);
        var linkElement = website.DocumentNode.SelectSingleNode("//div[@class='anime_muti_link']/ul/li[@class='vidcdn']/a");
        var videoEmbedLink = "https:" + linkElement.Attributes["data-video"].Value;
        website = ScrapingServices.HtmlWeb.Load(videoEmbedLink);
        var scriptCode = website.DocumentNode.SelectSingleNode("//div[@class='videocontent']/script").InnerText;
        var videoSource = ExtractVideoSource(scriptCode);
        return videoSource;
    }

    private static string ExtractVideoSource(string code)
    {
        var prefixString = "file: '";
        var postfixString = "'";
        var prefixIndex = code.IndexOf(prefixString, StringComparison.Ordinal) + postfixString.Length;
        return code[prefixIndex..].Remove(code.IndexOf(postfixString, StringComparison.Ordinal));
    }

}