using Windows.UI.Xaml;
using Otakulore.Core.Services.Anime;
using Otakulore.Core.Services.Kitsu;

namespace Otakulore.Models
{

    public class EpisodeItemModel
    {
        
        public string EpisodeNumber { get; set; }
        public string WatchUrl { get; set; }

        public static EpisodeItemModel CreateModel(AnimeEpisode episode)
        {
            var episodeItem = new EpisodeItemModel { WatchUrl = episode.WatchUrl };
            if (episode.EpisodeNumber != null)
                episodeItem.EpisodeNumber = "Episode " + episode.EpisodeNumber;
            else
                episodeItem.EpisodeNumber = "Unknown Episode";
            return episodeItem;
        }

    }

}