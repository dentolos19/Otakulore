namespace Otakulore.Core.Services.Manga
{

    public interface IMangaProvider
    {

        string Id { get; }
        string Name { get; }

        object[] SearchManga(string query);
        object[] ScrapeMangaChapters(string url);
        string[] ScrapeChapterSources(string url);

    }

}