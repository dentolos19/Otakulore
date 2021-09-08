using HtmlAgilityPack;
using Otakulore.Core.Helpers;
using System;
using System.Collections.Generic;

namespace Otakulore.Core.Services.Anime.Providers
{

    public class FourAnimeProvider : IAnimeProvider
    {

        private static string BaseEndpoint => "https://4animes.org";
        private static string SearchAnimeEndpoint => BaseEndpoint + "/search?s={0}";

        public string Id => "4a";
        public string Name => "4Anime";

        public AnimeInfo[] SearchAnime(string query)
        {
            try
            {
                var document = new HtmlWeb().Load(string.Format(SearchAnimeEndpoint, Uri.EscapeDataString(query)));
                var nodes = document.DocumentNode.SelectNodes("//div[@class='container']/div[@id='headerDIV_2']");
                var list = new List<AnimeInfo>();
                foreach (var node in nodes)
                {
                    var root = node.SelectSingleNode("./div/a");
                    if (root == null)
                        continue;
                    list.Add(new AnimeInfo
                    {
                        ImageUrl = BaseEndpoint + root.SelectSingleNode("./img").Attributes["src"].Value,
                        Title = root.SelectSingleNode("./div").InnerText,
                        Url = BaseEndpoint + root.Attributes["href"].Value
                    });
                }
                return list.ToArray();
            }
            catch (Exception exception)
            {
                CoreLogger.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

        public AnimeEpisode[] ScrapeAnimeEpisodes(string url)
        {
            try
            {
                var document = new HtmlWeb().Load(url);
                var nodes = document.DocumentNode.SelectNodes("//section[@class='single-anime-category']/div[@class='watchpage']/div/div/div/ul/li");
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
                        Number = episodeNumber,
                        Url = BaseEndpoint + root.Attributes["href"].Value
                    });
                }
                return list.ToArray();
            }
            catch (Exception exception)
            {
                CoreLogger.PostLine(exception.Message, LoggerStatus.Error);
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
                CoreLogger.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

    }

}