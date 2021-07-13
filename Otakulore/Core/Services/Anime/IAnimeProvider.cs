namespace Otakulore.Core.Services.Anime
{

    public interface IAnimeProvider
    {

        string Id { get; }
        string Name { get; }

        AnimeInfo[] SearchAnime(string query);
        AnimeEpisode[] ScrapeAnimeEpisodes(string url);
        string ScrapeEpisodeSource(string url);

    }

}