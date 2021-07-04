using Otakulore.Core.Services.Anime;

namespace Otakulore.Models
{

    public class WatchItemModel
    {

        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string EpisodesUrl { get; set; }
        public IAnimeProvider Provider { get; set; }

    }

}