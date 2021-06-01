using Otakulore.Core;

namespace Otakulore.Models
{

    public class StreamItemModel
    {

        public string ImageUrl { get; init; }
        public string Title { get; init; }
        public StreamingService Service { get; init; }
        public string EpisodesUrl { get; init; }

    }

}