using System.Text.Json.Serialization;

namespace Otakulore.Core.Services.Kitsu
{

    public class KitsuImage
    {

        [JsonPropertyName("original")] public string ImageUrl { get; set; }

    }

}