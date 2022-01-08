using HtmlAgilityPack;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Otakulore.Core.Providers;

public class GogoanimeProvider : IAnimeProvider
{

    public Uri ImageUrl => new("https://gogoanime.film/img/icon/logo.png");
    public string Name => "Gogoanime";
    public Uri Url => new("https://gogoanime.film");

    public IEnumerable<MediaSource> SearchAnime(string query)
    {
        query = Uri.EscapeDataString(query);
        var website = ScrapingServices.HtmlWeb.Load($"{Url}/search.html?keyword={query}");
        var animeElements = website.DocumentNode.SelectNodes("//div[@class='last_episodes']/ul/li");
        if (animeElements is not { Count: > 0 })
            return Array.Empty<MediaSource>();
        var animeList = new List<MediaSource>();
        foreach (var animeElement in animeElements)
        {
            var linkElement = animeElement.SelectSingleNode("./div/a");
            animeList.Add(new MediaSource
            {
                ImageUrl = new Uri(linkElement.SelectSingleNode("./img").Attributes["src"].Value),
                Title = linkElement.Attributes["title"].Value,
                Url = new Uri(Url, linkElement.Attributes["href"].Value)
            });
        }
        return animeList.ToArray();
    }

    public IEnumerable<MediaContent> GetAnimeEpisodes(MediaSource source)
    {
        var webDriver = ScrapingServices.WebDriver;
        webDriver.Navigate().GoToUrl(source.Url);
        var pages = webDriver.FindElements(By.XPath("//ul[@id='episode_page']/li"));
        var animeEpisodes = new List<MediaContent>();
        foreach (var page in pages)
        {
            page.Click();
            var website = new HtmlDocument();
            website.LoadHtml(webDriver.PageSource);
            var episodeElements = website.DocumentNode.SelectNodes("//ul[@id='episode_related']/li");
            if (episodeElements is null)
                continue;
            var subAnimeEpisodes = new List<MediaContent>();
            foreach (var episodeElement in episodeElements)
            {
                var linkElement = episodeElement.SelectSingleNode("./a");
                var episodeNumber = int.Parse(linkElement.SelectSingleNode("./div[@class='name']").InnerText.Trim()[3..]);
                subAnimeEpisodes.Add(new MediaContent
                {
                    Name = $"Episode {episodeNumber}",
                    Url = new Uri(Url, linkElement.Attributes["href"].Value)
                });
            }
            subAnimeEpisodes.Reverse();
            animeEpisodes.AddRange(subAnimeEpisodes);
        }
        return animeEpisodes;
    }

    public Uri ExtractVideoUrl(MediaContent content)
    {
        var website = ScrapingServices.HtmlWeb.Load(content.Url);
        var streamingElements = website.DocumentNode.SelectNodes("//div[@class='anime_muti_link']/ul/li");
        return new Uri("https:" + streamingElements[0].SelectSingleNode("./a").Attributes["data-video"].Value);
    }

}