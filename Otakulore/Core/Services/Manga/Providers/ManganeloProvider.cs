using HtmlAgilityPack;
using Otakulore.Core.Helpers;
using System;
using System.Linq;

namespace Otakulore.Core.Services.Manga.Providers
{

    public class ManganeloProvider : IMangaProvider
    {

        private static string BaseEndpoint => "https://manganelo.tv";
        private static string SearchMangaEndpoint => BaseEndpoint + "/search/{0}";

        public string Id => "mnl";
        public string Name => "Manganelo";

        public MangaInfo[] SearchManga(string query)
        {
            try
            {
                var document = new HtmlWeb().Load(string.Format(SearchMangaEndpoint, Uri.EscapeDataString(query)));
                var nodes = document.DocumentNode.SelectNodes("//div[@class='panel-search-story']/div[@class='search-story-item']");
                return nodes.Select(node => node.SelectSingleNode("./a")).Select(node => new MangaInfo
                {
                    ImageUrl = BaseEndpoint + node.SelectSingleNode("./img").Attributes["src"].Value,
                    Title = node.Attributes["title"].Value,
                    Url = BaseEndpoint + node.Attributes["href"].Value
                }).ToArray();
            }
            catch (Exception exception)
            {
                CoreLogger.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

        public MangaChapter[] ScrapeMangaChapters(string url)
        {
            try
            {
                var document = new HtmlWeb().Load(url);
                var nodes = document.DocumentNode.SelectNodes("//ul[@class='row-content-chapter']/li[@class='a-h']");
                return nodes.Select(node => node.SelectSingleNode("./a")).Select(root => new MangaChapter
                {
                    Name = root.InnerText,
                    Url = BaseEndpoint + root.Attributes["href"].Value
                }).Reverse().ToArray();
            }
            catch (Exception exception)
            {
                CoreLogger.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

        public string[] ScrapeChapterSources(string url)
        {
            try
            {
                var document = new HtmlWeb().Load(url);
                var nodes = document.DocumentNode.SelectNodes("//div[@class='container-chapter-reader']/img");
                return nodes.Select(node => node.Attributes["data-src"].Value).ToArray();
            }
            catch (Exception exception)
            {
                CoreLogger.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

    }

}