using System.Drawing;
using Newtonsoft.Json;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class Cover : Image
{

    internal static GqlSelection[] Selections =>
        Image.Selections.Concat(new GqlSelection[]
        {
            new("extraLarge"),
            new("color")
        }).ToArray();

    [JsonProperty("color")] private readonly string? _colorHex;

    [JsonProperty("extraLarge")] public string ExtraLargeImageUrl { get; init; }
    public Color? Color => Utilities.ParseColorHex(_colorHex);

}