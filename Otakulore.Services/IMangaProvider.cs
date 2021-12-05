namespace Otakulore.Services;

public interface IMangaProvider : IProvider
{

    IList<IMediaInfo> SearchManga(string query);
    IList<IMediaContent> GetMangaChapters(IMediaInfo mediaInfo);
    IList<string> GetMangaChapterSource(IMediaContent mediaContent);

}