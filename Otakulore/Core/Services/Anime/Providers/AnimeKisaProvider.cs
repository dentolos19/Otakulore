using HtmlAgilityPack;
using Otakulore.Core.Helpers;
using System;
using System.Collections.Generic;

namespace Otakulore.Core.Services.Anime.Providers
{

    public class AnimeKisaProvider : IAnimeProvider
    {

        public string ProviderId => "aktv";
        public string ProviderName => "animekisa.tv";

        private static string BaseEndpoint => "https://animekisa.tv";
        private static string SearchAnimeEndpoint => BaseEndpoint + "/search?q={0}";

        public AnimeInfo[] ScrapeAnimes(string query)
        {
            try
            {
                var document = new HtmlWeb().Load(string.Format(SearchAnimeEndpoint, Uri.EscapeDataString(query)));
                var parentNodes = document.DocumentNode.SelectNodes("//div[@class='lisbox22']/div[@class='similarboxmain']");
                if (parentNodes == null)
                    return null;
                var list = new List<AnimeInfo>();
                foreach (var parentNode in parentNodes)
                {
                    var nodes = parentNode.SelectNodes("./div/a[@class='an']");
                    if (nodes == null)
                        continue;
                    foreach (var node in nodes)
                    {
                        list.Add(new AnimeInfo
                        {
                            ImageUrl = BaseEndpoint + node.SelectSingleNode("./div/div/div[@class='similarpic']/img").Attributes["src"].Value,
                            Title = node.SelectSingleNode("./div/div/div[@class='similard']/div/div[@class='similardd']").InnerText.Trim(),
                            EpisodesUrl = BaseEndpoint + node.Attributes["href"].Value
                        });
                    }
                }
                return list.ToArray();
            }
            catch (Exception exception)
            {
                LogPoster.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

        public AnimeEpisode[] ScrapeAnimeEpisodes(string url)
        {
            try
            {
                var document = new HtmlWeb().Load(url);
                var nodes = document.DocumentNode.SelectNodes("//div[@class='infoepboxmain']/div[@class='infoepbox']/a[@class='infovan']");
                if (nodes == null)
                    return null;
                var list = new List<AnimeEpisode>();
                foreach (var node in nodes)
                {
                    int? episodeNumber = null;
                    if (int.TryParse(node.SelectSingleNode("./div/div/div/div[@class='infoept2']/div").InnerText, out var result))
                        episodeNumber = result;
                    list.Add(new AnimeEpisode
                    {
                        EpisodeNumber = episodeNumber,
                        WatchUrl = BaseEndpoint + "/" + node.Attributes["href"].Value
                    });
                }
                list.Reverse();
                return list.ToArray();
            }
            catch (Exception exception)
            {
                LogPoster.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

        public string ScrapeEpisodeSource(string url)
        {
            try
            {
                var document = new HtmlWeb().Load(url);
                document = new HtmlWeb().Load(document.DocumentNode.SelectNodes("//div[@id='main']/script")[1].InnerText.GetStringBetween("VidStreaming = \"", "\""));
                return document.DocumentNode.SelectSingleNode("//div[@class='videocontent']/script").InnerText?.GetStringBetween("file: '", "'");
            }
            catch (Exception exception)
            {
                LogPoster.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

    }

}