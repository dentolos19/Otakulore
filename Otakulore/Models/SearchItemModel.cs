using Otakulore.Core.Kitsu;

namespace Otakulore.Models
{

    public class SearchItemModel
    {

        public string Image { get; init; }
        public string Title { get; init; }
        public object Data { get; init; }

        public string Subtitle
        {
            get
            {
                var subtitle = string.Empty;
                if (Data is KitsuAnimeAttributes attributes)
                {
                    subtitle += attributes.StartingDate.Substring(0, 4);
                    subtitle += " | ";
                    subtitle += attributes.AgeRating;
                    subtitle += " | ";
                    subtitle += attributes.Status;
                }
                return subtitle;
            }
        }

    }

}