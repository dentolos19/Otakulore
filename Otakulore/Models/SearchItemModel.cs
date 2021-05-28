using Humanizer;
using Kitsu.Anime;

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
                if (Data is AnimeAttributesModel attributes)
                {
                    subtitle += attributes.StartDate.Substring(0, 4);
                    subtitle += " | ";
                    subtitle += attributes.AgeRating;
                    subtitle += " | ";
                    subtitle += attributes.Status.Transform(To.TitleCase);
                }
                return subtitle;
            }
        }

    }

}