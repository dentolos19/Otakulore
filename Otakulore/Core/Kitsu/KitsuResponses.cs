﻿using System.Text.Json.Serialization;

namespace Otakulore.Core.Kitsu
{

    public class KitsuResponses
    {

        [JsonPropertyName("data")] public KitsuData[] Data { get; set; }
        [JsonPropertyName("links")] public KitsuLinks Links { get; set; }

    }

}