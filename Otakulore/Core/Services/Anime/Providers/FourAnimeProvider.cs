using HtmlAgilityPack;
using Otakulore.Core.Helpers;
using System;
using System.Collections.Generic;

namespace Otakulore.Core.Services.Anime.Providers
{

    public class FourAnimeProvider : IAnimeProvider
    {

        public string Id => "4ato";
        public string Name => "4anime.to";

        private static string BaseEndpoint => "https://4anime.to";
        private static string SearchAnimeEndpoint => BaseEndpoint + "/?s={0}";

        public AnimeInfo[] ScrapeAnimes(string query)
        {
            try
            {
                var document = new HtmlWeb().Load(string.Format(SearchAnimeEndpoint, Uri.EscapeDataString(query)));
                var nodes = document.DocumentNode.SelectNodes("//div[@class='container']/div[@id='headerDIV_2']");
                if (nodes == null)
                    return null;
                var list = new List<AnimeInfo>();
                foreach (var node in nodes)
                {
                    var root = node.SelectSingleNode("./div/a");
                    if (root == null)
                        continue;
                    list.Add(new AnimeInfo
                    {
                        ImageUrl = root.SelectSingleNode("./img").Attributes["src"].Value,
                        Title = root.SelectSingleNode("./div").InnerText,
                        EpisodesUrl = root.Attributes["href"].Value
                    });
                }
                return list.ToArray();
            }
            catch (Exception exception)
            {
                BasicLogger.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

        public AnimeEpisode[] ScrapeAnimeEpisodes(string url)
        {
            try
            {
                var document = new HtmlWeb().Load(url);
                var nodes = document.DocumentNode.SelectNodes("//div[@class='watchpage']//li");
                if (nodes == null)
                    return null;
                var list = new List<AnimeEpisode>();
                foreach (var node in nodes)
                {
                    var root = node.SelectSingleNode("./a");
                    if (root == null)
                        continue;
                    int? episodeNumber = null;
                    if (int.TryParse(root.InnerText, out var result))
                        episodeNumber = result;
                    list.Add(new AnimeEpisode
                    {
                        EpisodeNumber = episodeNumber,
                        WatchUrl = root.Attributes["href"].Value
                    });
                }
                return list.ToArray();
            }
            catch (Exception exception)
            {
                BasicLogger.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

        public string ScrapeEpisodeSource(string url)
        {
            try
            {
                var document = new HtmlWeb().Load(url);
                var node = document.DocumentNode.SelectSingleNode("//video/source");
                return node?.Attributes["src"].Value;
            }
            catch (Exception exception)
            {
                BasicLogger.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

    }

}