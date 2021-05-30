using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace Otakulore.Core.AnimeServices.Scrapers
{

    public static class FourAnimeScraper
    {

        private static string BaseEndpoint => "https://4anime.to";
        private static string SearchAnimeEndpoint => BaseEndpoint + "/?s={0}";

        public static AnimePoster[] SearchAnime(string query)
        {
            var document = new HtmlWeb().Load(string.Format(SearchAnimeEndpoint, Uri.EscapeDataString(query)));
            var nodes = document.DocumentNode.SelectNodes("//div[@class='container']/div[@id='headerDIV_2']");
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
                    ReleaseYear = root.SelectNodes("./span").First().InnerText,
                    InfoLink = root.Attributes["href"].Value
                });
            }
            return list.ToArray();
        }

    }

}