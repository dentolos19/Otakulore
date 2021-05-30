using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace Otakulore.Core.AnimeServices
{

    public class AnimeScraper
    {

        private static string BaseEndpoint => "https://www1.gogoanime.ai";
        private static string SearchEndpoint => BaseEndpoint + "/search.html?keyword={0}";
        
        public static AnimePoster[] SearchAnime(string query)
        {
            var document = new HtmlWeb().Load(string.Format(SearchEndpoint, Uri.EscapeDataString(query)));
            var nodes = document.DocumentNode.SelectNodes("//div[@class='last_episodes']/ul[@class='items']/li");
            var list = new List<AnimePoster>();
            foreach (var node in nodes)
            {
                var root = node.SelectSingleNode("./div/a");
                list.Add(new AnimePoster
                {
                    ImageUrl = root.SelectSingleNode("./img").Attributes["src"].Value,
                    Title = root.Attributes["title"].Value,
                    ReleaseYear = node.SelectSingleNode("./p[@class='released']").InnerText.Trim().Remove(9),
                    InfoLink = BaseEndpoint + root.Attributes["href"]
                });
            }
            return list.ToArray();
        }

    }

}