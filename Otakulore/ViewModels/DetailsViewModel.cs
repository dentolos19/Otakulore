using System.ComponentModel;
using Humanizer;
using Otakulore.Core.Kitsu;

namespace Otakulore.ViewModels
{

    public class DetailsViewModel : INotifyPropertyChanged
    {

        private bool _isLoading;

        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string Format { get; set; }
        public string Status { get; set; }
        public string Episodes { get; set; }
        public string StartingDate { get; set; }
        public string EndingDate { get; set; }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLoading)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static DetailsViewModel CreateViewModel(KitsuData data)
        {
            return new DetailsViewModel
            {
                ImageUrl = data.Attributes.PosterImage.OriginalImageUrl,
                Title = data.Attributes.CanonicalTitle,
                Synopsis = data.Attributes.Synopsis,
                
                Format = data.Attributes.Format.Humanize(),
                Status = data.Attributes.Status.Humanize(),
                Episodes = data.Attributes.EpisodeCount?.ToString() ?? "???",
                StartingDate = string.IsNullOrEmpty(data.Attributes.StartingDate) ? "TBA" : data.Attributes.StartingDate,
                EndingDate = string.IsNullOrEmpty(data.Attributes.EndingDate) ? "TBA" : data.Attributes.EndingDate
            };
        }

    }

}