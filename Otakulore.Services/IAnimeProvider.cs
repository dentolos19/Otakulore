namespace Otakulore.Services;

public interface IAnimeProvider : IProvider
{

    IList<IMediaInfo> SearchAnime(string query);
    IList<IMediaContent> GetAnimeEpisodes(IMediaInfo mediaInfo);
    string GetAnimeEpisodeSource(IMediaContent mediaContent);

}