﻿using System.Text.Json.Serialization;

namespace Otakulore.Api.Kitsu
{

    public class KitsuResponse<T>
    {

        [JsonPropertyName("data")] public KitsuData<T>[] Data { get; init; }
        [JsonPropertyName("meta")] public KitsuMeta Meta { get; init; }
        [JsonPropertyName("links")] public KitsuLinks Links { get; init; }

    }

}