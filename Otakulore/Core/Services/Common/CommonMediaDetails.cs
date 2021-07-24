using Otakulore.Core.Services.Kitsu;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Otakulore.Core.Services.Common
{

    public class CommonMediaDetails
    {

        public int KitsuId { get; set; }
        public CommonMediaType MediaType { get; set; }
        [JsonIgnore] public string MediaFormat { get; set; }
        [JsonIgnore] public KitsuMediaStatus MediaStatus { get; set; }

        [JsonIgnore] public string ImageUrl { get; set; }
        [JsonIgnore] public string CanonicalTitle { get; set; }
        [JsonIgnore] public IDictionary<string, string> AlternativeTitles { get; set; }
        [JsonIgnore] public DateTime? StartingDate { get; set; }
        [JsonIgnore] public DateTime? EndingDate { get; set; }
        [JsonIgnore] public string Synopsis { get; set; }
        [JsonIgnore] public double? AverageRating { get; set; }

        [JsonIgnore] public int? EpisodeCount { get; set; }
        [JsonIgnore] public int? ChapterCount { get; set; }
        [JsonIgnore] public int? VolumeCount { get; set; }

    }

}