using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace Otakulore.Core.Providers;

public class MangakakalotProvider : IMangaProvider
{

    public ImageSource Image => (ImageSource)Application.Current.Resources["MangakakalotAsset"];
    public string Name => "Mangakakalot";
    public string Url => "https://mangakakalot.com";

    public IEnumerable<MediaSource> SearchManga(string query)
    {
        query = query.Replace(' ', '_');
        var website = ScrapingService.HtmlWeb.Load($"{Url}/search/story/{query}");
        var mangaElements = website.DocumentNode.SelectNodes("//div[@class='panel_story_list']/div[@class='story_item']");
        if (mangaElements is not { Count: > 0 })
            return Array.Empty<MediaSource>();
        var mangaList = new List<MediaSource>();
        foreach (var mangaElement in mangaElements)
        {
            var linkElement = mangaElement.SelectSingleNode("./a");
            var imageElement = linkElement.SelectSingleNode("./img");
            mangaList.Add(new MediaSource
            {
                ImageUrl = imageElement.Attributes["src"].Value,
                Title = imageElement.Attributes["alt"].Value,
                Url = linkElement.Attributes["href"].Value
            });
        }
        return mangaList;
    }

    public IEnumerable<MediaContent> GetMangaChapters(MediaSource source)
    {
        var website = ScrapingService.HtmlWeb.Load(source.Url);
        var chapterElements = website.DocumentNode.SelectNodes("//div[@class='panel-story-chapter-list']/ul/li");
        var chapterList = new List<MediaContent>();
        foreach (var chapterItem in chapterElements)
        {
            var linkElement = chapterItem.SelectSingleNode("./a");
            chapterList.Add(new MediaContent
            {
                Name = linkElement.InnerText,
                Url = linkElement.Attributes["href"].Value
            });
        }
        chapterList.Reverse();
        return chapterList;
    }

}