﻿using System;
using System.Collections.Generic;

namespace Otakulore.Core.Services.Common
{

    public class CommonMediaDetails
    {

        public int KitsuId { get; set; }
        public CommonMediaType MediaType { get; set; }
        public string MediaFormat { get; set; }

        public string ImageUrl { get; set; }
        public string CanoncialTitle { get; set; }
        public IDictionary<string, string> AlternativeTitles { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public string Synopsis { get; set; }
        public double AverageRating { get; set; }

        public int? EpisodeCount { get; set; }
        public int? ChapterCount { get; set; }
        public int? VolumeCount { get; set; }

    }

}