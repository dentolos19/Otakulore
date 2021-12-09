using System.Collections.Generic;

namespace Otakulore.Core;

public interface IMangaProvider : IProvider
{

    IEnumerable<MediaInfo> SearchManga(string query);
    IEnumerable<MediaContent> GetMangaChapters(MediaInfo info);

}