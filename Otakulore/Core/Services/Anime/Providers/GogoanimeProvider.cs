using HtmlAgilityPack;
using Otakulore.Core.Helpers;
using System;
using System.Collections.Generic;

namespace Otakulore.Core.Services.Anime.Providers
{

    public static class GogoanimeProvider
    {

        private static string BaseEndpoint => "https://www1.gogoanime.ai";
        private static string SearchAnimeEndpoint => BaseEndpoint + "/search.html?keyword={0}";

        public static AnimeInfo[] ScrapeSearchAnime(string query)
        {
            try
            {
                var document = new HtmlWeb().Load(string.Format(SearchAnimeEndpoint, Uri.EscapeDataString(query)));
                var nodes = document.DocumentNode.SelectNodes("//div[@class='last_episodes']/ul[@class='items']/li");
                if (nodes == null)
                    return null;
                var list = new List<AnimeInfo>();
                foreach (var node in nodes)
                {
                    var root = node.SelectSingleNode("./div/a");
                    list.Add(new AnimeInfo
                    {
                        ImageUrl = root.SelectSingleNode("./img").Attributes["src"].Value,
                        Title = root.Attributes["title"].Value,
                        EpisodesUrl = BaseEndpoint + root.Attributes["href"].Value
                    });
                }
                return list.ToArray();
            }
            catch (Exception exception)
            {
                SimpleLogger.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

        public static AnimeEpisode[] ScrapeAnimeEpisodes(string url)
        {
            try
            {
                var driver = WebDriver.GetWebDriver();
                driver.Url = url;
                var pages = driver.FindElementsByXPath("//ul[@id='episode_page']/li");
                if (pages == null)
                    return null;
                var list = new List<AnimeEpisode>();
                foreach (var page in pages)
                {
                    page.Click();
                    var document = new HtmlDocument();
                    document.LoadHtml(driver.PageSource);
                    var nodes = document.DocumentNode.SelectNodes("//ul[@id='episode_related']/li");
                    if (nodes == null)
                        return null;
                    var subList = new List<AnimeEpisode>();
                    foreach (var node in nodes)
                    {
                        var root = node.SelectSingleNode("./a");
                        if (root == null)
                            continue;
                        int? episodeNumber = null;
                        if (int.TryParse(root.SelectSingleNode("./div[@class='name']").InnerText.Trim().Substring(3), out int result))
                            episodeNumber = result;
                        subList.Add(new AnimeEpisode
                        {
                            EpisodeNumber = episodeNumber,
                            WatchUrl = BaseEndpoint + root.Attributes["href"].Value.Trim()
                        });
                    }
                    subList.Reverse();
                    list.AddRange(subList);
                }
                return list.ToArray();
            }
            catch (Exception exception)
            {
                SimpleLogger.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

        public static string ScrapeVideoUrl(string url)
        {
            try
            {
                var document = new HtmlWeb().Load(url);
                document = new HtmlWeb().Load("https:" + document.DocumentNode.SelectSingleNode("//div[@class='anime_muti_link']/ul/li[@class='vidcdn']/a").Attributes["data-video"].Value);
                return document.DocumentNode.SelectSingleNode("//div[@class='videocontent']/script").InnerText?.GetStringBetween("file: '", "'");
            }
            catch (Exception exception)
            {
                SimpleLogger.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

    }

}