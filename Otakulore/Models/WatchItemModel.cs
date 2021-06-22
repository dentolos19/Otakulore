using Otakulore.Core.Anime;

namespace Otakulore.Models
{

    public class WatchItemModel
    {

        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string EpisodesUrl { get; set; }
        public AnimeProvider Provider { get; set; }

    }

}