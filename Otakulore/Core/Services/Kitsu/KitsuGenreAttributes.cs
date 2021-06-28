using System.Text.Json.Serialization;

namespace Otakulore.Core.Services.Kitsu
{

    public class KitsuGenreAttributes
    {
        
        [JsonPropertyName("name")] public string Name { get; set; }

    }

}