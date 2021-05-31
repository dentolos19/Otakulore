using Otakulore.Core.AnimeServices;

namespace Otakulore.Models
{

    public class EpisodeItemModel
    {

        public string EpisodeNumber { get; init; }
        public StreamingService Service { get; init; }
        public string WatchUrl { get; init; }

    }

}