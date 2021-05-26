using System.Runtime.Serialization;

namespace Otakulore.Api.Kitsu
{

    public enum KitsuDataType
    {

        [EnumMember(Value = "anime")] Anime,
        [EnumMember(Value = "manga")] Manga,
        [EnumMember(Value = "user")] User

    }

}