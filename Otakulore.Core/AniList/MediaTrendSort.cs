using System.Runtime.Serialization;

namespace Otakulore.Core.AniList;

public enum MediaTrendSort
{

    [EnumMember(Value = "SCORE")] Score,
    [EnumMember(Value = "POPULARITY")] Popularity,
    [EnumMember(Value = "TRENDING")] Trending

}