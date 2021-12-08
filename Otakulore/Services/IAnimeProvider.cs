using System.Collections.Generic;

namespace Otakulore.Services;

public interface IAnimeProvider : IProvider
{

    IEnumerable<IMediaInfo> SearchAnime(string query);
    IEnumerable<IMediaContent> GetAnimeEpisodes(IMediaInfo info);
    string GetAnimeEpisodeSource(IMediaContent content);

}