using System.Runtime.Serialization;

namespace Otakulore.Core.Kitsu
{

    public enum KitsuDataType
    {

        [EnumMember(Value = "anime")] Anime,
        [EnumMember(Value = "manga")] Manga,

    }

}