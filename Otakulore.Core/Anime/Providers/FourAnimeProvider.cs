using System;
using System.Collections.Generic;
using System.IO;
using HtmlAgilityPack;

namespace Otakulore.Core.Anime.Providers
{

    public static class FourAnimeProvider
    {

        private static string BaseEndpoint => "https://4anime.to";
        private static string SearchAnimeEndpoint => BaseEndpoint + "/?s={0}";

        public static AnimePoster[]? SearchAnime(string query)
        {
            try
            {
                var document = new HtmlWeb().Load(string.Format(SearchAnimeEndpoint, Uri.EscapeDataString(query)));
                var nodes = document.DocumentNode.SelectNodes("//div[@class='container']/div[@id='headerDIV_2']");
                if (nodes == null)
                    return null;
                var list = new List<AnimePoster>();
                foreach (var node in nodes)
                {
                    var root = node.SelectSingleNode("./div/a");
                    if (root == null)
                        continue;
                    list.Add(new AnimePoster
                    {
                        ImageUrl = root.SelectSingleNode("./img").Attributes["src"].Value,
                        Title = root.SelectSingleNode("./div").InnerText,
                        EpisodesUrl = root.Attributes["href"].Value
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
                    if (int.TryParse(root.InnerText, out int result))
                        episodeNumber = result;
                    list.Add(new AnimeEpisode
                    {
                        EpisodeNumber = episodeNumber,
                        WatchUrl = root.Attributes["href"].Value
                    });
                }
                return list.ToArray();
            }
            catch
            {
                return null;
            }
        }

        public static string? ScrapeVideoSource(string url)
        {
            try
            {
                var document = new HtmlWeb().Load(url);
                var node = document.DocumentNode.SelectSingleNode("//video/source");
                if (node == null)
                    return null;
                return node.Attributes["src"].Value;
            }
            catch
            {
                return null;
            }
        }

    }

}