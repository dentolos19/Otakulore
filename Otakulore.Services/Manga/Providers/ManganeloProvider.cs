using HtmlAgilityPack;

namespace Otakulore.Services.Manga.Providers;

public class ManganeloProvider : IMangaProvider
{

    private static string BaseEndpoint => "https://manganelo.tv";
    private static string SearchMangaEndpoint => BaseEndpoint + "/search/{0}";

    public string Name => "Manganelo Provider";
    public string Author => "dentolos19";

    public void Initialize()
    {
        // do nothing
    }

    public IList<IMediaInfo> SearchManga(string query)
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

    public IList<IMediaContent> GetMangaChapters(IMediaInfo mediaInfo)
    {
        if (mediaInfo is not MangaInfo info)
            throw new ArgumentException(null, nameof(mediaInfo));
        var document = new HtmlWeb().Load(info.Url);
        var nodes = document.DocumentNode.SelectNodes("//ul[@class='row-content-chapter']/li[@class='a-h']");
        return nodes.Select(node => node.SelectSingleNode("./a")).Select(root => new MangaChapter
        {
            Index = 0, // TODO: get chapter index
            Title = root.InnerText,
            Url = BaseEndpoint + root.Attributes["href"].Value
        }).Reverse().ToArray();
    }

    public IList<string> GetMangaChapterSource(IMediaContent mediaContent)
    {
        if (mediaContent is not MangaChapter chapter)
            throw new ArgumentException(null, nameof(mediaContent));
        var document = new HtmlWeb().Load(chapter.Url);
        var nodes = document.DocumentNode.SelectNodes("//div[@class='container-chapter-reader']/img");
        return nodes.Select(node => node.Attributes["data-src"].Value).ToArray();
    }

}