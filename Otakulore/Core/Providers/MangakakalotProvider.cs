using System;
using System.Collections.Generic;

namespace Otakulore.Core.Providers;

public class MangakakalotProvider : IMangaProvider
{

    public Uri ImageUrl => new("https://mangakakalot.com/themes/home/icons/logo.png");
    public string Name => "Mangakakalot";
    public Uri Url => new("https://mangakakalot.com");

    public IEnumerable<MediaInfo> SearchManga(string query)
    {
        query = query.Replace(' ', '_');
        var website = ScrapingServices.HtmlWeb.Load($"{Url}/search/story/{query}");
        var mangaElements = website.DocumentNode.SelectNodes("//div[@class='panel_story_list']/div[@class='story_item']");
        if (mangaElements is not { Count: > 0 })
            return Array.Empty<MediaInfo>();
        var mangaList = new List<MediaInfo>();
        foreach (var mangaElement in mangaElements)
        {
            var linkElement = mangaElement.SelectSingleNode("./a");
            var imageElement = linkElement.SelectSingleNode("./img");
            mangaList.Add(new MediaInfo
            {
                ImageUrl = new Uri(imageElement.Attributes["src"].Value),
                Title = imageElement.Attributes["alt"].Value,
                Url = new Uri(linkElement.Attributes["href"].Value)
            });
        }
        return mangaList;
    }

    public IEnumerable<MediaContent> GetMangaChapters(MediaInfo info)
    {
        var website = ScrapingServices.HtmlWeb.Load(info.Url);
        var chapterElements = website.DocumentNode.SelectNodes("//div[@class='panel-story-chapter-list']/ul/li");
        var chapterList = new List<MediaContent>();
        foreach (var chapterItem in chapterElements)
        {
            var linkElement = chapterItem.SelectSingleNode("./a");
            chapterList.Add(new MediaContent
            {
                Name = linkElement.InnerText,
                Url = new Uri(linkElement.Attributes["href"].Value)
            });
        }
        chapterList.Reverse();
        return chapterList;
    }

}