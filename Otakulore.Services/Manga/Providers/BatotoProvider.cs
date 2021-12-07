using HtmlAgilityPack;

namespace Otakulore.Services.Manga.Providers;

public class BatotoProvider : IMangaProvider
{

    public string Name => "Batoto";
    public string Author => "dentolos19";
    public string Website => "https://bato.to";

    public IEnumerable<IMediaInfo> SearchManga(string query)
    {
        query = query.Replace(' ', '+');
        var website = new HtmlWeb().Load($"{Website}/search?word={query}");
        var mangaElements = website.DocumentNode.SelectNodes("//div[@class='series-list']/div");
        if (mangaElements is null)
            return Array.Empty<IMediaInfo>();
        var mangaList = new List<MangaInfo>();
        foreach (var storyItem in mangaElements)
        {
            var linkElement = storyItem.SelectSingleNode("./a");
            var imageElement = linkElement.SelectSingleNode("./img");
            var titleElement = storyItem.SelectSingleNode("./div/a");
            mangaList.Add(new MangaInfo
            {
                ImageUrl = imageElement.Attributes["src"].Value,
                Title = titleElement.InnerText,
                Url = linkElement.Attributes["href"].Value
            });
        }
        return mangaList;
    }

    public IEnumerable<IMediaContent> GetMangaChapters(IMediaInfo info)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> GetMangaChapterSource(IMediaContent content)
    {
        throw new NotImplementedException();
    }

}