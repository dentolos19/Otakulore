﻿using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace Otakulore.Core.AnimeServices.Scrapers
{

    public static class GogoanimeScraper
    {

        private static string BaseEndpoint => "https://www1.gogoanime.ai";
        private static string SearchAnimeEndpoint => BaseEndpoint + "/search.html?keyword={0}";
        
        public static AnimePoster[]? SearchAnime(string query)
        {
            try
            {
                var document = new HtmlWeb().Load(string.Format(SearchAnimeEndpoint, Uri.EscapeDataString(query)));
                var nodes = document.DocumentNode.SelectNodes("//div[@class='last_episodes']/ul[@class='items']/li");
                if (nodes == null)
                    return null;
                var list = new List<AnimePoster>();
                foreach (var node in nodes)
                {
                    var root = node.SelectSingleNode("./div/a");
                    var releaseYear = node.SelectSingleNode("./p[@class='released']").InnerText.Trim();
                    list.Add(new AnimePoster
                    {
                        ImageUrl = root.SelectSingleNode("./img").Attributes["src"].Value,
                        Title = root.Attributes["title"].Value,
                        ReleaseYear = releaseYear.Substring(releaseYear.Length - 4),
                        InfoLink = BaseEndpoint + root.Attributes["href"]
                    });
                }
                return list.ToArray();
            }
            catch
            {
                return null;
            }
        }

    }

}