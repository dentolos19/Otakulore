using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using OpenQA.Selenium;

namespace Otakulore.Core.Providers;

public class GogoanimeProvider : IAnimeProvider
{

    public ImageSource Image => (ImageSource)Application.Current.Resources["GogoanimeAsset"];
    public string Name => "Gogoanime";
    public string Url => "https://gogoanime.film";

    public IEnumerable<MediaSource> SearchAnime(string query)
    {
        query = Uri.EscapeDataString(query);
        var website = ScrapingService.HtmlWeb.Load($"{Url}/search.html?keyword={query}");
        var animeElements = website.DocumentNode.SelectNodes("//div[@class='last_episodes']/ul/li");
        if (animeElements is not { Count: > 0 })
            return Array.Empty<MediaSource>();
        var animeList = new List<MediaSource>();
        foreach (var animeElement in animeElements)
        {
            var linkElement = animeElement.SelectSingleNode("./div/a");
            animeList.Add(new MediaSource
            {
                ImageUrl = linkElement.SelectSingleNode("./img").Attributes["src"].Value,
                Title = linkElement.Attributes["title"].Value,
                Url = Url + linkElement.Attributes["href"].Value
            });
        }
        return animeList.ToArray();
    }

    public IEnumerable<MediaContent> GetAnimeEpisodes(MediaSource source)
    {
        var webDriver = ScrapingService.WebDriver;
        webDriver.Navigate().GoToUrl(source.Url);
        var pages = webDriver.FindElements(By.XPath("//ul[@id='episode_page']/li"));
        var animeEpisodes = new List<MediaContent>();
        foreach (var page in pages)
        {
            webDriver.ScrollToElement(page);
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
                var url = Url + linkElement.Attributes["href"].Value.Trim();
                try
                {
                    website = ScrapingService.HtmlWeb.Load(url);
                    var streamingElements = website.DocumentNode.SelectNodes("//div[@class='anime_muti_link']/ul/li");
                    url = "https:" + streamingElements[0].SelectSingleNode("./a").Attributes["data-video"].Value;
                }
                catch
                {
                    continue; // do nothing
                }
                subAnimeEpisodes.Add(new MediaContent
                {
                    Name = $"Episode {episodeNumber}",
                    Url = url
                });
            }
            subAnimeEpisodes.Reverse();
            animeEpisodes.AddRange(subAnimeEpisodes);
        }
        return animeEpisodes;
    }

}