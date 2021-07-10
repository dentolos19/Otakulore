using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using HtmlAgilityPack;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.Services.Anime.Providers
{

    public class AniMixPlayProvider : IAnimeProvider
    {

        private static string ScrapeAnimesEndpoint => "https://cdn.animixplay.to/api/search";

        public string Id => "amp";
        public string Name => "AniMixPlay";

        public AnimeInfo[] ScrapeAnimes(string query)
        {
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(ScrapeAnimesEndpoint);
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/x-www-form-urlencoded";
                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                    streamWriter.Write("qfast=" + Uri.EscapeDataString(query));
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    return null;
                string json;
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    json = streamReader.ReadToEnd();
                if (string.IsNullOrEmpty(json))
                    return null;
                var html = JsonDocument.Parse(json).RootElement.GetProperty("result").GetString();
                if (string.IsNullOrEmpty(html))
                    return null;
                var document = new HtmlDocument();
                document.LoadHtml(html);
                var nodes = document.DocumentNode.SelectNodes("/li");
                var list = new List<AnimeInfo>();
                foreach (var node in nodes)
                {
                    list.Add(new AnimeInfo
                    {
                        ImageUrl = node.SelectSingleNode("./div[@class='searchimg']/a/img").Attributes["src"].Value,
                        Title = node.SelectSingleNode("./div[@class='details']/p[@class='name']/a").InnerText,
                        EpisodesUrl = "" // TODO: add episodes url to animixplay
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
            return null;
        }

        public string ScrapeEpisodeSource(string url)
        {
            return null;
        }

    }

}