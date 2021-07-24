using Humanizer;
using Otakulore.Core.Services.Common;
using Windows.UI.Xaml;

namespace Otakulore.ViewModels
{

    public class DetailsOverviewViewModel
    {

        public Visibility AnimeInfoVisibility { get; set; }
        public Visibility MangaInfoVisibility { get; set; }

        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string Format { get; set; }
        public string Status { get; set; }
        public string Episodes { get; set; }
        public string Chapters { get; set; }
        public string Volumes { get; set; }
        public string StartingDate { get; set; }
        public string EndingDate { get; set; }

        public static DetailsOverviewViewModel CreateViewModel(CommonMediaDetails data)
        {
            return new DetailsOverviewViewModel
            {
                AnimeInfoVisibility = data.MediaType == CommonMediaType.Anime ? Visibility.Visible : Visibility.Collapsed,
                MangaInfoVisibility = data.MediaType != CommonMediaType.Anime ? Visibility.Visible : Visibility.Collapsed,
                ImageUrl = data.ImageUrl,
                Title = data.CanonicalTitle,
                Synopsis = data.Synopsis,
                Format = data.MediaFormat,
                Status = data.MediaStatus.Humanize(),
                Episodes = data.EpisodeCount?.ToString() ?? "???",
                Chapters = data.ChapterCount?.ToString() ?? "???",
                Volumes = data.VolumeCount?.ToString() ?? "???",
                StartingDate = data.StartingDate.HasValue ? $"{data.StartingDate:yyyy-MM-dd}" : "???",
                EndingDate = data.EndingDate.HasValue ? $"{data.EndingDate:yyyy-MM-dd}" : "???"
            };
        }

    }

}