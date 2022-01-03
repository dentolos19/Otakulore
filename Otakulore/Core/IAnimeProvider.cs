using System;
using System.Collections.Generic;

namespace Otakulore.Core;

public interface IAnimeProvider : IProvider
{

    IEnumerable<MediaSource> SearchAnime(string query);
    IEnumerable<MediaContent> GetAnimeEpisodes(MediaSource source);
    Uri ExtractVideoUrl(MediaContent content);

}