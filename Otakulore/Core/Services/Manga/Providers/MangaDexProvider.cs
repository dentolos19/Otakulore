using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.Services.Manga.Providers
{

    public class MangaDexProvider : IMangaProvider
    {

        private static string BaseEndpoint => "https://mangadex.tv";
        private static string SearchMangaEndpoint => BaseEndpoint + "/search?type=titles&title={0}";

        public string Id => "mdx";
        public string Name => "MangaDex";

        public MangaInfo[] SearchManga(string query)
        {
            try
            {
                var document = new HtmlWeb().Load(string.Format(SearchMangaEndpoint, query.Replace(' ', '+')));
                var nodes = document.DocumentNode.SelectNodes("//div[@class='container']/div/div");
                var list = new List<MangaInfo>();
                foreach (var node in nodes)
                {
                    var divElements = node.SelectNodes("./div");
                    var imageElement = divElements[0].SelectSingleNode("./a/img");
                    var titleElement = divElements[1].SelectSingleNode("./a");
                    list.Add(new MangaInfo
                    {
                        ImageUrl = BaseEndpoint + imageElement.Attributes["data-src"].Value,
                        Title = titleElement.Attributes["title"].Value,
                        Url = BaseEndpoint + titleElement.Attributes["href"].Value
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

        public MangaChapter[] ScrapeMangaChapters(string url)
        {
            try
            {
                var document = new HtmlWeb().Load(url);
                var nodes = document.DocumentNode.SelectNodes("//div[@class='chapter-container ']/div");
                var list = new List<MangaChapter>();
                foreach (var node in nodes)
                {
                    var itemElement = node.SelectNodes("./div/div/div")[0].SelectSingleNode("./a");
                    if (itemElement == null)
                        continue;
                    list.Add(new MangaChapter
                    {
                        Name = itemElement.InnerText,
                        Url = BaseEndpoint + itemElement.Attributes["href"].Value
                    });
                }
                list.Reverse();
                return list.ToArray();
            }
            catch (Exception exception)
            {
                CoreLogger.PostLine(exception.Message, LoggerStatus.Error);
                return null;
            }
        }

        public string[] ScrapeChapterSources(string url)
        {
            return null; // TODO: add mangadex scraping
        }

    }

}