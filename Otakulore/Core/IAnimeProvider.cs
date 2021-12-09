using System.Collections.Generic;

namespace Otakulore.Core;

public interface IAnimeProvider : IProvider
{

    IEnumerable<MediaInfo> SearchAnime(string query);
    IEnumerable<MediaContent> GetAnimeEpisodes(MediaInfo info);

}