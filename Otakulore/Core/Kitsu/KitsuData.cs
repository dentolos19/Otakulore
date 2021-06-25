﻿using System.Text.Json.Serialization;

namespace Otakulore.Core.Kitsu
{

    public class KitsuData
    {

        [JsonPropertyName("id")]
        public string Id { get; set; } // int
        
        [JsonPropertyName("attributes")]
        public KitsuMediaAttributes Attributes { get; set; }

    }

}