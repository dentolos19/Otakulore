using HtmlAgilityPack;

namespace Otakulore.Services.Manga.Providers;

public class MangakakalotProvider : IMangaProvider
{

    public string Name => "Mangakakalot";
    public string Author => "dentolos19";
    public string Website => "https://mangakakalot.com";

    public IEnumerable<IMediaInfo> SearchManga(string query)
    {
        query = query.Replace(' ', '_');
        var website = new HtmlWeb().Load($"{Website}/search/story/{query}");
        var mangaElements = website.DocumentNode.SelectNodes("//div[@class='panel_story_list']/div[@class='story_item']");
        if (mangaElements is null)
            return Array.Empty<IMediaInfo>();
        var mangaList = new List<MangaInfo>();
        foreach (var storyItem in mangaElements)
        {
            var linkElement = storyItem.SelectSingleNode("./a");
            var imageElement = linkElement.SelectSingleNode("./img");
            mangaList.Add(new MangaInfo
            {
                ImageUrl = imageElement.Attributes["src"].Value,
                Title = imageElement.Attributes["alt"].Value,
                Url = linkElement.Attributes["href"].Value
            });
        }
        return mangaList;
    }

    public IEnumerable<IMediaContent> GetMangaChapters(IMediaInfo info)
    {
        if (info is not MangaInfo mangaInfo)
            return Array.Empty<IMediaContent>(); // TODO: throw exception
        var website = new HtmlWeb().Load(mangaInfo.Url);
        var chapterElements = website.DocumentNode.SelectNodes("//div[@class='panel-story-chapter-list']/ul/li");
        var chapterList = new List<MangaChapter>();
        foreach (var chapterItem in chapterElements)
        {
            var linkElement = chapterItem.SelectSingleNode("./a");
            chapterList.Add(new MangaChapter
            {
                Name = linkElement.InnerText,
                Url = linkElement.Attributes["href"].Value
            });
        }
        chapterList.Reverse();
        return chapterList;
    }

    public IEnumerable<string> GetMangaChapterSource(IMediaContent content)
    {
        if (content is not MangaChapter mangaChapter)
            return Array.Empty<string>(); // TODO: throw exception
        var website = new HtmlWeb().Load(mangaChapter.Url);
        var imageElements = website.DocumentNode.SelectNodes("//div[@class='container-chapter-reader']/img");
        return imageElements.Select(element => element.Attributes["src"].Value);
    }

}