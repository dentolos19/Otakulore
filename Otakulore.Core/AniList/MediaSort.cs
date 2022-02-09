using System.Runtime.Serialization;

namespace Otakulore.Core.AniList;

public enum MediaSort
{

    [EnumMember(Value = "SEARCH_MATCH")] Relevance,
    [EnumMember(Value = "SCORE")] Score,
    [EnumMember(Value = "POPULARITY")] Popularity,
    [EnumMember(Value = "TRENDING")] Trending

}