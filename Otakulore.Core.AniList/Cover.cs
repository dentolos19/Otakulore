using Newtonsoft.Json;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class Cover : Image
{

    public static GqlSelection[] Selections =>
        Image.Selections.Concat(new GqlSelection[]
        {
            new("extraLarge"),
            new("color")
        }).ToArray();

    [JsonProperty("extraLarge")] public string ExtraLargeImageUrl { get; init; }
    [JsonProperty("color")] public string Color { get; init; }

}