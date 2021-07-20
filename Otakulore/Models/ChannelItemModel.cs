using Otakulore.Core.Services.Anime;
using Otakulore.Core.Services.Manga;

namespace Otakulore.Models
{

    public class ChannelItemModel
    {

        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public IAnimeProvider AnimeProvider { get; set; }
        public IMangaProvider MangaProvider { get; set; }

    }

}