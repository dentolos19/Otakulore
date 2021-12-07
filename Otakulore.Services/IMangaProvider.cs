namespace Otakulore.Services;

public interface IMangaProvider : IProvider
{

    IEnumerable<IMediaInfo> SearchManga(string query);
    IEnumerable<IMediaContent> GetMangaChapters(IMediaInfo info);
    IEnumerable<string> GetMangaChapterSource(IMediaContent content);

}