using System.Runtime.Serialization;

namespace Otakulore.Core.Kitsu
{

    public enum KitsuAnimeFormat
    {

        [EnumMember(Value = "ONA")] Ona,
        [EnumMember(Value = "OVA")] Ova,
        [EnumMember(Value = "TV")] Tv,
        [EnumMember(Value = "movie")] Movie,
        [EnumMember(Value = "music")] Music,
        [EnumMember(Value = "special")] Special

    }

}