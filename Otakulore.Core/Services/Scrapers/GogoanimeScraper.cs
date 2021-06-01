using System;
using System.Collections.Generic;
using System.Diagnostics;
using HtmlAgilityPack;

namespace Otakulore.Core.Services.Scrapers
{

    public static class GogoanimeScraper
    {

        private static string BaseEndpoint => "https://www1.gogoanime.ai";
        private static string SearchAnimeEndpoint => BaseEndpoint + "/search.html?keyword={0}";
        
        public static AnimePoster[]? SearchAnime(string query)
        {
            try
            {
                var document = new HtmlWeb().Load(string.Format(SearchAnimeEndpoint, Uri.EscapeDataString(query)));
                var nodes = document.DocumentNode.SelectNodes("//div[@class='last_episodes']/ul[@class='items']/li");
                if (nodes == null)
                    return null;
                var list = new List<AnimePoster>();
                foreach (var node in nodes)
                {
                    var root = node.SelectSingleNode("./div/a");
                    var releaseYear = node.SelectSingleNode("./p[@class='released']").InnerText.Trim();
                    list.Add(new AnimePoster
                    {
                        ImageUrl = root.SelectSingleNode("./img").Attributes["src"].Value,
                        Title = root.Attributes["title"].Value,
                        ReleaseYear = releaseYear.Substring(releaseYear.Length - 4),
                        EpisodesUrl = BaseEndpoint + root.Attributes["href"].Value
                    });
                }
                return list.ToArray();
            }
            catch
            {
                return null;
            }
        }

        public static AnimeEpisode[]? ScrapeEpisodes(string url)
        {
            try
            {
                var document = new HtmlDocument();
                document.LoadHtml(EdgeWebDriver.ScrapeDynamicHtml(url));
                var nodes = document.DocumentNode.SelectNodes("//ul[@id='episode_related']//li");
                if (nodes == null)
                    return null;
                var list = new List<AnimeEpisode>();
                foreach (var node in nodes)
                {
                    var root = node.SelectSingleNode("./a");
                    if (root == null)
                        continue;
                    int? episodeNumber = null;
                    if (int.TryParse(root.SelectSingleNode("./div[@class='name']").InnerText.Trim().Substring(3), out int result))
                        episodeNumber = result;
                    list.Add(new AnimeEpisode
                    {
                        EpisodeNumber = episodeNumber,
                        WatchUrl = BaseEndpoint + root.Attributes["href"].Value.Trim()
                    });
                }
                return list.ToArray();
            }
            catch
            {
                return null;
            }
        }

    }

}