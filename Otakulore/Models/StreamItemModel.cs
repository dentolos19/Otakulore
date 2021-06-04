using Otakulore.Core.Anime;

namespace Otakulore.Models
{

    public class StreamItemModel
    {

        public string ImageUrl { get; init; }
        public string Title { get; init; }
        public AnimeProvider Service { get; init; }
        public string EpisodesUrl { get; init; }

    }

}