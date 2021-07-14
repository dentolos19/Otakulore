using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Humanizer;
using Otakulore.Core.Services.Anime;
using Otakulore.Core.Services.Common;
using Otakulore.Core.Services.Kitsu;
using Otakulore.Core.Services.Manga;

namespace Otakulore.Core
{

    public static class ServiceUtilities
    {

        public static CommonMediaDetails CastCommonMediaDetails(KitsuData<KitsuMediaAttributes> mediaData)
        {
            DateTime? startingDate = null;
            if (!string.IsNullOrEmpty(mediaData.Attributes.StartingDate))
            {
                var dateIntegers = mediaData.Attributes.StartingDate.Split('-').Select(int.Parse).ToArray();
                startingDate = new DateTime(dateIntegers[0], dateIntegers[1], dateIntegers[2]);
            }
            DateTime? endingDate = null;
            if (!string.IsNullOrEmpty(mediaData.Attributes.EndingDate))
            {
                var dateIntegers = mediaData.Attributes.EndingDate.Split('-').Select(int.Parse).ToArray();
                endingDate = new DateTime(dateIntegers[0], dateIntegers[1], dateIntegers[2]);
            }
            return new CommonMediaDetails
            {
                KitsuId = int.Parse(mediaData.Id),
                ImageUrl = mediaData.Attributes.PosterImage.ImageUrl,
                CanoncialTitle = mediaData.Attributes.CanonicalTitle,
                AlternativeTitles = mediaData.Attributes.Titles,
                StartingDate = startingDate,
                EndingDate = endingDate,
                Synopsis = mediaData.Attributes.Synopsis,
                AverageRating = double.Parse(mediaData.Attributes.AverageRating) / 20,
            };
        }

        public static CommonMediaDetails CastCommonMediaDetails(KitsuData<KitsuAnimeAttributes> animeData)
        {
            var mediaDetails = CastCommonMediaDetails(CoreUtilities.CastObject<KitsuData<KitsuMediaAttributes>>(animeData));
            mediaDetails.MediaType = CommonMediaType.Anime;
            mediaDetails.MediaFormat = animeData.Attributes.Format.Humanize();
            mediaDetails.EpisodeCount = animeData.Attributes.EpisodeCount;
            return mediaDetails;
        }

        public static CommonMediaDetails CastCommonMediaDetails(KitsuData<KitsuMangaAttributes> mangaData)
        {
            var mediaDetails = CastCommonMediaDetails(CoreUtilities.CastObject<KitsuData<KitsuMediaAttributes>>(mangaData));
            mediaDetails.MediaType = mangaData.Attributes.Format == KitsuMangaFormat.Novel ? CommonMediaType.Novel : CommonMediaType.Manga;
            mediaDetails.MediaFormat = mangaData.Attributes.Format.Humanize();
            mediaDetails.ChapterCount = mangaData.Attributes.ChapterCount;
            mediaDetails.VolumeCount = mangaData.Attributes.VolumeCount;
            return mediaDetails;
        }

        public static IAnimeProvider[] GetAnimeProviders()
        {
            var providerList = new List<IAnimeProvider>();
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.GetInterface(nameof(IAnimeProvider)) == null)
                    continue;
                var provider = (IAnimeProvider)Activator.CreateInstance(type);
                providerList.Add(provider);
            }
            return providerList.ToArray();
        }

        public static IMangaProvider[] GetMangaProviders()
        {
            var providerList = new List<IMangaProvider>();
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.GetInterface(nameof(IMangaProvider)) == null)
                    continue;
                var provider = (IMangaProvider)Activator.CreateInstance(type);
                providerList.Add(provider);
            }
            return providerList.ToArray();
        }

    }

}