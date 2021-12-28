using System.Collections.Generic;

namespace Otakulore.Core;

public interface IMangaProvider : IProvider
{

    IEnumerable<MediaSource> SearchManga(string query);
    IEnumerable<MediaContent> GetMangaChapters(MediaSource source);

}