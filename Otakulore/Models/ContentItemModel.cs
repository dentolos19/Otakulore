using Humanizer;
using Otakulore.Core.Services.Common;
using Windows.UI.Xaml;
using Otakulore.Core;

namespace Otakulore.Models
{

    public class ContentItemModel
    {

        public Visibility RatingVisibility { get; set; }
        public Visibility SynopsisVisibility { get; set; }

        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Synopsis { get; set; }
        public double Rating { get; set; }

        public int LevenshteinDistance { get; set; }
        public CommonMediaDetails Data { get; set; }
        
        public static ContentItemModel CreateModel(CommonMediaDetails data)
        {
            var model = new ContentItemModel
            {
                ImageUrl = data.ImageUrl,
                Title = data.CanonicalTitle,
                Subtitle = (data.StartingDate.HasValue ? $"{data.StartingDate:yyyy}" : "????") + " | " +
                           data.MediaFormat + " | " +
                           data.MediaStatus.Humanize(),
                Data = data
            };
            if (!string.IsNullOrEmpty(data.Synopsis))
                model.Synopsis = data.Synopsis;
            else
                model.SynopsisVisibility = Visibility.Collapsed;
            if (data.AverageRating.HasValue)
                model.Rating = (double)data.AverageRating;
            else
                model.RatingVisibility = Visibility.Collapsed;
            return model;
        }

    }

}