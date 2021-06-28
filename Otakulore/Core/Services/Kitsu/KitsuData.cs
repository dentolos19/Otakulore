using System.Text.Json.Serialization;

namespace Otakulore.Core.Services.Kitsu
{

    public class KitsuData<T>
    {

        [JsonPropertyName("id")] public string Id { get; set; } // int
        [JsonPropertyName("attributes")] public T Attributes { get; set; }

    }

}