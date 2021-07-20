using System;
using System.Linq;
using HtmlAgilityPack;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.Services.Manga.Providers
{

    public class ManganatoProvider : IMangaProvider
    {

        private static string BaseEndpoint => "https://manganato.com";
        private static string SearchMangaEndpoint => BaseEndpoint + "/search/story/{0}";

        public string Id => "mnto";
        public string Name => "Manganato";

        public MangaInfo[] SearchManga(string query)
        {
            try
            {
                var document = new HtmlWeb().Load(string.Format(SearchMangaEndpoint, Uri.EscapeDataString(query).Replace("%20", "_")));
                var nodes = document.DocumentNode.SelectNodes("//div[@class='panel-search-story']/div[@class='search-story-item']");
                return nodes?.Select(node => node.SelectSingleNode("./a")).Select(node => new MangaInfo
                {
                    ImageUrl = node.SelectSingleNode("./img").Attributes["src"].Value,
                    Title = node.Attributes["title"].Value,
                    Url = node.Attributes["href"].Value
                }).ToArray();
            }
            catch (Exception exception)
            {
                BasicLogger.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

        public object[] ScrapeMangaChapters(string url)
        {
            try
            {
                return null; // TODO
            }
            catch (Exception exception)
            {
                BasicLogger.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

        public string[] ScrapeChapterSources(string url)
        {
            try
            {
                return null; // TODO
            }
            catch (Exception exception)
            {
                BasicLogger.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

    }

}