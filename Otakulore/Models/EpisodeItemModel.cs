using Windows.UI.Xaml;
using Otakulore.Core.Services.Anime;
using Otakulore.Core.Services.Kitsu;

namespace Otakulore.Models
{

    public class EpisodeItemModel
    {

        public Visibility InfoVisibility { get; set; } = Visibility.Collapsed;
        public Visibility SynopsisVisibility { get; set; }
        public Visibility AirDateVisibility { get; set; }

        public string ImageUrl { get; set; }
        public string EpisodeTitle { get; set; }
        public string EpisodeNumber { get; set; }
        public string SeasonNumber { get; set; }
        public string Synopsis { get; set; }
        public string AirDate { get; set; }
        public string WatchUrl { get; set; }

        public static EpisodeItemModel CreateModel(AnimeEpisode episode, KitsuEpisodeAttributes episodeAttributes = null)
        {
            var episodeItem = new EpisodeItemModel { WatchUrl = episode.WatchUrl };
            if (episode.EpisodeNumber != null)
                episodeItem.EpisodeNumber = "Episode " + episode.EpisodeNumber;
            else
                episodeItem.EpisodeNumber = "Unknown Episode";
            if (episodeAttributes != null)
            {
                if (episodeAttributes.Thumbnail != null)
                    episodeItem.ImageUrl = episodeAttributes.Thumbnail.ImageUrl;
                episodeItem.EpisodeTitle = episodeAttributes.CanonicalTitle;
                episodeItem.SeasonNumber = "Season " + episodeAttributes.Season;
                if (!string.IsNullOrEmpty(episodeAttributes.Synopsis))
                    episodeItem.Synopsis = episodeAttributes.Synopsis;
                else
                    episodeItem.SynopsisVisibility = Visibility.Collapsed;
                if (!string.IsNullOrEmpty(episodeAttributes.AirDate))
                    episodeItem.AirDate = episodeAttributes.AirDate;
                else
                    episodeItem.AirDateVisibility = Visibility.Collapsed;
                episodeItem.InfoVisibility = Visibility.Visible;
            }
            return episodeItem;
        }

    }

}