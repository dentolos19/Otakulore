namespace Otakulore.Core.Services.Anime
{

    public interface IAnimeProvider
    {

        string ProviderId { get; }
        string ProviderName { get; }

        AnimeInfo[] ScrapeAnimes(string query);
        AnimeEpisode[] ScrapeAnimeEpisodes(string url);
        string ScrapeEpisodeSource(string url);

    }

}