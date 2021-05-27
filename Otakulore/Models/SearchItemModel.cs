using Otakulore.Api.Kitsu;

namespace Otakulore.Models
{

    public class SearchItemModel
    {

        public string Image { get; init; }
        public string Title { get; init; }
        public KitsuData<KitsuAnimeAttributes> Data { get; init; }

        public string Year => Data.Attributes.StartingDate.Substring(0, 4);

    }

}