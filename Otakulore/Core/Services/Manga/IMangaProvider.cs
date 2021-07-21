namespace Otakulore.Core.Services.Manga
{

    public interface IMangaProvider
    {

        string Id { get; }
        string Name { get; }

        MangaInfo[] SearchManga(string query);
        MangaChapter[] ScrapeMangaChapters(string url);
        string[] ScrapeChapterSources(string url);

    }

}