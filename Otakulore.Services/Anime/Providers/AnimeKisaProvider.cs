namespace Otakulore.Services.Anime.Providers;

public class AnimeKisaProvider : IAnimeProvider
{

    public string Name => "AnimeKisa Provider";
    public string Author => "dentolos19";

    public void Initialize()
    {
        // do nothing
    }

    public IList<IMediaInfo> SearchAnime(string query)
    {
        throw new NotImplementedException();
    }

    public IList<IMediaContent> GetAnimeEpisodes(IMediaInfo mediaInfo)
    {
        throw new NotImplementedException();
    }

    public string GetAnimeEpisodeSource(IMediaContent mediaContent)
    {
        throw new NotImplementedException();
    }

}