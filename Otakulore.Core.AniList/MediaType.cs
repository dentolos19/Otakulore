using System.Runtime.Serialization;

namespace Otakulore.Core.AniList;

public enum MediaType
{

    [EnumMember(Value = "ANIME")] Anime,
    [EnumMember(Value = "MANGA")] Manga

}